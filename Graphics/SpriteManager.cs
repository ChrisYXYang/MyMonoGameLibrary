using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MyMonoGameLibrary.Graphics;

// Component for managing a sprite for a game object.
public class SpriteManager : Component
{
    // variables and properties
    public Sprite Sprite { get; set; }
    public Color Color { get; set; }
    public bool FlipX { get; set; }
    public bool FlipY { get; set; }
    public float LayerDepth {  get; set; }  

    private Transform _transform;

    // constructor
    public SpriteManager(GameObject parent, Sprite sprite, Color color, 
        bool flipX, bool flipY) : base(parent)
    {
        Sprite = sprite;
        Color = color;
        FlipX = flipX;
        FlipY = flipY;
    }

    // initialize
    public override void Initialize()
    {
        base.Initialize();
        _transform = GetComponent<Transform>();
    }

    // Summary:
    //      Draw the sprite
    public void Draw(SpriteBatch spriteBatch)
    {
        int spriteEffect = 0;
        
        if (FlipX) { spriteEffect += 1; }
        if (FlipY) { spriteEffect += 2; }

        Sprite.Draw(
            spriteBatch, 
            _transform.Position, 
            _transform.Rotation, 
            _transform.Scale, 
            Color, 
            (SpriteEffects)spriteEffect, 
            LayerDepth);
    }
}

