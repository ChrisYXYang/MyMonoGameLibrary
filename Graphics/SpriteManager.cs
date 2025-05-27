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
    public float LayerDepth { get; set; }  

    private Transform _transform;

    // constructor
    //
    // param: parent - parent game object
    // param: sprite - Sprite to use
    // param: color - color of sprite
    // param: flipX - flip horizontal or not
    // param: flipY - flip vertical or not
    // param: layerDepth - layer depth
    public SpriteManager(GameObject parent, Sprite sprite, Color color,
        bool flipX, bool flipY, float layerDepth) : base(parent)
    {
        Sprite = sprite;
        Color = color;
        FlipX = flipX;
        FlipY = flipY;
        LayerDepth = layerDepth;
    }


    // initialize
    public override void Initialize()
    {
        base.Initialize();
        _transform = GetComponent<Transform>();
    }

    // draw the sprite
    //
    // param: spriteBatch - SpriteBatch to use to draw
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

