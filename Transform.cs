using System;
using Microsoft.Xna.Framework;

namespace MyMonoGameLibrary;

// component for the transform of gameobject, which is position, scale, rotation
public class Transform : Component
{
    // variables and properties
    public Vector2 Position { get; set; }
    public Vector2 Scale { get; set; }
    public float Rotation { get; set; }

    // constructor
    //
    // param: parent - parent game object
    // param: position - position of game object
    // param: rotation - rotation of game object
    // param: scale - scale of game object
    public Transform(Vector2 position, float rotation, Vector2 scale)
    {
        Position = position;
        Scale = scale;
        Rotation = rotation;
    }

}

