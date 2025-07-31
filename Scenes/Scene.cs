using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyMonoGameLibrary.Input;
using MyMonoGameLibrary.Tilemap;
using MyMonoGameLibrary.Tools;
using MyMonoGameLibrary.UI;

namespace MyMonoGameLibrary.Scenes;

// this class represents the inner workings of a scene such as updating all the gameobjects,
// handling the physics, etc.
public abstract class Scene : IDisposable
{
    // variables and properties
    public Library SceneLibrary { get; private set; }
    public TileMap Tilemap { get; private set; }

    private readonly Dictionary<string, int> _names = [];

    private readonly Dictionary<string, GameObject> _gameObjects = [];
    private readonly List<GameObject> _toInstantiate = [];
    private readonly List<GameObject> _toDestroy = [];

    private readonly Dictionary<string, ColliderComponent> _colliders = [];
    private readonly Dictionary<string, GameObject> _gameDraw = [];
    private readonly Dictionary<string, GameObject> _uiDraw = [];
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
        Camera.position = Vector2.Zero;
        LoadContent();
    }

    /// <summary>
    /// Override to provide logic to load content for the scene.
    /// </summary>
    public virtual void LoadContent()
    {
        // awake behavior scripts
        foreach (GameObject gameObject in _gameObjects.Values)
        {
            gameObject.AwakeBehaviors();
        }

        // start behavior scripts
        foreach (GameObject gameObject in _gameObjects.Values)
        {
            gameObject.StartBehaviors();
        }
        UpdateDestroyInstantiate();
    }


    /// <summary>
    /// Updates this scene.
    /// </summary>
    /// <param name="gameTime">A snapshot of the timing values for the current frame.</param>
    public virtual void Update(GameTime gameTime) 
    {
        // store previous position of rigidbody
        foreach (GameObject gameObject in _gameObjects.Values)
        {
            gameObject.Rigidbody?.UpdatePrevPos();
        }

        // behavior update
        foreach (GameObject gameObject in _gameObjects.Values)
        {
            gameObject.UpdateBehaviors(gameTime);
        }
        UpdateDestroyInstantiate();

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
            if (rigidbody.Solid)
            {
                foreach (TileCollider tileCol in _tileColliders)
                {
                    if (!tileCol.Solid)
                        continue;
                    
                    if (Collisions.Intersect(rigidbody.Collider, tileCol))
                    {
                        rigidbody.AddCollision(tileCol);
                    }
                    else
                    {
                        rigidbody.RemoveCollision(tileCol);
                    }
                }
            }

            // correct collisions with tilemap
            rigidbody.CorrectPosition();

        }

        // update collisions between game objects
        List<ColliderComponent> colliderList = [.. _colliders.Values];
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

        // udpate collisions betweeen game objects and tile colliders
        foreach (ColliderComponent col in colliderList)
        {
            foreach (TileCollider tile in _tileColliders)
            {
                if (Collisions.Intersect(col, tile))
                {
                    col.Colliding(tile);
                }
                else
                {
                    col.NotColliding(tile);
                }
            }
        }

        UpdateDestroyInstantiate();

        // behavior late update
        foreach (GameObject gameObject in _gameObjects.Values)
        {
            gameObject.LateUpdateBehaviors(gameTime);
        }
        UpdateDestroyInstantiate();


        // update aniamtions
        foreach (GameObject gameObject in _gameObjects.Values)
        {
            if (gameObject.Animator != null)
                gameObject.Animator.Update(gameTime);
        }
    }

    // updates destroy and instantiated bojects
    private void UpdateDestroyInstantiate()
    {
        // setup instantiated objects from last frame
        foreach (GameObject gameObject in _toInstantiate)
        {
            // register game object
            _gameObjects.Add(gameObject.Name, gameObject);

            // register renderer
            if (gameObject.Renderer is UIText || gameObject.Renderer is UISprite)
            {
                _uiDraw.Add(gameObject.Name, gameObject);
            }
            else
            {
                if (gameObject.Renderer is TextRenderer || gameObject.Renderer is SpriteRenderer)
                    _gameDraw.Add(gameObject.Name, gameObject);

                // register collider
                ColliderComponent collider = gameObject.Collider;
                if (collider != null)
                {
                    _colliders.Add(gameObject.Name, collider);
                }
            }
        }
        _toInstantiate.Clear();

        // destroy instantiated objects from last frame
        foreach (GameObject gameObject in _toDestroy)
        {
            // remove from scene/unregister
            gameObject.SetParent(null);
            _gameObjects.Remove(gameObject.Name);

            if (gameObject.Renderer is UIText || gameObject.Renderer is UISprite)
            {
                _uiDraw.Remove(gameObject.Name);
            }
            else
            {
                if (gameObject.Renderer is TextRenderer || gameObject.Renderer is SpriteRenderer)
                    _gameDraw.Remove(gameObject.Name);

                // remove collider
                if (gameObject.Collider != null)
                {
                    if (gameObject.Collider.Layer != "ui")
                    {
                        _colliders.Remove(gameObject.Name);

                        foreach (ICollider collider in gameObject.Collider.GetCollisions())
                        {
                            collider.NotColliding(gameObject.Collider);
                        }
                    }
                }
            }
        }
        _toDestroy.Clear();
    }

    /// <summary>
    /// Draws this scene.
    /// </summary>
    /// <param name="gameTime">A snapshot of the timing values for the current frame.</param>
    public virtual void Draw(GameTime gameTime) 
    {
        // game rendering
        foreach (GameObject gameObject in _gameDraw.Values)
        {
            Camera.Draw(gameObject.Renderer);
        }
        Camera.Draw(Tilemap);
        Core.SpriteBatch.End();

        // UI rendering
        Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp, sortMode: SpriteSortMode.FrontToBack);
        
        foreach (GameObject gameObject in _uiDraw.Values)
        {
            UICamera.Draw(gameObject);
        }

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
    /// Unloads scene-specific content.
    /// </summary>
    public virtual void UnloadContent()
    {
        Content.Unload();
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
            UnloadContent();
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

    // get all game draw objects
    //
    // return: list of all game draw objects
    public List<GameObject> GetGameDrawObjects()
    {
        return [.. _gameDraw.Values];
    }

    // get all ui draw objects
    //
    // return: list of all ui draw objects
    public List<GameObject> GetUIDrawObjects()
    {
        return [.. _uiDraw.Values];
    }

    // get all game objects
    //
    // return: list of all game objects
    public List<GameObject> GetGameObjects()
    {
        return [.. _gameObjects.Values];
    }

    // get colliders
    //
    // return: list of all colliders
    public List<ICollider> GetColliders()
    {
        return [.. _colliders.Values];
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

        // register renderer
        if (gameObject.Renderer is UIText || gameObject.Renderer is UISprite)
        {
            _uiDraw.Add(name, gameObject);
        }
        else 
        {
            if (gameObject.Renderer is TextRenderer || gameObject.Renderer is SpriteRenderer)
                _gameDraw.Add(name, gameObject);

            // register collider
            ColliderComponent collider = gameObject.Collider;
            if (collider != null)
            {
                if (collider.Layer != "ui")
                {
                    _colliders.Add(gameObject.Name, collider);
                }
            }
        }

        return gameObject;
    }

    // setup a gameobject using a list of components and register it
    //
    // param: name - name of game object
    // param: components - components of game object
    // param: parent - parent of game object
    // return: game object that was created
    public GameObject Setup(string name, Component[] components, GameObject parent)
    {
        GameObject gameObject = Setup(name, components);
        parent.AddChild(gameObject);
        return gameObject;
    }

    // setup a gameobject using prefab
    //
    // param: prefab - prefab
    // return: game object that was created
    public GameObject Setup(PrefabInstance prefab)
    {
        GameObject parent = Setup(prefab.Name, prefab.components);

        foreach (PrefabInstance child in prefab.children)
        {
            parent.AddChild(Setup(child));
        }

        return parent;
    }

    // setup a gameobject using prefab
    //
    // param: prefab - prefab
    // param: position - position
    // return: game object that was created
    public GameObject Setup(PrefabInstance prefab, Vector2 position)
    {
        GameObject gameObject = Setup(prefab);
        gameObject.Transform.position = position;
        return gameObject;
    }

    // setup a gameobject using prefab
    //
    // param: prefab - prefab
    // param: position - position
    // param: rotation - rotation
    // return: game object that was created
    public GameObject Setup(PrefabInstance prefab, Vector2 position, float rotation)
    {
        GameObject gameObject = Setup(prefab);
        gameObject.Transform.position = position;
        gameObject.Transform.Rotation = rotation;
        return gameObject;
    }

    // setup a gameobject using prefab
    //
    // param: prefab - prefab
    // param: position - position
    // param: rotation - rotation
    // param: parent - parent game object
    // return: game object that was created
    public GameObject Setup(PrefabInstance prefab, Vector2 position, float rotation, GameObject parent)
    {
        GameObject gameObject = Setup(prefab);
        gameObject.Transform.position = position;
        gameObject.Transform.Rotation = rotation;
        gameObject.SetParent(parent);
        return gameObject;
    }

    // setup a gameobject using prefab
    //
    // param: prefab - prefab
    // param: parent - parent game object
    // return: game object that was created
    public GameObject Setup(PrefabInstance prefab, GameObject parent)
    {
        GameObject gameObject = Setup(prefab);
        gameObject.SetParent(parent);
        return gameObject;
    }

    // instantiate a gameobject using prefab
    //
    // param: prefab - prefab
    // return: game object that was created
    public GameObject Instantiate(PrefabInstance prefab)
    {
        // check if name is unique and register it
        string name = RegisterName(prefab.Name);

        GameObject gameObject = new(name, prefab.components);

        foreach (PrefabInstance child in prefab.children)
        {
            gameObject.AddChild(Instantiate(child));
        }

        gameObject.AwakeBehaviors();
        gameObject.StartBehaviors();
        _toInstantiate.Add(gameObject);

        return gameObject;
    }

    // instantiate a gameobject using prefab
    //
    // param: prefab - prefab
    // param: position - position
    // return: game object that was created
    public GameObject Instantiate(PrefabInstance prefab, Vector2 position)
    {
        GameObject gameObject = Instantiate(prefab);
        gameObject.Transform.position = position;
        return gameObject;
    }

    // instantiate a gameobject using prefab
    //
    // param: prefab - prefab
    // param: position - position
    // param: rotation - rotation
    // return: game object that was created
    public GameObject Instantiate(PrefabInstance prefab, Vector2 position, float rotation)
    {
        GameObject gameObject = Instantiate(prefab);
        gameObject.Transform.position = position;
        gameObject.Transform.Rotation = rotation;
        return gameObject;
    }

    // instantiate a gameobject using prefab
    //
    // param: prefab - prefab
    // param: position - position
    // param: rotation - rotation
    // param: parent - parent game object
    // return: game object that was created
    public GameObject Instantiate(PrefabInstance prefab, Vector2 position, float rotation, GameObject parent)
    {
        GameObject gameObject = Instantiate(prefab);
        gameObject.Transform.position = position;
        gameObject.Transform.Rotation = rotation;
        gameObject.SetParent(parent);
        return gameObject;
    }

    // instantiate a gameobject using prefab
    //
    // param: prefab - prefab

    // param: parent - parent game object
    // return: game object that was created
    public GameObject Instantiate(PrefabInstance prefab, GameObject parent)
    {
        GameObject gameObject = Instantiate(prefab);
        gameObject.SetParent(parent);
        return gameObject;
    }

    // change name of prefab
    //
    // param: name - name to change to
    // param: prefab - prefab to change name
    // return: new prefab
    public static PrefabInstance Rename(string name, PrefabInstance prefab)
    {
        prefab.Name = name;
        return prefab;
    }

    // destroy a game object
    //
    // param: gameObject - game object to destroy
    public void Destroy(GameObject gameObject)
    {
        _toDestroy.Add(gameObject);

        for (int i = gameObject.ChildCount - 1; i >= 0; i--)
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
