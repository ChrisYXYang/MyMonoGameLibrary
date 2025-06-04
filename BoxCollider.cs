using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyMonoGameLibrary.Tools;

namespace MyMonoGameLibrary;

// component for rectangle collider for gameobject
public class BoxCollider : Component, RectCollider
{
    // variables and properties
    private Transform _transform;
    
    public int XOffset { get; private set; } = 0;
    private float _realXOffset;
    public int YOffset { get; private set; } = 0;
    private float _realYOffset;
    public int Width { get; private set; }
    private float _realWidth;
    public int Height { get; private set; }
    private float _realHeight;
    public float Left => _transform.position.X + _realXOffset;
    public float Right => Left + _realWidth;
    public float Top => _transform.position.Y + _realYOffset;
    public float Bottom => Top + _realHeight;


    // constructor
    //
    // param: attributes - attributes
    public BoxCollider(Dictionary<string, string> attributes)
    {
        // set the properties
        Width = int.Parse(attributes["width"]);
        Height = int.Parse(attributes["height"]);

        bool centered = true;
        if (attributes.ContainsKey("centered"))
        {
            centered = bool.Parse(attributes["centered"]);
        }

        if (centered)
        {
            XOffset = -(Width /2);
            YOffset = -Height;
        }
        
        if (attributes.ContainsKey("xOffset"))
        {
            XOffset = int.Parse(attributes["xOffset"]);
        }

        if (attributes.ContainsKey("yOffset"))
        {
            YOffset = int.Parse(attributes["yOffset"]);
        }

        // set the private variables
        _realXOffset = (float)XOffset / Camera.SpritePixelsPerUnit;
        _realYOffset = (float)YOffset / Camera.SpritePixelsPerUnit;
        _realWidth = (float)Width / Camera.SpritePixelsPerUnit;
        _realHeight = (float)Height / Camera.SpritePixelsPerUnit;
    }

    // initialize
    //
    // param: parent - parent game object
    public override void Initialize(GameObject parent)
    {
        base.Initialize(parent);
        _transform = GetComponent<Transform>();
    }

    // see if intersects between another rect collider
    //
    // param: other - other collider
    // return: intersect or not
    public bool Intersects(RectCollider other)
    {
        return (this.Left < other.Right &&
                other.Left < this.Right &&
                this.Top < other.Bottom &&
                other.Top < this.Bottom);
    }

    // see if intersection between two game objects
    //
    // param: a - first box collider
    // param: b - second box collider
    // return: intersect or not
    public static bool Intersect(GameObject a, GameObject b)
    {
        BoxCollider aC = a.GetComponent<BoxCollider>();
        BoxCollider bC = b.GetComponent<BoxCollider>();
        
        return (aC.Left < bC.Right &&
                bC.Left < aC.Right &&
                aC.Top < bC.Bottom &&
                bC.Top < aC.Bottom);
    }
}
