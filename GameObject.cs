using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MyMonoGameLibrary;

// This class represents the objects in the game. Each object will have multiple components.
public class GameObject
{
    // variables and properties
    private Dictionary<string, Component> _components = new Dictionary<string, Component>();

    // add a component to the game object
    //
    // param: component - component to be added
    public void AddComponent<T>(T component) where T: Component
    {
       string compName = typeof(T).Name;
        _components.Add(compName, component);
    }

    // initialize the game object
    public void Initialize()
    {
        foreach (KeyValuePair<string, Component> entry in _components)
        {
            entry.Value.Initialize(this);
        }
    }

    // get chosen component from game object.
    //
    // return: chosen component, null if no component
    public T GetComponent<T>() where T : Component
    {
        string chosenComponent = typeof(T).Name;

        if (_components.ContainsKey(chosenComponent))
        {
            return (T)_components[chosenComponent];
        }

        return null;
    }


    // 4 testing
    public void PrintComponents()
    {
        foreach (KeyValuePair<string, Component> entry in _components)
        {
            Debug.WriteLine(entry.Key + "\n");
        }
    }
}

