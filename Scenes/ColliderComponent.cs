using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using MyMonoGameLibrary.Tilemap;

namespace MyMonoGameLibrary.Scenes;

// base class for a collider component. Contains implementations of ICollider for game objects
public abstract class ColliderComponent : CoreComponent, ICollider
{
    public string Layer { get; set; } = "default";
    // set of names of objects colliding with this collider
    protected Dictionary<string, ICollider> Colliders { get; private set; } = [];
    public Vector2 Offset { get; set; }
    // center of collider
    public Vector2 Center => Transform.TruePosition + Offset;

    // empty constructor
    public ColliderComponent() 
    {
    }

    // constructor
    //
    // param: layer - the layer
    public ColliderComponent(string layer)
    {
        Layer = layer;
    }


    // get name of parent
    //
    // return: name of parent
    public virtual string GetName()
    {
        return Parent.Name;
    }

    // what to do when collision happens
    //
    // param: other - other collider
    public virtual void Colliding(ICollider other)
    {
        if (!Colliders.ContainsKey(other.GetName()))
        {
            Parent.OnCollisionEnter(other);
            Colliders.Add(other.GetName(), other);
        }
        else
        {
            Parent.OnCollisionStay(other);
        }
    }

    // what to do when not colliding
    //
    // param: other - other collider
    public virtual void NotColliding(ICollider other)
    {
        if (Colliders.ContainsKey(other.GetName()))
        {
            Colliders.Remove(other.GetName());
            Parent.OnCollisionExit(other);
        }
    }

    // get colliders colliding with it
    //
    // return: list of colliders currently colliding with this one
    public List<ICollider> GetCollisions()
    {
        return [.. Colliders.Values];
    }
}
