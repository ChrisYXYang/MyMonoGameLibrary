using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMonoGameLibrary.Graphics;

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


}
