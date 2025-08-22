using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyMonoGameLibrary.Audio;
using MyMonoGameLibrary.Graphics;
using MyMonoGameLibrary.Input;
using MyMonoGameLibrary.Scenes;
using MyMonoGameLibrary.Tilemap;
using MyMonoGameLibrary.Tools;

namespace MyMonoGameLibrary;

// Core is an extension of the Game class that simplifies the code needed in Game1 by handling
// important tasks such as updating.
public class Core : Game
{
    // variables and properties
    internal static Core s_instance;
    public static Core Instance => s_instance;

    private static Scene s_activeScene;
    private static Scene s_nextScene;

    public static GraphicsDeviceManager Graphics { get; private set; }
    public static SpriteBatch SpriteBatch { get; private set; }
    public static new GraphicsDevice GraphicsDevice { get; private set; }
    public static new ContentManager Content { get; private set; }
    public static Library GlobalLibrary { get; private set; }
    public static AudioController Audio { get; private set; }
    public static Random Random { get; private set; } = new Random();


    // for debugging
    private static SpriteSheet _sprites;
    private static Sprite _originPoint;
    private static Sprite _boxCollider;
    private static Sprite _circleCollider;
    private static Color _colliderColor = Color.White * 0.8f;

    private static List<GameObject> _originGameObject = [];
    private static List<GameObject> _originUI = [];
    private static List<GameObject> _colliderGameObject = [];
    private static List<GameObject> _colliderUI = [];
    public static bool DrawTilemap { get; set; }

    // game settings
    public static bool Particles { get; set; } = true;

    // constructor
    //
    // param: title - title of window
    // param: width - width of screen
    // param: height - height of screen
    // param: fullScreen - full screen or not
    public Core(string title, int width, int height)
    {
        // ensure only one core
        if (s_instance != null)
        {
            throw new InvalidOperationException("only single core");
        }

        // store reference for core
        s_instance = this;

        // create new graphics device manager
        Graphics = new GraphicsDeviceManager(this);

        // set grpahics defaults
        Graphics.PreferredBackBufferWidth = width;
        Graphics.PreferredBackBufferHeight = height;
        Graphics.ApplyChanges();

        // mouse visible by default
        IsMouseVisible = true;

        // window title
        Window.Title = title;

        // set content manager to base Game's
        Content = base.Content;

        // root directory for content
        Content.RootDirectory = "Content";

        // set sprite library
        GlobalLibrary = new Library(Content);
    }

    protected override void Initialize()
    {
        // set graphics device to base Game's
        GraphicsDevice = base.GraphicsDevice;

        // create sprite batch instance
        SpriteBatch = new SpriteBatch(GraphicsDevice);

        // create audio controller
        Audio = new AudioController();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        // load content for debugging
        _sprites = new SpriteSheet(Core.Content, "debug");
        _originPoint = _sprites.GetSprite("origin_point");
        _boxCollider = _sprites.GetSprite("box_collider");
        _circleCollider = _sprites.GetSprite("circle_collider");

        base.LoadContent();
    }

    protected override void UnloadContent()
    {
        Audio.Dispose();
        
        base.UnloadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        InputManager.Update();
        Audio.Update();

        // if there is next scene waiting to switch to, transition to that scene
        if (s_nextScene != null)
        {
            TransitionScene();
        }

        // if active scene, update it
        if (s_activeScene != null)
        {
            s_activeScene.Update(gameTime);
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        // if there is active scene, draw it
        if (s_activeScene != null)
        {
            s_activeScene.Draw(gameTime);

            // draw debugging visualizations
            SpriteBatch.Begin(samplerState: SamplerState.PointClamp, sortMode: SpriteSortMode.FrontToBack);

            if (SceneTools.ActiveScene)
            {
                // draw tilemap
                if (DrawTilemap)
                {
                    if (SceneTools.Tilemap != null)
                    {
                        TileMap tilemap = SceneTools.Tilemap;

                        List<string> layerNames = tilemap.Layers;

                        foreach (string layerName in layerNames)
                        {
                            for (int i = 0; i < tilemap.Rows; i++)
                            {
                                for (int j = 0; j < tilemap.Columns; j++)
                                {
                                    Tile tile = tilemap.GetTile(layerName, i, j);

                                    if (tile == null)
                                        continue;

                                    if (tile.Collider == null)
                                        continue;

                                    DrawBoxCollider(tile.Collider);
                                }
                            }
                        }
                    }
                }

                // draw collider
                foreach (GameObject gameObject in _colliderGameObject)
                {
                    ICollider collider = gameObject.Collider;

                    if (collider != null)
                    {
                        if (collider is BoxCollider box)
                            DrawBoxCollider(box);
                        else if (collider is CircleCollider circle)
                            DrawCircleCollider(circle);
                    }
                }

                // draw origin
                foreach (GameObject gameObject in _originGameObject)
                {
                    Transform transform = gameObject.Transform;

                    if (transform == null)
                        return;

                    Camera.Draw
                        (
                            _originPoint,
                            transform.TruePosition,
                            Color.White,
                            0f,
                            0.05f,
                            SpriteEffects.None,
                            1f
                        );
                }

                // draw ui collider
                foreach (GameObject gameObject in _colliderUI)
                {
                    ICollider collider = gameObject.Collider;

                    if (collider != null)
                    {
                        if (collider is BoxCollider box)
                            DrawUIBoxCollider(box);
                        else if (collider is CircleCollider circle)
                            DrawUICircleCollider(circle);
                    }
                }

                // draw ui origin
                foreach (GameObject gameObject in _originUI)
                {
                    Transform transform = gameObject.Transform;

                    if (transform == null)
                        return;

                    Core.SpriteBatch.Draw
                        (
                            _originPoint.SpriteSheet,
                            transform.TruePosition,
                            _originPoint.SourceRectangle,
                            Color.White,
                            0f,
                            _originPoint.OriginPoint,
                            Vector2.One,
                            SpriteEffects.None,
                            1f
                        );
                }
            }

            _colliderGameObject.Clear();
            _originGameObject.Clear();
            _colliderUI.Clear();
            _originUI.Clear();
            SpriteBatch.End();
        }

        base.Draw(gameTime);
    }

    public static void ChangeScene(Scene next)
    {
        // Only set the next scene value if it is not the same
        // instance as the currently active scene.
        if (s_activeScene != next)
        {
            s_nextScene = next;
        }
    }

    private static void TransitionScene()
    {
        // If there is an active scene, dispose of it
        List<GameObject> persist = [];
        if (s_activeScene != null)
        {
            persist = s_activeScene.GetPersisting();
            s_activeScene.Dispose();
        }

        // Force the garbage collector to collect to ensure memory is cleared
        GC.Collect();

        // Change the currently active scene to the new scene
        s_activeScene = s_nextScene;
        SceneTools.ChangeActive(s_nextScene);

        // Null out the next scene value so it does not trigger a change over and over.
        s_nextScene = null;

        // If the active scene now is not null, initialize it.
        // Remember, just like with Game, the Initialize call also calls the
        // Scene.LoadContent
        if (s_activeScene != null)
        {
            s_activeScene.Persisting = persist;
            s_activeScene.Initialize();
        }
    }

    // draw origin point for a gameobject
    //
    // param: gameObject - game object to draw origin point for
    public static void DrawOrigin(GameObject gameObject)
    {
        _originGameObject.Add(gameObject);
    }

    // draw collider for game object
    //
    // param: gameObject - game object to draw
    public static void DrawCollider(GameObject gameObject)
    {
        _colliderGameObject.Add(gameObject);
    }

    // draw origin point for ui
    //
    // param: gameObject - ui to draw origin point for
    public static void DrawUIOrigin(GameObject gameObject)
    {
        _originUI.Add(gameObject);
    }


    // draw collider for ui
    //
    // param: gameObject - game object to draw
    public static void DrawUICollider(GameObject gameObject)
    {
        _colliderUI.Add(gameObject);
    }

    // helper method to draw box collider
    //
    // param: collider - box collider to draw
    private static void DrawBoxCollider(IAABBCollider collider)
    {
        Camera.Draw
            (
                _boxCollider,
                new Vector2(collider.Left, collider.Top),
                _colliderColor,
                0f,
                new Vector2(collider.Right - collider.Left, collider.Bottom - collider.Top) * 0.5f,
                SpriteEffects.None,
                0.9f
            );
    }

    // helper method to draw circle collider 
    //
    // param: collider - circle collider to draw
    private static void DrawCircleCollider(ICircleCollider collider)
    {
        Camera.Draw
            (
                _circleCollider,
                collider.Center,
                _colliderColor,
                0f,
                collider.Radius,
                SpriteEffects.None,
                0.9f
            );
    }

    // helper method to draw ui box collider
    //
    // param: collider - box collider to draw
    private static void DrawUIBoxCollider(BoxCollider collider)
    {
        Core.SpriteBatch.Draw
            (
                _boxCollider.SpriteSheet,
                new Vector2(collider.Left, collider.Top),
                _boxCollider.SourceRectangle,
                _colliderColor,
                0f,
                _boxCollider.OriginPoint,
                 new Vector2(collider.Right - collider.Left, collider.Bottom - collider.Top) / 16f,
                SpriteEffects.None,
                0.9f
            );
    }

    // helper method to draw ui circle collider 
    //
    // param: collider - circle collider to draw
    private static void DrawUICircleCollider(CircleCollider collider)
    {
        Core.SpriteBatch.Draw
            (
                _circleCollider.SpriteSheet,
                collider.Center,
                _circleCollider.SourceRectangle,
                _colliderColor,
                0f,
                _circleCollider.OriginPoint,
                collider.Diameter / 16f,
                SpriteEffects.None,
                0.9f
            );
    }
}
