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
    public GameObject()
    {
        _components = new Dictionary<string, Component>();
    }

    public void AddComponent<T>(T component) where T: Component
    {
       string compName = typeof(T).Name;
        _components.Add(compName, component);
    }

    public void InitializeComponents()
    {
        foreach (KeyValuePair<string, Component> entry in _components)
        {
            entry.Value.Initialize();
        }
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

