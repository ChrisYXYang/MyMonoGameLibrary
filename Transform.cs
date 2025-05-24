using System;
using Microsoft.Xna.Framework;

namespace MyMonoGameLibrary;

public class Transform : Component
{
    // properties
    public Vector2 Position { get; set; }
    public float Scale { get; set; }
    public float Rotation { get; set; }

    public Transform(Vector2 position, float scale, float rotation)
    {
        this.Position = position;
        this.Scale = scale;
        this.Rotation = rotation;
    }

}

