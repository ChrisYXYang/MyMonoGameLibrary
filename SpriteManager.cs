using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MyMonoGameLibrary;

// Summary:
//      component for managing sprites
public class SpriteManager : Component
{
    // variables and properties
    public Texture2D Sprite { get; set; }
    public Color Color { get; set; }
    public bool FlipX { get; set; }
    public bool FlipY { get; set; }

    // constructor
    public SpriteManager(Texture2D sprite, Color color, bool flipX, bool flipY)
    {
        this.Sprite = sprite;
        this.Color = color;
        this.FlipX = flipX;
        this.FlipY = flipY;
    }
}

