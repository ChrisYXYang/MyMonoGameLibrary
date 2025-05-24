using System;
using System.Collections.Generic;

namespace MyMonoGameLibrary;

public class GameObject
{
    // vars
    private Dictionary<string, Component> _components;

    public GameObject(Dictionary<string, Component> components)
    {
        this._components = components;
    }

    // get chosen attribute from game object
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

