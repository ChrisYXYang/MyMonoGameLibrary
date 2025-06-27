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
    public ColliderComponent Collider { get; private set; }
    public RendererComponent Renderer { get; private set; }
    public Animator Animator { get; private set; }
    public Rigidbody Rigidbody { get; private set; }
    public int ChildCount { get => _children.Count; }

    private readonly Dictionary<string, Component> _components = [];
    private readonly List<BehaviorComponent> _behaviors = [];
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
                _behaviors.Add(behavior);
            else if (component is ColliderComponent collider)
            {
                if (!hasCollider)
                {
                    hasCollider = true;
                    Collider = collider;
                }
                else
                    throw new Exception("multiple colliders!");
            }
            else if (component is RendererComponent renderer)
            {
                if (!hasRenderer)
                {
                    hasRenderer = true;
                    Renderer = renderer;
                }
                else
                    throw new Exception("multiple renderers!");
            } else if (component is Transform transform)
            {
                Transform = transform;
            } else if (component is Animator animator)
            {
                Animator = animator;
            } else if (component is Rigidbody rb)
            {
                Rigidbody = rb;
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
        child.Parent?.RemoveChild(child);
        child.Parent = this;
    }

    // set parent
    //
    // param: parent - parent game object
    public void SetParent(GameObject parent)
    {
        if (parent == null)
        {
            this.Parent.RemoveChild(this);
            return;
        }
        
        parent.AddChild(this);
    }

    // remove a child
    //
    // param: child - child to remove
    public void RemoveChild(GameObject child)
    {
        child.Parent = null;
        _children.Remove(child);
    }

    // get a child by index
    //
    // param: index - index of child
    // return: chosen child based on name
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
        foreach (BehaviorComponent behavior in _behaviors)
        {
            behavior.OnCollisionEnter(other);
        }
    }

    // what to do when collision exit
    //
    // param: other - other collider
    public void OnCollisionExit(ICollider other)
    {
        foreach (BehaviorComponent behavior in _behaviors)
        {
            behavior.OnCollisionExit(other);
        }
    }

    // what to do when ongoing collision
    //
    // param: other - other collider
    public void OnCollisionStay(ICollider other)
    {
        foreach (BehaviorComponent behavior in _behaviors)
        {
            behavior.OnCollisionStay(other);
        }
    }

    // start all behaviors
    public void StartBehaviors()
    {
        foreach (BehaviorComponent behavior in _behaviors)
        {
            behavior.Start();
        }
    }

    // update all behaviors
    //
    // param: gameTime - the game time
    public void UpdateBehaviors(GameTime gameTime)
    {
        foreach (BehaviorComponent behavior in _behaviors)
        {
            behavior.Update(gameTime);
        }
    }

    // late update all behaviors
    //
    // param: gameTime - the game time
    public void LateUpdateBehaviors(GameTime gameTime)
    {
        foreach (BehaviorComponent behavior in _behaviors)
        {
            behavior.LateUpdate(gameTime);
        }
    }
}

