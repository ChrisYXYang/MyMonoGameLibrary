using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyMonoGameLibrary.Audio;
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
    public static int Width { get; private set; }
    public static int Height { get; private set; }

    public static List<GameObject> OriginGameObject { get; set; } = [];
    public static List<GameObject> OriginUI { get; set; } = [];
    public static List<GameObject> ColliderGameObject { get; set; } = [];
    public static List<GameObject> ColliderUI { get; set; } = [];
    public static bool DrawTilemap { get; set; }

    // constructor
    //
    // param: title - title of window
    // param: width - width of screen
    // param: height - height of screen
    // param: fullScreen - full screen or not
    public Core(string title, int width, int height, bool fullScreen)
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
        Width = width;
        Height = height;
        Graphics.IsFullScreen = fullScreen;
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

        // load content for debugging
        Debugging.LoadContent();

        base.Initialize();
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
                if (DrawTilemap)
                    Debugging.DrawTilemapCollider(SceneTools.Tilemap);

                foreach (GameObject gameObject in ColliderGameObject)
                {
                    Debugging.DrawCollider(gameObject);
                }

                foreach (GameObject gameObject in OriginGameObject)
                {
                    Debugging.DrawOrigin(gameObject);
                }

                foreach (GameObject gameObject in ColliderUI)
                {
                    Debugging.DrawUICollider(gameObject);
                }

                foreach (GameObject gameObject in OriginUI)
                {
                    Debugging.DrawUIOrigin(gameObject);
                }
            }

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
}
