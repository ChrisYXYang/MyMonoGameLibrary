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
            Vector2 truePos = position;
            GameObject parent = Parent.Parent;
            while (parent != null)
            {
                if (parent.Transform != null)
                {
                    truePos += parent.Transform.position;
                }
                parent = parent.Parent;
            }

            return truePos;
        }
    }

    public Vector2 TrueScale
    {
        get
        {
            Vector2 trueScale = Scale;
            GameObject parent = Parent.Parent;
            while (parent != null)
            {
                if (parent.Transform != null)
                {
                    trueScale *= Parent.Parent.Transform.Scale;
                }
                parent = parent.Parent;
            }

            return trueScale;
        }
    }

    public float TrueRotation
    {
        get
        {
            float trueRot = Rotation;
            GameObject parent = Parent.Parent;
            while (parent != null)
            {
                if (parent.Transform != null)
                {
                    trueRot += parent.Transform.Rotation;
                }
                parent = parent.Parent;
            }

            return trueRot;
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

