using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MyMonoGameLibrary.Scenes;

// circle collider component for game object
public class CircleCollider : ColliderComponent, ICircleCollider
{
    // radius of collider
    public float Radius { get; private set; }
    public float Diameter { get; private set; }

    // constructor
    //
    // param: diameter - diameter of collider
    // param: solid - solid or not
    public CircleCollider(int diameter, bool solid)
    {
        Diameter = (float)diameter / Camera.SpritePixelsPerUnit;
        Radius = Diameter / 2;
        Offset = Vector2.Zero;
        Solid = solid;
    }

    // constructor
    //
    // param: diameter - diameter of collider
    // param: xOffset - x offset
    // param: yOffset - y offset
    // param: solid - solid or not
    public CircleCollider(int diameter, float xOffset, float yOffset, bool solid)
    {
        Diameter = (float)diameter / Camera.SpritePixelsPerUnit;
        Radius = Diameter / 2;
        Offset = new Vector2(xOffset, yOffset) / Camera.SpritePixelsPerUnit;
        Solid = solid;
    }
}
