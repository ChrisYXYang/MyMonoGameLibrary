using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MyMonoGameLibrary.Tools;

namespace MyMonoGameLibrary;

// component for the transform of gameobject, which is position, scale, rotation
public class Transform : Component
{
    // variables and properties
    public Vector2 position = Vector2.Zero;
    public Vector2 Scale { get; set; } = Vector2.One;
    public float Rotation { get; set; } = 0;

    // constructor
    //
    // param: attributes - attributes
    public Transform(Dictionary<string, string> attributes)
    {
        if (attributes.ContainsKey("position"))
        {
            position = Converter.ParseVector2(attributes["position"]);
        }

        if (attributes.ContainsKey("scale"))
        {
            Scale = Converter.ParseVector2(attributes["scale"]);
        }

        if (attributes.ContainsKey("rotation"))
        {
            Rotation = MathHelper.ToRadians(float.Parse(attributes["rotation"]));
        }
    }

}

