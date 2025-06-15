using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MyMonoGameLibrary.Scenes;

// base class for a collider component. Contains implementations of ICollider for game objects
public abstract class ColliderComponent : CoreComponent, ICollider
{
    // set of names of objects colliding with this collider
    protected HashSet<string> Collisions { get; private set; } = [];
    protected Transform ParentTransform { get; private set; }
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
        if (!Collisions.Contains(other.GetName()))
        {
            Parent.OnCollisionEnter(other);
            Collisions.Add(other.GetName());
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
        if (Collisions.Contains(other.GetName()))
        {
            Collisions.Remove(other.GetName());
            Parent.OnCollisionExit(other);
        }
    }
}
