using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyMonoGameLibrary.Graphics;
using MyMonoGameLibrary.Tools;
using MyMonoGameLibrary.Tilemap;
using System.Collections.Generic;
using MyMonoGameLibrary.Scenes;
using System.Reflection.Metadata.Ecma335;
using MyMonoGameLibrary.UI;

namespace MyMonoGameLibrary.Tools;

// Extension of the Core class that gives debugging tools such as visualizing origin and colliders
public class Debugging
{
    // print the scene game object heirarchy
    public static void PrintScene()
    {
        List<GameObject> gameObjects = SceneTools.GetGameObjects();

        // iterate through every game object. If game object has no parent, then print the
        // game object and its heirarchy
        foreach(GameObject gameObject in gameObjects)
        {
            if (gameObject.Parent == null)
            {
                Stack<(string, GameObject)> printStack = [];
                printStack.Push(("- ", gameObject));

                while (printStack.Count > 0)
                {
                    (string, GameObject) currentPrint = printStack.Pop();
                    Debug.WriteLine(currentPrint.Item1 + currentPrint.Item2.Name);

                    for (int i = currentPrint.Item2.ChildCount - 1; i >= 0; i--)
                    {
                        printStack.Push(("  " + currentPrint.Item1, currentPrint.Item2.GetChild(i)));
                    }
                }
            }
        }
        Debug.WriteLine("");
    }
}
