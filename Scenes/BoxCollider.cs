using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyMonoGameLibrary.Tools;

namespace MyMonoGameLibrary.Scenes;

// component for rectangle collider for gameobject
public class BoxCollider : ColliderComponent, IAABBCollider
{
    // variables and properties
    public float Width { get; private set; }
    public float Height { get; private set; }
    public float Left => ParentTransform.position.X + Offset.X - (Width * 0.5f);
    public float Right => ParentTransform.position.X + Offset.X + (Width * 0.5f);
    public float Top => ParentTransform.position.Y + Offset.Y - (Height * 0.5f);
    public float Bottom => ParentTransform.position.Y + Offset.Y + (Height * 0.5f);

    
    // constructor
    //
    // param: width - width of collider
    // param: height - height of collider
    public BoxCollider(int width, int height)
    {
        // set the properties
        Width = (float)width / Camera.SpritePixelsPerUnit;
        Height = (float)height / Camera.SpritePixelsPerUnit;
        Offset = Vector2.Zero;
    }

    // constructor
    //
    // param: width - width of collider
    // param: height - height of collider
    // param: xOffset - x offset of collider
    // param: yOffset - y offset of collider
    public BoxCollider(int width, int height, float xOffset, float yOffset)
    {
        // set the properties
        Width = (float)width / Camera.SpritePixelsPerUnit;
        Height = (float)height / Camera.SpritePixelsPerUnit;
        Offset = new Vector2(xOffset, yOffset) / Camera.SpritePixelsPerUnit;
    }
}
