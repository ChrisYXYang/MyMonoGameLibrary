using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using MyMonoGameLibrary.UI;


namespace MyMonoGameLibrary.Scenes;

// This class represents the objects in the game. Each object will have multiple components.
public class GameObject
{
    // variables and properties
    public string Name { get; private set; }
    public GameObject Parent { get; private set; }
    public Transform Transform { get; private set; }
    private readonly Dictionary<string, Component> _components = [];
    private readonly Dictionary<string, BehaviorComponent> _behaviors = [];
    private readonly List<GameObject> _children = [];

    // constructor
    //
    // param: name - name of game object
    // param: components - components of game object
    public GameObject(string name, Component[] components)
    {
        Name = name;

        // store every component and check for only one collider and one renderer
        bool hasRenderer = false;
        bool hasCollider = false;
        foreach (Component component in components)
        {
            string compName = component.GetType().Name;

            _components.Add(compName, component);

            if (component is BehaviorComponent behavior)
                _behaviors.Add(compName, behavior);
            else if (component is ColliderComponent collider)
            {
                if (!hasCollider)
                    hasCollider = true;
                else
                    throw new Exception("multiple colliders!");
            }
            else if (component is RendererComponent renderer)
            {
                if (!hasRenderer)
                    hasRenderer = true;
                else
                    throw new Exception("multiple renderers!");
            } else if (component is Transform transform)
            {
                Transform = transform;
            }
        }

        foreach (Component component in _components.Values)
        {
            if (component is CoreComponent core)
                ((CoreComponent)component).Initialize(this);
            else
                component.Initialize(this);

        }
    }

    // add a child
    //
    // param: child - child to add
    public void AddChild(GameObject child)
    {
        _children.Add(child);
        child.Parent = this;
    }

    // get all children
    //
    // return: array of children
    public GameObject[] GetChildren()
    {
        GameObject[] output = new GameObject[_children.Count];
        _children.CopyTo(output);
        return output;
    }

    // get a child
    //
    // param: index - index of child in list
    // return: chosen child based on index
    public GameObject GetChild(int index)
    {
        return _children[index];
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
    public void OnCollisionEnter(ICollider other)
    {
        foreach (BehaviorComponent behavior in _behaviors.Values)
        {
            behavior.OnCollisionEnter(other);
        }
    }

    // what to do when collision exit
    //
    // param: other - other collider
    public void OnCollisionExit(ICollider other)
    {
        foreach (BehaviorComponent behavior in _behaviors.Values)
        {
            behavior.OnCollisionExit(other);
        }
    }

    // what to do when ongoing collision
    //
    // param: other - other collider
    public void OnCollisionStay(ICollider other)
    {
        foreach (BehaviorComponent behavior in _behaviors.Values)
        {
            behavior.OnCollisionStay(other);
        }
    }

    // get the collider
    //
    // return: box collider
    public ColliderComponent GetCollider()
    {
        ColliderComponent collider = GetComponent<BoxCollider>();

        collider ??= GetComponent<CircleCollider>();
        
        return collider;
    }

    // get the renderer
    //
    // return: the renderer component
    public RendererComponent GetRenderer()
    {
        RendererComponent renderer = GetComponent<SpriteRenderer>();

        renderer ??= GetComponent<TextRenderer>();

        return renderer;
    }

    // get all behavior components
    //
    // return: behavior components
    public List<BehaviorComponent> GetBehaviors()
    {
        return [.. _behaviors.Values];
    }
}

