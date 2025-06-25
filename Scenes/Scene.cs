using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyMonoGameLibrary.Graphics;
using MyMonoGameLibrary.Input;
using MyMonoGameLibrary.Tilemap;
using MyMonoGameLibrary.UI;

namespace MyMonoGameLibrary.Scenes;

// this class represents the inner workings of a scene such as updating all the gameobjects,
// handling the physics, etc.
public abstract class Scene : IDisposable
{
    // variables and properties
    public Library SceneLibrary { get; private set; }

    protected Canvas Canvas { get; private set; } = new Canvas();
    protected TileMap Tilemap { get; private set; }

    private readonly Dictionary<string, int> _names = [];
    private readonly Dictionary<string, GameObject> _gameObjects = [];
    private readonly List<BehaviorComponent> _behaviors = [];
    private readonly List<Animator> _animators = [];
    private readonly List<ICollider> _colliders = [];
    private readonly List<RendererComponent> _renderComps = [];

    private readonly List<TileCollider> _tileColliders = [];
    private readonly List<Rigidbody> _rigidbodies = [];

    public float Gravity { get; protected set; } = 9.8f;

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
        SceneLibrary = new Library(Content);
        LoadContent();
    }

    /// <summary>
    /// Override to provide logic to load content for the scene.
    /// </summary>
    public virtual void LoadContent()
    {
        // start behavior scripts
        foreach (BehaviorComponent behavior in _behaviors)
        {
            behavior.Start();
        }

        Canvas.Start();

        // testing
        List<GameObject> gameObjects = [.. _gameObjects.Values];
        for (int i = 0; i < gameObjects.Count; i++)
        {
            Debug.WriteLine(gameObjects[i].Name);

            for (int k = i + 1; k < gameObjects.Count; k++)
            {
                if (gameObjects[i].Name.Equals(gameObjects[k].Name))
                {
                    throw new Exception("same name");
                }
            }
        }
    }


    /// <summary>
    /// Updates this scene.
    /// </summary>
    /// <param name="gameTime">A snapshot of the timing values for the current frame.</param>
    public virtual void Update(GameTime gameTime) 
    {
        // store previous position of rigidbody
        foreach (Rigidbody rigidbody in _rigidbodies)
        {
            rigidbody.UpdatePrevPos();
        }

        // behavior update
        foreach (BehaviorComponent behavior in _behaviors)
        {
            behavior.Update(gameTime);
        }

        // update rigidbodies
        foreach (Rigidbody rigidbody in _rigidbodies)
        {
            // afflict gravity
            if (rigidbody.UseGravity)
                rigidbody.YVelocity += Gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // move rigidbodies
            Vector2 movePosition = rigidbody.GetMovePosition();
            rigidbody.Transform.position.X += (movePosition.X + rigidbody.XVelocity) * (float)gameTime.ElapsedGameTime.TotalSeconds;
            rigidbody.Transform.position.Y += (movePosition.Y + rigidbody.YVelocity) * (float)gameTime.ElapsedGameTime.TotalSeconds;
            rigidbody.ClearMovePosition();

            // get tile collisions with rigidbody
            foreach (TileCollider tileCol in _tileColliders)
            {
                if (Collisions.Intersect(rigidbody.Collider, tileCol))
                {
                    rigidbody.AddCollision(tileCol);
                }
                else
                {
                    rigidbody.RemoveCollision(tileCol);
                }
            }

            // correct collisions with tilemap
            rigidbody.CorrectPosition();

        }

        // update collisions
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

        // behavior late update
        foreach (BehaviorComponent behavior in _behaviors)
        {
            behavior.LateUpdate(gameTime);
        }

        // update UI
        Canvas.Update(gameTime);

        // update aniamtions
        foreach (Animator animator in _animators)
        {
            animator.Update(gameTime);
        }
    }

    /// <summary>
    /// Draws this scene.
    /// </summary>
    /// <param name="gameTime">A snapshot of the timing values for the current frame.</param>
    public virtual void Draw(GameTime gameTime) 
    {
        // game rendering
        foreach (RendererComponent renderer in _renderComps)
        {
            Camera.Draw(renderer);
        }
        Camera.Draw(Tilemap);
        Core.SpriteBatch.End();

        // UI rendering
        Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp, sortMode: SpriteSortMode.Deferred);
        UICamera.Draw(Canvas);
        Core.SpriteBatch.End();

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

    // add a behavior to list
    //
    // param: behavior - behavior to add
    public void AddBehavior(BehaviorComponent behavior)
    {
        _behaviors.Add(behavior);
    }


    // instantiate a gameobject using a list of components and register it
    //
    // param: name - name of game object
    // param: components - components of game object
    public void Instantiate(string name, Component[] components)
    {
        // check if name is unique and register it
        name = RegisterName(name);

        // create game object
        GameObject gameObject = new(name, components);
        _gameObjects.Add(name, gameObject);

        // register relevant gameobject components
        foreach (BehaviorComponent behavior in gameObject.GetBehaviors())
        {
            _behaviors.Add(behavior);
        }

        RendererComponent renderer = gameObject.GetRenderer();
        if (renderer != null)
            _renderComps.Add(renderer);

        ICollider collider = gameObject.GetCollider();
        if (collider != null)
            _colliders.Add(collider);

        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        if (rb != null)
            _rigidbodies.Add(rb);

        Animator anim = gameObject.GetComponent<Animator>();
        if (anim != null)
            _animators.Add(anim);
    }

    // instantiate a gameobject using prefab
    //
    // param: prefab - prefab
    public void Instantiate((string, Component[]) prefab)
    {
        Instantiate(prefab.Item1, prefab.Item2);
    }

    // set the tilemap and register it
    //
    // param: name - name of tilemap
    // param: tileset - the tileset
    public void SetTilemap(string name, Tileset tileset)
    {
        // check if name is unique and register it
        name = RegisterName(name);

        // create tilemap
        TileMap tileMap = new(name, tileset);
        Tilemap = tileMap;

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
                    _tileColliders.Add(tile.GetCollider());

                    if (_names.ContainsKey(tile.Name))
                        throw new Exception("name taken");
                    else
                        _names.Add(tile.Name, 0);

                }
            }
        }
    }

    // check if name is unique and register
    //
    // param: name - name to check and register
    // return: new unique name
    private string RegisterName(string name)
    {
        name = name.Split('_')[0];

        if (_names.ContainsKey(name))
        {
            _names[name] += 1;
            name = name + "_" + _names[name];
        }
        else
            _names.Add(name, 0);

        return name;
    }
}
