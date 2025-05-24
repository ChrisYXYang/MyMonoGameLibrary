using System;

namespace MyMonoGameLibrary;

public abstract class Component
{
    protected GameObject parent;

    public void Initialize(GameObject parent)
    {
        this.parent = parent;
    }
}

