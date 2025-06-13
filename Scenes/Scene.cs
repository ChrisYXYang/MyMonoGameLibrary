using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyMonoGameLibrary.Graphics;
using MyMonoGameLibrary.Input;
using MyMonoGameLibrary.Tilemap;

namespace MyMonoGameLibrary.Scenes;

public abstract class Scene : IDisposable
{
    // variables and properties
    protected SpriteLibrary SceneSpriteLibrary { get; private set; }

    private readonly HashSet<string> _names = [];
    private readonly Dictionary<string, GameObject> _gameObjects = [];
    private readonly Dictionary<string, TileMap> _tileMaps = [];
    private readonly List<IBehavior> _behaviors = [];
    private readonly List<IAnimator> _animators = [];
    private readonly List<ICollider> _colliders = [];
    private readonly List<IGameRenderer> _renderers = [];

    /// <summary>
    /// Gets the ContentManager used for loading scene-specific assets.
    /// </summary>
    /// <remarks>
    /// Assets loaded through this ContentManager will be automatically unloaded when this scene ends.
    /// </remarks>
    protected ContentManager Content { get; }

    /// <summary>
    /// Gets a value that indicates if the scene has been disposed of.
    /// </summary>
    public bool IsDisposed { get; private set; }

    /// <summary>
    /// Creates a new scene instance.
    /// </summary>
    public Scene()
    {
        // Create a content manager for the scene
        Content = new ContentManager(Core.Content.ServiceProvider);

        // Set the root directory for content to the same as the root directory
        // for the game's content.
        Content.RootDirectory = Core.Content.RootDirectory;
    }

    // Finalizer, called when object is cleaned up by garbage collector.
    ~Scene() => Dispose(false);

    /// <summary>
    /// Initializes the scene.
    /// </summary>
    /// <remarks>
    /// When overriding this in a derived class, ensure that base.Initialize()
    /// still called as this is when LoadContent is called.
    /// </remarks>
    public virtual void Initialize()
    {
        SceneSpriteLibrary = new SpriteLibrary();
        LoadContent();
    }

    /// <summary>
    /// Override to provide logic to load content for the scene.
    /// </summary>
    public virtual void LoadContent()
    {
        // start behavior scripts
        foreach (IBehavior behavior in _behaviors)
        {
            behavior.Start();
        }
    }


    /// <summary>
    /// Updates this scene.
    /// </summary>
    /// <param name="gameTime">A snapshot of the timing values for the current frame.</param>
    public virtual void Update(GameTime gameTime) 
    {
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
                if (Collisions.Intersect(_colliders[i], _colliders[k]))
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
    }

    /// <summary>
    /// Draws this scene.
    /// </summary>
    /// <param name="gameTime">A snapshot of the timing values for the current frame.</param>
    public virtual void Draw(GameTime gameTime) 
    {
        // game rendering
        foreach (IGameRenderer renderer in _renderers)
        {
            Camera.Draw(renderer);
        }
    }

    /// <summary>
    /// Disposes of this scene.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Disposes of this scene.
    /// </summary>
    /// <param name="disposing">'
    /// Indicates whether managed resources should be disposed.  This value is only true when called from the main
    /// Dispose method.  When called from the finalizer, this will be false.
    /// </param>
    protected virtual void Dispose(bool disposing)
    {
        if (IsDisposed)
        {
            return;
        }

        if (disposing)
        {
            Content.Dispose();
        }

        IsDisposed = true;
    }

    // get a game object
    //
    // param: name - name of game object
    // return: requested gameobject or throws exception
    public GameObject GetGameObject(string name)
    {
        return _gameObjects[name];
    }

    // get all game objects
    //
    // return: list of all game objects
    public List<GameObject> GetGameObjects()
    {
        return [.. _gameObjects.Values];
    }

    // get all tilemaps
    //
    // return: list of all game objects
    public List<TileMap> GetTileMaps()
    {
        return [.. _tileMaps.Values];
    }

    // add a behavior to list
    //
    // param: behavior - behavior to add
    public void AddBehavior(IBehavior behavior)
    {
        _behaviors.Add(behavior);
    }


    // instantiate a gameobject and register it
    //
    // param: name - name of game object
    // param: components - components of game object
    public void Instantiate(string name, Component[] components)
    {
        // create game object
        GameObject gameObject = new(name, components);

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
    // param: tileset - the tileset
    public void Instantiate(string name, Tileset tileset)
    {
        // create tilemap
        TileMap tileMap = new(name, tileset);

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
