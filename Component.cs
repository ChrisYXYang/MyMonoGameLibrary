using System;

namespace MyMonoGameLibrary;

// Summary:
//      This class is the base class for components for a game object.
//      Components include Transfrom, SpriteManager, BoxCollider, etc.
public abstract class Component
{
    // variables and properties
    protected GameObject parent;

    public void Initialize(GameObject parent)
    {
        this.parent = parent;
    }
}

