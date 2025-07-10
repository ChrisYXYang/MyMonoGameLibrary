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

    // get all game objects
    //
    // return: list of all game objects in active scene
    public static List<GameObject> GetGameObjects()
    {
        return _activeScene.GetGameObjects();
    }

    // get ui element
    //
    // param: name - name of ui element
    // return: requested ui element
    public static BaseUI GetElement(string name)
    {
        return _activeScene.GetElement(name);
    }

    // get text ui element
    //
    // param: name - name of text ui element
    // return: requested text ui element
    public static TextUI GetText(string name)
    {
        return _activeScene.GetText(name);
    }

    // get sprite ui element
    //
    // param: name - name of sprite ui element
    // return: requested sprite ui element
    public static SpriteUI GetSprite(string name)
    {
        return _activeScene.GetSprite(name);
    }

    // get the canvas
    //
    // return: the canvas of the scene
    public static Canvas GetCanvas()
    {
        return _activeScene.Canvas;
    }

    // get the tilemap
    //
    // return: the tilemap of scene
    public static TileMap GetTilemap()
    {
        return _activeScene.Tilemap;
    }

    // instantiate a gameobject using a list of components
    //
    // param: name - name of game object
    // param: components - components of game object
    // return: game object that was created
    public static GameObject Instantiate(string name, Component[] components)
    {
        return _activeScene.Instantiate(name, components);
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

    // instantiate an ui element with canvas parent
    //
    // param: name - name of ui element
    // param: element - the ui element
    public static void Instantiate(string name, BaseUI element)
    {
        _activeScene.Instantiate(name, element);
    }

    // instantiate an ui element with another ui element as parent
    //
    // param: name - name of ui element
    // param: element - the ui element
    // param: parent - parent element
    public static void Instantiate(string name, BaseUI element, BaseUI parent)
    {
        _activeScene.Instantiate(name, element, parent);
    }

    // destroy a game object
    //
    // param: gameObject - game object to destroy
    public static void Destroy(GameObject gameObject)
    {
        _activeScene.Destroy(gameObject);
    }

    // destroy an ui element
    //
    // param: element - UI element to destroy
    public static void Destroy(BaseUI element)
    {
        _activeScene.Destroy(element);
    }
}
