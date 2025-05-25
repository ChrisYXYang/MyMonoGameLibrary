using System;

namespace MyMonoGameLibrary;

// Summary:
//      This class is the base class for components for a game object. Components include Transfrom,
//      SpriteManager, BoxCollider, etc.
public abstract class Component
{
    // variables and properties
    protected GameObject parent;
    
    // constructor
    public Component(GameObject parent)
    {
        this.parent = parent;
    }

    // initialization for the component
    public virtual void Initialize()
    {
        
    }

    // get component
    public T GetComponent<T>() where T : Component
    {
        return parent.GetComponent<T>();
    }
}

