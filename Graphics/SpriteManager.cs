using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MyMonoGameLibrary.Graphics;

// Component for managing a sprite for a game object.
public class SpriteManager : Component
{
    // variables and properties
    public Sprite Sprite { get; set; }
    public Color Color { get; set; } = Color.White;
    public bool FlipX { get; set; } = false;
    public bool FlipY { get; set; } = false;
    public float LayerDepth { get; set; } = 0;
    private Transform _transform;

    // constructor
    //
    // attributes - attributes
    public SpriteManager(Dictionary<string, string> attributes)
    {
        Sprite = SpriteLibrary.GetSprite(attributes["spriteSheet"], attributes["sprite"]);

        if (attributes.ContainsKey("color"))
        {
            var prop = typeof(Color).GetProperty(attributes["color"]);
            Color = (Color)prop.GetValue(null, null);
        }

        if (attributes.ContainsKey("flipX"))
        {
            FlipX = bool.Parse(attributes["flipX"]);
        }

        if (attributes.ContainsKey("flipY"))
        {
            FlipY = bool.Parse(attributes["flipY"]);
        }

        if (attributes.ContainsKey("layerDepth"))
        {
            LayerDepth = float.Parse(attributes["layerDepth"]);
        }
    }


    // initialize
    //
    // param: parent - parent game object
    public override void Initialize(GameObject parent)
    {
        base.Initialize(parent);
        _transform = GetComponent<Transform>();
    }

    // draw the sprite using sprite manager and transform properties
    public void Draw()
    {
        // set sprite effects
        int spriteEffect = 0;
        if (FlipX)
            spriteEffect += 1;
        if (FlipY)
            spriteEffect += 2;

        Sprite.GameDraw
            (
                _transform.position,
                Color,
                _transform.Rotation,
                _transform.Scale,
                (SpriteEffects)spriteEffect,
                LayerDepth
            );
    }
}

