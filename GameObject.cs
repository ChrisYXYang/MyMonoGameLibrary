using System;
using System.Collections.Generic;

namespace MyMonoGameLibrary;

// summary:
//      The class for the objects in the game. Each object will have multiple components.
public class GameObject
{
    // variables and properties
    private Dictionary<string, Component> _components;

    // constructor
    public GameObject(Dictionary<string, Component> components)
    {
        this._components = components;
    }

    // Summary:
    //      get chosen attribute from game object.
    public T GetComponent<T>() where T : Component
    {
        string chosenComponent = typeof(T).Name;

        if (_components.ContainsKey(chosenComponent))
        {
            return (T)_components[chosenComponent];
        }

        return null;
    }
}

