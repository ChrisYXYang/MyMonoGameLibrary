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
    public string Layer { get; set; }
    // set of names of objects colliding with this collider
    protected Dictionary<string, ICollider> Colliders { get; private set; } = [];
    public Transform ParentTransform { get; private set; }
    public Vector2 Offset { get; protected set; }
    // center of collider
    public Vector2 Center => ParentTransform.position + Offset;

    // initialize
    //
    // param: parent - parent game object
    public override void Initialize(GameObject parent)
    {
        base.Initialize(parent);
        ParentTransform = GetComponent<Transform>();
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

    // get colliders colliding with collider
    public List<ICollider> GetColliders()
    {
        return [.. Colliders.Values];
    }
}
