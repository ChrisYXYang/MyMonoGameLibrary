using System;
using System.Collections.Generic;
using System.ComponentModel;
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

    // can go down platform or not
    public bool DescendPlatform { get; set; } = false;

    // affected by gravity or not
    public bool UseGravity { get; set; }

    // is bottom of rigidbody touching tile collider
    public bool TouchingBottom { get; private set; }

    // collider rigidbody will use
    public BoxCollider Collider { get; private set; }

    // tile colliders intersecting the rigidbody
    private Dictionary<string, TileCollider> _colliders = new(10);

    // position before updating the position
    private Vector2 _previousPosition;
    private float _previousLeft;
    private float _previousRight;
    private float _previousTop;
    private float _previousBottom;

    // how much rigidbody should move in one update
    private Vector2 _movePosition;


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

        if (Collider == null)
            Solid = false;
    }

    // update move position
    //
    // param: x - x to move
    // param: y - y to move
    public void MovePosition(float x, float y)
    {
        _movePosition.X += x;
        _movePosition.Y += y;
    }

    // clear move position
    public void ClearMovePosition()
    {
        _movePosition.X = 0;
        _movePosition.Y = 0;
    }

    // get move position
    //
    // return: _movePosition
    public Vector2 GetMovePosition()
    {
        return _movePosition;
    }

    // update previous position
    public void UpdatePrevPos()
    {
        if (Solid)
        {
            _previousPosition = Transform.position;
            _previousLeft = Collider.Left;
            _previousRight = Collider.Right;
            _previousTop = Collider.Top;
            _previousBottom = Collider.Bottom;
        }
    }

    // correct the position of collider of within a solid tile collider
    public void CorrectPosition()
    {
        if (!Solid)
            return;

        Vector2 currentPos = Transform.position;
        Vector2 newPos = _previousPosition;

        // correct x movement
        float xDelta = _previousPosition.X - currentPos.X;

        // correct left movement
        if (xDelta > 0)
        {
            Transform.position = new Vector2(currentPos.X, _previousPosition.Y);
            foreach (ICollider other in _colliders.Values)
            {
                if (other is TileCollider tile)
                {
                    if (!tile.Platform)
                    {
                        if (Collisions.Intersect(Collider, tile))
                        {
                            Transform.position.X = tile.Right + (Collider.Width * 0.5f);
                            this.XVelocity = 0;
                        }
                    }
                }
            }

            newPos.X = Transform.position.X;
        }

        // correct right movement
        if (xDelta < 0)
        {
            Transform.position = new Vector2(currentPos.X, _previousPosition.Y);
            foreach (ICollider other in _colliders.Values)
            {
                if (other is TileCollider tile)
                {
                    if (!tile.Platform)
                    {
                        if (Collisions.Intersect(Collider, tile))
                        {
                            Transform.position.X = tile.Left - (Collider.Width * 0.5f);
                            this.XVelocity = 0;
                        }
                    }
                }
            }
            newPos.X = Transform.position.X;
        }

        // correct y movement
        float yDelta = _previousPosition.Y - currentPos.Y;

        // correct up movement
        if (yDelta > 0)
        {
            Transform.position = new Vector2(_previousPosition.X, currentPos.Y);
            foreach (ICollider other in _colliders.Values)
            {
                if (other is TileCollider tile)
                {
                    if (!tile.Platform)
                    {
                        if (Collisions.Intersect(Collider, tile))
                        {
                            Transform.position.Y = tile.Bottom + (Collider.Height * 0.5f);
                            this.YVelocity = 0;
                        }
                    }
                }
            }

            newPos.Y = Transform.position.Y;
        }

        // correct down movement (also check if touching bottom)
        TouchingBottom = false;
        if (yDelta < 0)
        {
            Transform.position = new Vector2(_previousPosition.X, currentPos.Y);
            foreach (ICollider other in _colliders.Values)
            {
                if (other is TileCollider tile)
                {
                    if (Collisions.Intersect(Collider, tile))
                    {
                        if (tile.Platform)
                        {
                            if (DescendPlatform)
                                continue;
                            
                            if (_previousBottom > tile.Top)
                                continue;
                        }
                        
                        Transform.position.Y = tile.Top - (Collider.Height * 0.5f);
                        this.YVelocity = 0;
                        TouchingBottom = true;
                    }
                }
            }

            newPos.Y = Transform.position.Y;
        }

        // move transform to new position
        Transform.position = newPos;

        foreach (ICollider other in _colliders.Values)
        {
            if (other is TileCollider tile)
            {
                if (!tile.Platform)
                {
                    if (Collisions.Intersect(Collider, tile))
                    {
                        Transform.position = new Vector2(newPos.X, _previousPosition.Y);
                    }
                }
            }
        }
    }

    // add a tile collider to collisions
    //
    // param: other - collider to add
    public void AddCollision(TileCollider other)
    {
        if (!_colliders.ContainsKey(other.GetName()))
            _colliders.Add(other.GetName(), other);
    }

    // remove a tile collider from collisions
    //
    // param: other - collider to remove
    public void RemoveCollision(TileCollider other)
    {
        if (_colliders.ContainsKey(other.GetName()))
            _colliders.Remove(other.GetName());
    }
}
