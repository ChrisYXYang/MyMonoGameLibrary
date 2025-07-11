using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Xna.Framework;
using MyMonoGameLibrary.Graphics;
using MyMonoGameLibrary.Tilemap;
using MyMonoGameLibrary.UI;

namespace MyMonoGameLibrary.Scenes;

// tools for use in a scene
public static class SceneTools
{
    private static Scene _activeScene;

    // get sprite library
    public static Library SceneLibrary { get => _activeScene.SceneLibrary; }

    // change the active scene
    //
    // param: scene - scene to make active
    public static void ChangeActive(Scene scene)
    {
        _activeScene = scene;
    }

    // get a game object
    //
    // param: name - name of game object
    // return: requested gameobject or throws exception
    public static GameObject GetGameObject(string name)
    {
        return _activeScene.GetGameObject(name);
    }

    // get all game draw objects
    //
    // return: list of all game draw objects
    public static List<GameObject> GetGameDrawObjects()
    {
        return _activeScene.GetGameDrawObjects();
    }

    // get all ui draw objects
    //
    // return: list of all ui draw objects
    public static List<GameObject> GetUIDrawObjects()
    {
        return _activeScene.GetUIDrawObjects();
    }

    // get all game objects
    //
    // return: list of all game objects in active scene
    public static List<GameObject> GetGameObjects()
    {
        return _activeScene.GetGameObjects();
    }

    // get the tilemap
    //
    // return: the tilemap of scene
    public static TileMap GetTilemap()
    {
        return _activeScene.Tilemap;
    }

    // instantiate a gameobject using prefab
    //
    // param: prefab - prefab
    // return: game object that was created
    public static GameObject Instantiate((string, Component[]) prefab)
    {
        return _activeScene.Instantiate(prefab);
    }

    // instantiate a gameobject using prefab
    //
    // param: prefab - prefab
    // param: position - position
    // return: game object that was created
    public static GameObject Instantiate((string, Component[]) prefab, Vector2 position)
    {
        return _activeScene.Instantiate(prefab, position);
    }

    // instantiate a gameobject using prefab
    //
    // param: prefab - prefab
    // param: position - position
    // param: rotation - rotation
    // return: game object that was created
    public static GameObject Instantiate((string, Component[]) prefab, Vector2 position, float rotation)
    {
        return _activeScene.Instantiate(prefab, position, rotation);
    }

    // instantiate a gameobject using prefab
    //
    // param: prefab - prefab
    // param: position - position
    // param: rotation - rotation
    // param: parent - parent game object
    // return: game object that was created
    public static GameObject Instantiate((string, Component[]) prefab, Vector2 position, float rotation, GameObject parent)
    {
        return _activeScene.Instantiate(prefab, position, rotation, parent);
    }

    // instantiate a gameobject using prefab
    //
    // param: prefab - prefab
    // param: parent - parent game object
    // return: game object that was created
    public static GameObject Instantiate((string, Component[]) prefab, GameObject parent)
    {
        return _activeScene.Instantiate(prefab, parent);
    }

    // destroy a game object
    //
    // param: gameObject - game object to destroy
    public static void Destroy(GameObject gameObject)
    {
        _activeScene.Destroy(gameObject);
    }
}
