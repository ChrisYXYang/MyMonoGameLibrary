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
    public float Width { get; set; } = 0;
    public float Height { get; set; } = 0;
    public float Left => Center.X - (Width * 0.5f);
    public float Right => Center.X + (Width * 0.5f);
    public float Top => Center.Y - (Height * 0.5f);
    public float Bottom => Center.Y + (Height * 0.5f);

    // empty constructor
    public BoxCollider()
    {
    }

    // constructor
    //
    // param: width - width of collider
    // param: height - height of collider
    public BoxCollider(float width, float height)
    {
        // set the properties
        Width = width;
        Height = height;
        Offset = Vector2.Zero;
    }

    // constructor
    //
    // param: width - width of collider
    // param: height - height of collider
    // param: xOffset - x offset of collider
    // param: yOffset - y offset of collider
    public BoxCollider(float width, float height, float xOffset, float yOffset)
    {
        // set the properties
        Width = width;
        Height = height;
        Offset = new Vector2(xOffset, yOffset);
    }

    // constructor
    //
    // param: layer - the layer
    public BoxCollider(string layer) : base(layer)
    {
    }

    // constructor
    //
    // param: width - width of collider
    // param: height - height of collider
    // param: layer - the layer
    public BoxCollider(float width, float height, string layer) : base(layer)
    {
        // set the properties
        Width = width;
        Height = height;
        Offset = Vector2.Zero;
    }

    // constructor
    //
    // param: width - width of collider
    // param: height - height of collider
    // param: xOffset - x offset of collider
    // param: yOffset - y offset of collider
    // param: layer - the layer
    public BoxCollider(float width, float height, float xOffset, float yOffset, string layer) : base(layer)
    {
        // set the properties
        Width = width;
        Height = height;
        Offset = new Vector2(xOffset, yOffset);
    }


}
