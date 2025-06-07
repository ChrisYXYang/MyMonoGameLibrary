using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;


namespace MyMonoGameLibrary.Scene;

// This class represents the objects in the game. Each object will have multiple components.
public class GameObject : ICollidable, IRenderable, IAnimatable
{
    // variables and properties
    public string Name { get; private set; }
    private Dictionary<string, Component> _components = new Dictionary<string, Component>();
    private Dictionary<string, IBehavior> _behaviors = new Dictionary<string, IBehavior>();

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

            if (component is IBehavior behavior)
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
        foreach (IBehavior behavior in _behaviors.Values)
        {
            behavior.OnCollisionEnter(other);
        }
    }

    // what to do when collision exit
    //
    // param: other - other collider
    public void OnCollisionExit(IRectCollider other)
    {
        foreach (IBehavior behavior in _behaviors.Values)
        {
            behavior.OnCollisionExit(other);
        }
    }

    // what to do when ongoing collision
    //
    // param: other - other collider
    public void OnCollisionStay(IRectCollider other)
    {
        foreach (IBehavior behavior in _behaviors.Values)
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
    public IRenderer GetRenderer()
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

    // 4 testing
    public void PrintComponents()
    {
        foreach (var entry in _components)
        {
            Debug.WriteLine(entry.Key + "\n");
        }
    }
}

