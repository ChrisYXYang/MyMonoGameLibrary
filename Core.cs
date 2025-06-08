using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using MyMonoGameLibrary.Scene;
using MyMonoGameLibrary.Tilemap;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using MyMonoGameLibrary.Input;

namespace MyMonoGameLibrary;

// Core is an extension of the Game class that simplifies the code needed in Game1 by handling
// important tasks such as updating.
public class Core : Game
{
    // variables and properties
    internal static Core s_instance;
    
    public static Core Instance => s_instance;
    public static GraphicsDeviceManager Graphics { get; private set; }
    public static SpriteBatch SpriteBatch { get; private set; }
    public static new GraphicsDevice GraphicsDevice { get; private set; }
    public static new ContentManager Content { get; private set; }

    // for the game
    protected HashSet<string> _names = new HashSet<string>();
    protected Dictionary<string, GameObject> _gameObjects = new Dictionary<string, GameObject>();
    protected Dictionary<string, TileMap> _tileMaps = new Dictionary<string, TileMap>();
    protected List<IBehavior> _behaviors = new List<IBehavior>();
    protected List<IAnimator> _animators = new List<IAnimator>();
    protected List<IRectCollider> _colliders = new List<IRectCollider>();
    protected List<IRenderer> _renderers = new List<IRenderer>();

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
        Graphics.IsFullScreen = fullScreen;
        Graphics.ApplyChanges();

        // window title
        Window.Title = title;

        // set content manager to base Game's
        Content = base.Content;

        // root directory for content
        Content.RootDirectory = "Content";

        // mouse visible by default
        IsMouseVisible = true;
    }

    protected override void LoadContent()
    {
        // start behavior scripts
        foreach (IBehavior behavior in _behaviors)
        {
            behavior.Start();
        }
    }

    protected override void Initialize()
    {
        base.Initialize();

        // set graphics device to base Game's
        GraphicsDevice = base.GraphicsDevice;

        // create sprite batch instance
        SpriteBatch = new SpriteBatch(GraphicsDevice);
    }

    protected override void Update(GameTime gameTime)
    {
        // update input
        InputManager.Update();

        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
        {
            Exit();
        }

        // TODO: Add your update logic here

        // update behaviors
        foreach (IBehavior behavior in _behaviors)
        {
            behavior.Update(gameTime);
        }

        foreach (IBehavior behavior in _behaviors)
        {
            behavior.LateUpdate(gameTime);
        }

        // update aniamtions
        foreach (IAnimator animator in _animators)
        {
            animator.Update(gameTime);
        }

        // check collisions
        for (int i = 0; i < _colliders.Count; i++)
        {
            for (int k = i + 1; k < _colliders.Count; k++)
            {
                if (Collisions.AABBIntersect(_colliders[i], _colliders[k]))
                {
                    _colliders[i].Colliding(_colliders[k]);
                    _colliders[k].Colliding(_colliders[i]);
                }
                else
                {
                    _colliders[i].NotColliding(_colliders[k]);
                    _colliders[k].NotColliding(_colliders[i]);
                }
            }
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        // game rendering
        foreach (IRenderer renderer in _renderers)
        {
            renderer.Draw();
        }

        SpriteBatch.End();

        base.Draw(gameTime);
    }

    // get a game object
    //
    // param: name - name of game object
    // return: requested gameobject or throws exception
    public GameObject GetGameObject(string name)
    {
        return _gameObjects[name];
    }


    // instantiate a gameobject and register it
    //
    // param: name - name of game object
    // param: components - components of game object
    public void Instantiate(string name, Component[] components)
    {
        // create game object
        GameObject gameObject = new GameObject(name, components);

        _gameObjects.Add(name, gameObject);

        if (!_names.Add(name))
            throw new Exception("name already taken");

        // register relevant gameobject components
        foreach (IBehavior behavior in gameObject.GetBehaviors())
        {
            _behaviors.Add(behavior);
        }

        if (gameObject.GetRenderer() != null)
            _renderers.Add(gameObject.GetRenderer());

        if (gameObject.GetCollider() != null)
            _colliders.Add(gameObject.GetCollider());

        if (gameObject.GetAnimator() != null)
            _animators.Add(gameObject.GetAnimator());
    }

    // instantiate a tilemap and register it
    //
    // param: name - name of tilemap
    public void Instantiate(string name)
    {
        // create tilemap
        TileMap tileMap = new TileMap(name);

        // add tilemap to dictionary
        _tileMaps.Add(name, tileMap);

        if (!_names.Add(name))
            throw new Exception("name already taken");

        // add tilemap to renderers
        _renderers.Add(tileMap.GetRenderer());

        // add tile colliders to colliders
        List<string> layerNames = tileMap.Layers;

        foreach (string layerName in layerNames)
        {
            for (int i = 0; i < tileMap.Rows; i++)
            {
                for (int j = 0; j < tileMap.Columns; j++)
                {
                    Tile tile = tileMap.GetTile(layerName, i, j);

                    if (tile == null)
                        continue;

                    if (tile.Collider == null)
                        continue;

                    _colliders.Add(tile.GetCollider());

                    if (!_names.Add(tile.Name))
                        throw new Exception("name taken");

                }
            }
        }
    }
}
