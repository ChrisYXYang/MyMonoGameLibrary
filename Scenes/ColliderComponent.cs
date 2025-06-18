using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MyMonoGameLibrary.Tilemap;

namespace MyMonoGameLibrary.Scenes;

// base class for a collider component. Contains implementations of ICollider for game objects
public abstract class ColliderComponent : CoreComponent, ICollider
{
    // set of names of objects colliding with this collider
    protected Dictionary<string, ICollider> Colliders { get; private set; } = [];
    protected Transform ParentTransform { get; private set; }
    public Vector2 Offset { get; protected set; }
    // center of collider
    public Vector2 Center => ParentTransform.position + Offset;
    public bool Solid { get; set; }
    public Vector2 PreviousPosition { get; private set; }

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

    // update previous position
    public void UpdatePrevPos()
    {
        if (Solid)
            PreviousPosition = ParentTransform.position;
    }

    // correct the position of collider of within a solid tile collider
    public void CorrectPosition()
    {
        if (Solid)
        {
            Vector2 currentPos = ParentTransform.position;

            // check whether moving x or y will cause collision. If so, then don't move x or y
            Vector2 newPos = PreviousPosition;

            // check if moving x will collide with any tile colliders
            ParentTransform.position = new Vector2(currentPos.X, PreviousPosition.Y);

            bool xCollides = false;
            foreach (ICollider other in Colliders.Values)
            {
                if (other is TileCollider tile)
                {
                    if (Collisions.Intersect(this, other))
                        xCollides = true;
                }
            }

            if (!xCollides)
            {
                newPos.X = currentPos.X;
            }

            // check if moving y will collide with any tile colliders
            ParentTransform.position = new Vector2(PreviousPosition.X, currentPos.Y);

            bool yCollides = false;
            foreach (ICollider other in Colliders.Values)
            {
                if (other is TileCollider tile)
                {
                    if (Collisions.Intersect(this, other))
                        yCollides = true;
                }
            }

            if (!yCollides)
            {
                newPos.Y = currentPos.Y;
            }

            // move transform to new position
            ParentTransform.position = newPos;
        }  
    }
}
