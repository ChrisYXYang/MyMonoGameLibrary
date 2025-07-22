using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using MyMonoGameLibrary.Graphics;

namespace MyMonoGameLibrary.Scenes;

// Component for drawing a sprite for a game object.
public class SpriteRenderer : RendererComponent
{
    // the sprite to render
    public Sprite Sprite { get; set; }

    // sprite to default on if no animations
    public Sprite DefaultSprite { get; set; }

    // empty constructor
    public SpriteRenderer()
    {
    }

    // constructor
    //
    // param: layerDepth - layer depth
    public SpriteRenderer(float layerDepth) : base(layerDepth)
    {
    }

    // constructor
    //
    // param: sprite - sprite to render
    public SpriteRenderer(Sprite sprite)
    {
        Sprite = sprite;
        DefaultSprite = sprite;
    }

    // constructor
    //
    // param: sprite - sprite to render
    // param: layerDepth - layer depth
    public SpriteRenderer(Sprite sprite, float layerDepth) : base(layerDepth)
    {
        Sprite = sprite;
        DefaultSprite = sprite;
    }

    // constructor
    //
    // param: sprite - sprite to render
    // param: color - color
    public SpriteRenderer(Sprite sprite, Color color) : base(color)
    {
        Sprite = sprite;
        DefaultSprite = sprite;
    }

    // constructor
    //
    // param: sprite - sprite to render
    // param: color - color
    // param: layerDepth - layer depth
    public SpriteRenderer(Sprite sprite, Color color, float layerDepth) : base(color, layerDepth)
    {
        Sprite = sprite;
        DefaultSprite = sprite;
    }

    // constructor
    //
    // param: sprite - sprite to render
    // param: color - sprite color
    // param: flipX - flip horizontally
    // param: flipY - flip vertically
    // param: layerDepth - layer depth
    public SpriteRenderer(Sprite sprite, Color color, bool flipX, bool flipY, float layerDepth)
        : base(color, flipX, flipY, layerDepth)
    {
        Sprite = sprite;
        DefaultSprite = sprite;
    }
}

