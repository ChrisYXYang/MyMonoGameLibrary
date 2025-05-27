using System;

namespace MyMonoGameLibrary;

// This class is the base class for components for a game object. Components include Transfrom,
// SpriteManager, BoxCollider, etc.
public abstract class Component
{
    // variables and properties
    protected GameObject parent;
    
    // initialization for the component
    //
    // param: parent - parent game object
    public virtual void Initialize(GameObject parent)
    {
        this.parent = parent;
    }

    // get component of parent game object
    //
    // return: chosen component
    public T GetComponent<T>() where T : Component
    {
        return parent.GetComponent<T>();
    }
}

