using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MyMonoGameLibrary.Tools;
using static System.Formats.Asn1.AsnWriter;

namespace MyMonoGameLibrary;

// component for rectangle collider for gameobject
public class BoxCollider : Component
{
    // variables and properties
    public int XOffset { get; set; } = 0;
    public int YOffset { get; set; } = 0;
    public int Width { get; set; }
    public int Height { get; set; }

    // constructor
    //
    // param: attributes - attributes
    public BoxCollider(Dictionary<string, string> attributes)
    {
        if (attributes.ContainsKey("xOffset"))
        {
            XOffset = int.Parse(attributes["xOffset"]);
        }

        if (attributes.ContainsKey("yOffset"))
        {
            YOffset = int.Parse(attributes["yOffset"]);
        }

        if (attributes.ContainsKey("width"))
        {
            Width = int.Parse(attributes["width"]);
        }

        if (attributes.ContainsKey("height"))
        {
            Height = int.Parse(attributes["height"]);
        }
    }
}
