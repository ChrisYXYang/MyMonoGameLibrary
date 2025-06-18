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
    public float Left => Center.X - (Width * 0.5f);
    public float Right => Center.X + (Width * 0.5f);
    public float Top => Center.Y - (Height * 0.5f);
    public float Bottom => Center.Y + (Height * 0.5f);

    
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
}
