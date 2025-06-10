using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using MyMonoGameLibrary.Graphics;

namespace MyMonoGameLibrary.Scenes;

// Component for drawing a sprite for a game object.
public class SpriteRenderer : Component, IRenderer
{
    // variables and properties
    public Sprite Sprite { get; set; }
    public Color Color { get; set; } = Color.White;
    public bool FlipX { get; set; } = false;
    public bool FlipY { get; set; } = false;
    public float LayerDepth { get; set; } = 0.5f;
    private Transform _transform;

    // constructor
    //
    // param: sprite - sprite to render
    public SpriteRenderer(Sprite sprite)
    {
        Sprite = sprite;
    }

    // constructor
    //
    // param: sprite - sprite to render

    // param: layerDepth - layer depth
    public SpriteRenderer(Sprite sprite, float layerDepth)
    {
        Sprite = sprite;
        LayerDepth = layerDepth;
    }

    // constructor
    //
    // param: sprite - sprite to render
    // param: color - sprite color
    // param: flipX - flip horizontally
    // param: flipY - flip vertically
    // param: layerDepth - layer depth
    public SpriteRenderer(Sprite sprite, Color color, bool flipX, bool flipY, float layerDepth)
    {
        Sprite = sprite;
        Color = color;
        FlipX = flipX;
        FlipY = flipY;
        LayerDepth = layerDepth;
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

