using System;
using Microsoft.Xna.Framework;

namespace MyMonoGameLibrary.Scenes;

// this class represents a prefab object returned from prefab function. This object will be
// instantiated/setup and is not meant to be referenced in any scripts.
public class PrefabInstance
{
    public string Name { get; set; }
    public readonly Component[] components;
    public readonly (PrefabInstance, Vector2)[] children;

    // constructor
    //
    // param: name - name
    // param: components - components
    public PrefabInstance(string name, Component[] components)
    {
        Name = name;
        this.components = components;
        this.children = [];
    }

    // constructor
    //
    // param: name - name
    // param: components - components
    // param: children - children prefabs
    public PrefabInstance(string name, Component[] components, (PrefabInstance, Vector2)[] children)
    {
        Name = name;
        this.components = components;
        this.children = children;
    }
}
