using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MyMonoGameLibrary.Scenes;

// circle collider component for game object
public class CircleCollider : ColliderComponent, ICircleCollider
{
    // radius of collider
    public float Radius { get; set; }
    public float Diameter { get; set; }

    // constructor
    //
    // param: diameter - diameter of collider
    // param: layer - the layer
    public CircleCollider(int diameter, string layer) : base(layer)
    {
        Diameter = (float)diameter / Camera.SpritePixelsPerUnit;
        Radius = Diameter / 2;
        Offset = Vector2.Zero;
    }

    // constructor
    //
    // param: diameter - diameter of collider
    // param: xOffset - x offset
    // param: yOffset - y offset
    // param: layer - the layer
    public CircleCollider(int diameter, float xOffset, float yOffset, string layer) : base(layer)
    {
        Diameter = (float)diameter / Camera.SpritePixelsPerUnit;
        Radius = Diameter / 2;
        Offset = new Vector2(xOffset, yOffset) / Camera.SpritePixelsPerUnit;
    }
}
