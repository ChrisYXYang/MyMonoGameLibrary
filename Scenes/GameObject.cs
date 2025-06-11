using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Xna.Framework;


namespace MyMonoGameLibrary.Scenes;

// This class represents the objects in the game. Each object will have multiple components.
public class GameObject
{
    // variables and properties
    public string Name { get; private set; }
    private Dictionary<string, Component> _components = new Dictionary<string, Component>();
    private Dictionary<string, IGameBehavior> _behaviors = new Dictionary<string, IGameBehavior>();

    // constructor
    //
    // param: name - name of game object
    // param: components - components of game object
    public GameObject(string name, Component[] components)
    {
        Name = name;

        // store every component
        foreach (Component component in components)
        {
            string compName = component.GetType().Name;

            _components.Add(compName, component);

            if (component is IGameBehavior behavior)
                _behaviors.Add(compName, behavior);
        }

        foreach (Component component in _components.Values)
        {
            component.Initialize(this);
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

    // what to do when just collided
    //
    // param: other - other collider
    public void OnCollisionEnter(IRectCollider other)
    {
        foreach (IGameBehavior behavior in _behaviors.Values)
        {
            behavior.OnCollisionEnter(other);
        }
    }

    // what to do when collision exit
    //
    // param: other - other collider
    public void OnCollisionExit(IRectCollider other)
    {
        foreach (IGameBehavior behavior in _behaviors.Values)
        {
            behavior.OnCollisionExit(other);
        }
    }

    // what to do when ongoing collision
    //
    // param: other - other collider
    public void OnCollisionStay(IRectCollider other)
    {
        foreach (IGameBehavior behavior in _behaviors.Values)
        {
            behavior.OnCollisionStay(other);
        }
    }

    // get the collider
    //
    // return: box collider
    public IRectCollider GetCollider()
    {
        return GetComponent<BoxCollider>();
    }

    // get the renderer
    //
    // return: sprite renderer
    public IGameRenderer GetRenderer()
    {
        return GetComponent<SpriteRenderer>();
    }

    // get the animator
    //
    // return: animator
    public IAnimator GetAnimator()
    {
        return GetComponent<Animator>();
    }

    public List<IGameBehavior> GetBehaviors()
    {
        return _behaviors.Values.ToList<IGameBehavior>();
    }

    // 4 testing
    public void PrintComponents()
    {
        foreach (var entry in _components)
        {
            Debug.WriteLine(entry.Key + "\n");
        }
    }
}

