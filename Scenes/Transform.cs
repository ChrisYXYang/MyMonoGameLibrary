using System;
using System.Collections.Generic;
using System.Diagnostics;
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

    public Vector2 TruePosition
    {
        get
        {
            if (Parent.Parent != null)
            {
                if (Parent.Parent.Transform != null)
                {
                    return position + Parent.Parent.Transform.position;
                }
            }

            return position;
        }
    }

    public Vector2 TrueScale
    {
        get
        {
            if (Parent.Parent != null)
            {
                if (Parent.Parent.Transform != null)
                {
                    return Scale * Parent.Parent.Transform.Scale;
                }
            }

            return Scale;
        }
    }

    public float TrueRotation
    {
        get
        {
            if (Parent.Parent != null)
            {
                if (Parent.Parent.Transform != null)
                {
                    return Rotation + Parent.Parent.Transform.Rotation;
                }
            }

            return Rotation;
        }
    }

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

