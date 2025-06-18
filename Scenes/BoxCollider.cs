using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyMonoGameLibrary.Tilemap;
using MyMonoGameLibrary.Tools;

namespace MyMonoGameLibrary.Scenes;

// component for rectangle collider for gameobject
public class BoxCollider : ColliderComponent, IAABBCollider
{
    // variables and properties
    public bool Solid { get; set; }
    public float Width { get; private set; }
    public float Height { get; private set; }
    public float Left => Center.X - (Width * 0.5f);
    public float Right => Center.X + (Width * 0.5f);
    public float Top => Center.Y - (Height * 0.5f);
    public float Bottom => Center.Y + (Height * 0.5f);
    
    private Vector2 _previousPosition;


    // constructor
    //
    // param: width - width of collider
    // param: height - height of collider
    // param: solid - solid or not
    public BoxCollider(int width, int height, bool solid)
    {
        // set the properties
        Width = (float)width / Camera.SpritePixelsPerUnit;
        Height = (float)height / Camera.SpritePixelsPerUnit;
        Offset = Vector2.Zero;
        Solid = solid;
    }

    // constructor
    //
    // param: width - width of collider
    // param: height - height of collider
    // param: xOffset - x offset of collider
    // param: yOffset - y offset of collider
    // param: solid - solid or not
    public BoxCollider(int width, int height, float xOffset, float yOffset, bool solid)
    {
        // set the properties
        Width = (float)width / Camera.SpritePixelsPerUnit;
        Height = (float)height / Camera.SpritePixelsPerUnit;
        Offset = new Vector2(xOffset, yOffset) / Camera.SpritePixelsPerUnit;
        Solid = solid;
    }
    // update previous position
    public void UpdatePrevPos()
    {
        if (Solid)
            _previousPosition = ParentTransform.position;
    }

    // correct the position of collider of within a solid tile collider
    public void CorrectPosition()
    {
        if (Solid)
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
}
