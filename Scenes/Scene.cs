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

    public Canvas Canvas { get; private set; } = new Canvas();
    protected TileMap Tilemap { get; private set; }

    private readonly Dictionary<string, int> _names = [];
    private readonly Dictionary<string, GameObject> _gameObjects = [];
    private readonly List<GameObject> _toInstantiate = [];
    private readonly List<GameObject> _toDestroy = [];

    private readonly Dictionary<string, ICollider> _colliders = [];
    private readonly List<TileCollider> _tileColliders = [];

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
        foreach (GameObject gameObject in _gameObjects.Values)
        {
            gameObject.StartBehaviors();
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
        // destroy instantiated objects from last frame
        foreach (GameObject gameObject in _toDestroy)
        {
            _gameObjects.Remove(gameObject.Name);
            _colliders.Remove(gameObject.Name);
        }
        _toDestroy.Clear();
        
        // setup instantiated objects from last frame
        foreach (GameObject gameObject in _toInstantiate)
        {
            _gameObjects.Add(gameObject.Name, gameObject);

            // register collider
            ICollider collider = gameObject.Collider;
            if (collider != null)
                _colliders.Add(gameObject.Name, collider);
        }
        _toInstantiate.Clear();
        
        // store previous position of rigidbody
        foreach (GameObject gameObject in _gameObjects.Values)
        {
            if (gameObject.Rigidbody != null)
                gameObject.Rigidbody.UpdatePrevPos();
        }

        // behavior update
        foreach (GameObject gameObject in _gameObjects.Values)
        {
            gameObject.UpdateBehaviors(gameTime);
        }

        // update rigidbodies
        foreach (GameObject gameObject in _gameObjects.Values)
        {
            Rigidbody rigidbody = gameObject.Rigidbody;

            if (rigidbody == null)
                continue;

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
        List<ICollider> colliderList = [.. _colliders.Values];
        for (int i = 0; i < colliderList.Count; i++)
        {
            for (int k = i + 1; k < colliderList.Count; k++)
            {
                if (Collisions.Intersect(colliderList[i], colliderList[k]))
                {
                    colliderList[i].Colliding(colliderList[k]);
                    colliderList[k].Colliding(colliderList[i]);
                }
                else
                {
                    colliderList[i].NotColliding(colliderList[k]);
                    colliderList[k].NotColliding(colliderList[i]);
                }
            }
        }

        // behavior late update
        foreach (GameObject gameObject in _gameObjects.Values)
        {
            gameObject.LateUpdateBehaviors(gameTime);
        }

        // update UI
        Canvas.Update(gameTime);

        // update aniamtions
        foreach (GameObject gameObject in _gameObjects.Values)
        {
            if (gameObject.Animator != null)
                gameObject.Animator.Update(gameTime);
        }
    }

    /// <summary>
    /// Draws this scene.
    /// </summary>
    /// <param name="gameTime">A snapshot of the timing values for the current frame.</param>
    public virtual void Draw(GameTime gameTime) 
    {
        // game rendering
        foreach (GameObject gameObject in _gameObjects.Values)
        {
            if (gameObject.Renderer != null)
                Camera.Draw(gameObject.Renderer);
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

    // setup a gameobject using a list of components and register it
    //
    // param: name - name of game object
    // param: components - components of game object
    // return: game object that was created
    public GameObject Setup(string name, Component[] components)
    {
        // check if name is unique and register it
        name = RegisterName(name);

        // create game object
        GameObject gameObject = new(name, components);
        _gameObjects.Add(name, gameObject);

        // register collider
        ICollider collider = gameObject.Collider;
        if (collider != null)
            _colliders.Add(name, collider);

        return gameObject;
    }

    // setup a gameobject using prefab
    //
    // param: prefab - prefab
    public void Setup((string, Component[]) prefab)
    {
        Setup(prefab.Item1, prefab.Item2);
    }

    // setup a gameobject using prefab
    //
    // param: prefab - prefab
    // param: position - position
    public void Setup((string, Component[]) prefab, Vector2 position)
    {
        GameObject gameObject = Setup(prefab.Item1, prefab.Item2);
        gameObject.Transform.position = position;
    }

    // setup a gameobject using prefab
    //
    // param: prefab - prefab
    // param: position - position
    // param: rotation - rotation
    public void Setup((string, Component[]) prefab, Vector2 position, float rotation)
    {
        GameObject gameObject = Setup(prefab.Item1, prefab.Item2);
        gameObject.Transform.position = position;
        gameObject.Transform.Rotation = rotation;
    }

    // setup a gameobject using prefab
    //
    // param: prefab - prefab
    // param: position - position
    // param: rotation - rotation
    // param: parent - parent game object
    public void Setup((string, Component[]) prefab, Vector2 position, float rotation, GameObject parent)
    {
        GameObject gameObject = Setup(prefab.Item1, prefab.Item2);
        gameObject.Transform.position = position;
        gameObject.Transform.Rotation = rotation;
        gameObject.SetParent(parent);
    }

    // instantiate a gameobject using a list of components
    //
    // param: name - name of game object
    // param: components - components of game object
    // return: game object that was created
    public GameObject Instantiate(string name, Component[] components)
    {
        // check if name is unique and register it
        name = RegisterName(name);

        GameObject gameObject = new(name, components);
        _toInstantiate.Add(gameObject);
        
        return gameObject;
    }

    // instantiate a gameobject using prefab
    //
    // param: prefab - prefab
    public void Instantiate((string, Component[]) prefab)
    {
        Instantiate(prefab.Item1, prefab.Item2);
    }

    // instantiate a gameobject using prefab
    //
    // param: prefab - prefab
    // param: position - position
    public void Instantiate((string, Component[]) prefab, Vector2 position)
    {
        GameObject gameObject = Setup(prefab.Item1, prefab.Item2);
        gameObject.Transform.position = position;
    }

    // instantiate a gameobject using prefab
    //
    // param: prefab - prefab
    // param: position - position
    // param: rotation - rotation
    public void Instantiate((string, Component[]) prefab, Vector2 position, float rotation)
    {
        GameObject gameObject = Setup(prefab.Item1, prefab.Item2);
        gameObject.Transform.position = position;
        gameObject.Transform.Rotation = rotation;
    }

    // instantiate a gameobject using prefab
    //
    // param: prefab - prefab
    // param: position - position
    // param: rotation - rotation
    // param: parent - parent game object
    public void Instantiate((string, Component[]) prefab, Vector2 position, float rotation, GameObject parent)
    {
        GameObject gameObject = Setup(prefab.Item1, prefab.Item2);
        gameObject.Transform.position = position;
        gameObject.Transform.Rotation = rotation;
        gameObject.SetParent(parent);
    }

    // destroy a game object
    //
    // param: gameObject - game object to destroy
    public void Destroy(GameObject gameObject)
    {
        gameObject.SetParent(null);
        _toDestroy.Add(gameObject);

        for (int i = 0; i < gameObject.ChildCount; i++)
        {
            Destroy(gameObject.GetChild(i));
        }
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

                    _colliders.Add(tile.Name, tile.GetCollider());
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
