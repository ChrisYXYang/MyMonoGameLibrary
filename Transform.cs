using System;
using Microsoft.Xna.Framework;

namespace MyMonoGameLibrary;

// Summary:
//      component for the Transform of gameobject
public class Transform : Component
{
    // variables and properties
    public Vector2 Position { get; set; }
    public float Scale { get; set; }
    public float Rotation { get; set; }

    // constructor
    public Transform(Vector2 position, float scale, float rotation)
    {
        this.Position = position;
        this.Scale = scale;
        this.Rotation = rotation;
    }

}

