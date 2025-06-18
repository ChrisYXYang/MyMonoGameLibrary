using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MyMonoGameLibrary.Tilemap;

namespace MyMonoGameLibrary.Scenes;

// box collider which can be affected by gravity and has wall collision (simple physics system)
public class Rigidbody : BoxCollider
{
    // affected by gravity or not
    public bool UseGravity { get; set; }

    private Vector2 _previousPosition;

    // constructor
    //
    // param: width - width of collider
    // param: height - height of collider
    public Rigidbody(int width, int height) : base(width, height)
    {
    }

    // constructor
    //
    // param: width - width of collider
    // param: height - height of collider
    // param: xOffset - x offset of collider
    // param: yOffset - y offset of collider
    public Rigidbody(int width, int height, float xOffset, float yOffset) : base(width, height, xOffset, yOffset)
    {
    }

    // update previous position
    public void UpdatePrevPos()
    {
        _previousPosition = ParentTransform.position;
    }

    // correct the position of collider of within a solid tile collider
    public void CorrectPosition()
    {
        Vector2 currentPos = ParentTransform.position;
        Vector2 newPos = _previousPosition;

        // correct x movement
        float xDelta = _previousPosition.X - currentPos.X;

        // correct left movement
        if (xDelta > 0)
        {
            ParentTransform.position = new Vector2(currentPos.X, _previousPosition.Y);
            foreach (ICollider other in Colliders.Values)
            {
                if (other is TileCollider tile)
                {
                    if (Collisions.Intersect(this, tile))
                    {
                        ParentTransform.position.X = tile.Right + (this.Width * 0.5f);
                    }
                }
            }

            newPos.X = ParentTransform.position.X;
        }

        // correct right movement
        if (xDelta < 0)
        {
            ParentTransform.position = new Vector2(currentPos.X, _previousPosition.Y);
            foreach (ICollider other in Colliders.Values)
            {
                if (other is TileCollider tile)
                {
                    if (Collisions.Intersect(this, tile))
                    {
                        ParentTransform.position.X = tile.Left - (this.Width * 0.5f);
                    }
                }
            }
            newPos.X = ParentTransform.position.X;
        }

        // correct y movement
        float yDelta = _previousPosition.Y - currentPos.Y;

        // correct up movement
        if (yDelta > 0)
        {
            ParentTransform.position = new Vector2(_previousPosition.X, currentPos.Y);
            foreach (ICollider other in Colliders.Values)
            {
                if (other is TileCollider tile)
                {
                    if (Collisions.Intersect(this, tile))
                    {
                        ParentTransform.position.Y = tile.Bottom + (this.Height * 0.5f);
                    }
                }
            }

            newPos.Y = ParentTransform.position.Y;
        }

        // correct down movement
        if (yDelta < 0)
        {
            ParentTransform.position = new Vector2(_previousPosition.X, currentPos.Y);
            foreach (ICollider other in Colliders.Values)
            {
                if (other is TileCollider tile)
                {
                    if (Collisions.Intersect(this, tile))
                    {
                        ParentTransform.position.Y = tile.Top - (this.Height * 0.5f);
                    }
                }
            }

            newPos.Y = ParentTransform.position.Y;
        }

        // move transform to new position
        ParentTransform.position = newPos;
    }
}
