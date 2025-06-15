using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MyMonoGameLibrary.Tools;

namespace MyMonoGameLibrary.Scenes;

// component for the transform of gameobject, which is position, scale, rotation
public class Transform : CoreComponent
{
    // variables and properties
    public Vector2 position = Vector2.Zero;
    public Vector2 Scale { get; set; } = Vector2.One;
    public float Rotation { get; set; } = 0;

    // default constructor
    public Transform()
    {

    }

    // constructor
    //
    // param: position - position
    public Transform(Vector2 position)
    {
        this.position = position;
    }

    // constructor
    //
    // param: position - position
    // param: scale - scale
    // param: rotation - rotation
    public Transform(Vector2 position, Vector2 scale, float rotation)
    {
        this.position = position;
        Scale = scale;
        Rotation = rotation;
    }

}

