using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MyMonoGameLibrary.Tilemap;

namespace MyMonoGameLibrary.Scenes;

// box collider which can be affected by gravity and has wall collision (simple physics system)
public class Rigidbody : CoreComponent
{
    // velocities
    public float XVelocity { get; set; } = 0;
    public float YVelocity { get; set; } = 0;
    // can collide with walls or not
    public bool Solid { get; set; }
    // affected by gravity or not
    public bool UseGravity { get; set; }
    public bool TouchingBottom { get; private set; }
    public Transform ParentTransform { get; private set; }
    public BoxCollider Collider { get; private set; }
    public Vector2 movePosition;

    private Dictionary<string, TileCollider> _colliders = new(10);
    private Vector2 _previousPosition;

    // constructor
    //
    // param: graivty - use gravity or not
    // param: solid - solid rigidbody or not
    public Rigidbody(bool gravity, bool solid)
    {
        UseGravity = gravity;
        Solid = solid;
    }

    // initialize
    //
    // param: parent - parent game object
    public override void Initialize(GameObject parent)
    {
        base.Initialize(parent);
        Collider = parent.GetComponent<BoxCollider>();
        ParentTransform = parent.GetComponent<Transform>();

        if (Collider == null)
            Solid = false;
    }

    // update previous position
    public void UpdatePrevPos()
    {
        if (Solid)
            _previousPosition = Collider.ParentTransform.position;
    }

    // correct the position of collider of within a solid tile collider
    public void CorrectPosition()
    {
        if (!Solid)
            return;

        Vector2 currentPos = Collider.ParentTransform.position;
        Vector2 newPos = _previousPosition;

        // correct x movement
        float xDelta = _previousPosition.X - currentPos.X;

        // correct left movement
        if (xDelta > 0)
        {
            Collider.ParentTransform.position = new Vector2(currentPos.X, _previousPosition.Y);
            foreach (ICollider other in _colliders.Values)
            {
                if (other is TileCollider tile)
                {
                    if (Collisions.Intersect(Collider, tile))
                    {
                        Collider.ParentTransform.position.X = tile.Right + (Collider.Width * 0.5f);
                        this.XVelocity = 0;
                    }
                }
            }

            newPos.X = Collider.ParentTransform.position.X;
        }

        // correct right movement
        if (xDelta < 0)
        {
            Collider.ParentTransform.position = new Vector2(currentPos.X, _previousPosition.Y);
            foreach (ICollider other in _colliders.Values)
            {
                if (other is TileCollider tile)
                {
                    if (Collisions.Intersect(Collider, tile))
                    {
                        Collider.ParentTransform.position.X = tile.Left - (Collider.Width * 0.5f);
                        this.XVelocity = 0;
                    }
                }
            }
            newPos.X = Collider.ParentTransform.position.X;
        }

        // correct y movement
        float yDelta = _previousPosition.Y - currentPos.Y;

        // correct up movement
        if (yDelta > 0)
        {
            Collider.ParentTransform.position = new Vector2(_previousPosition.X, currentPos.Y);
            foreach (ICollider other in _colliders.Values)
            {
                if (other is TileCollider tile)
                {
                    if (Collisions.Intersect(Collider, tile))
                    {
                        Collider.ParentTransform.position.Y = tile.Bottom + (Collider.Height * 0.5f);
                        this.YVelocity = 0;
                    }
                }
            }

            newPos.Y = Collider.ParentTransform.position.Y;
        }

        // correct down movement
        TouchingBottom = false;
        if (yDelta < 0)
        {
            Collider.ParentTransform.position = new Vector2(_previousPosition.X, currentPos.Y);
            foreach (ICollider other in _colliders.Values)
            {
                if (other is TileCollider tile)
                {
                    if (Collisions.Intersect(Collider, tile))
                    {
                        Collider.ParentTransform.position.Y = tile.Top - (Collider.Height * 0.5f);
                        this.YVelocity = 0;
                        TouchingBottom = true;
                    }
                }
            }

            newPos.Y = Collider.ParentTransform.position.Y;
        }

        // move transform to new position
        Collider.ParentTransform.position = newPos;
    }

    public void AddCollision(TileCollider other)
    {
        if (!_colliders.ContainsKey(other.GetName()))
            _colliders.Add(other.GetName(), other);
    }

    public void RemoveCollision(TileCollider other)
    {
        if (_colliders.ContainsKey(other.GetName()))
            _colliders.Remove(other.GetName());
    }
}
