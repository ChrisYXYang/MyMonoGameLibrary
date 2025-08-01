using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using System.Xml;
using Microsoft.Xna.Framework;
using MyMonoGameLibrary.Graphics;
using System.Diagnostics;

namespace MyMonoGameLibrary.Scenes;

// This class is the base class for components for a game object. Components include Transfrom,
// SpriteManager, BoxCollider, etc.
public abstract class Component
{
    // variables and properties
    public GameObject Parent { get; private set; }
    public Transform Transform { get; private set; }

    private bool _enabled = true;
    public bool Enabled
    {
        get => _enabled;
        set
        {
            if (_enabled && value == false)
            {
                OnDisable();
            }
            else if (!_enabled && value == true)
            {
                OnEnable();
            }

            _enabled = value;
        }
    }
    // initialization for the component
    //
    // param: parent - parent game object
    public void Initialize(GameObject parent)
    {
        Parent = parent;
        Transform = Parent.GetComponent<Transform>();
    }

    // get component of parent game object
    //
    // return: chosen component
    public T GetComponent<T>() where T : Component
    {
        return Parent.GetComponent<T>();
    }

    // executes when component is enabled
    protected virtual void OnEnable()
    {
    }

    // executes when component is disabled
    protected virtual void OnDisable()
    {

    }
}

