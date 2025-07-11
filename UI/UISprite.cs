using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MyMonoGameLibrary.Graphics;
using MyMonoGameLibrary.Scenes;

namespace MyMonoGameLibrary.UI;

public class UISprite : RendererComponent
{
    // sprite to render
    public Sprite Sprite { get; set; }
    
    // default sprite to render
    public Sprite DefaultSprite { get; set; }


    // custom origin point for this UI
    private Vector2 _origin;
    public Vector2 Origin 
    {
        get
        {
            if (OverrideOrigin)
            {
                return _origin;
            }

            return Sprite.OriginPoint;
        }
        set => _origin = value;
    }

    // will this UI have custom origin point
    public bool OverrideOrigin { get; set; } = false;

    // constructor
    //
    // param: sprite - sprite to render
    public UISprite(Sprite sprite)
    {
        Sprite = sprite;
        DefaultSprite = sprite;
    }

    // constructor
    //
    // param: sprite - sprite to render
    // param: layerDepth - layer depth
    public UISprite(Sprite sprite, float layerDepth) : base(layerDepth)
    {
        Sprite = sprite;
        DefaultSprite = sprite;
    }

    // constructor
    //
    // param: sprite - sprite to render
    // param: color - color
    public UISprite(Sprite sprite, Color color) : base(color)
    {
        Sprite = sprite;
        DefaultSprite = sprite;
    }

    // constructor
    //
    // param: sprite - sprite to render
    // param: color - color
    // param: layerDepth - layer depth
    public UISprite(Sprite sprite, Color color, float layerDepth) : base(color, layerDepth)
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
    public UISprite(Sprite sprite, Color color, bool flipX, bool flipY, float layerDepth)
        : base(color, flipX, flipY, layerDepth)
    {
        Sprite = sprite;
        DefaultSprite = sprite;
    }

    // constructor
    //
    // param: sprite - sprite to render
    // param: origin - origin override
    public UISprite(Sprite sprite, Vector2 origin)
    {
        Sprite = sprite;
        DefaultSprite = sprite;
        Origin = origin;
        OverrideOrigin = true;
    }

    // constructor
    //
    // param: sprite - sprite to render
    // param: layerDepth - layer depth
    // param: origin - origin override
    public UISprite(Sprite sprite, float layerDepth, Vector2 origin) : base(layerDepth)
    {
        Sprite = sprite;
        DefaultSprite = sprite;
        Origin = origin;
        OverrideOrigin = true;
    }

    // constructor
    //
    // param: sprite - sprite to render
    // param: color - color
    // param: origin - origin override
    public UISprite(Sprite sprite, Color color, Vector2 origin) : base(color)
    {
        Sprite = sprite;
        DefaultSprite = sprite;
        Origin = origin;
        OverrideOrigin = true;
    }

    // constructor
    //
    // param: sprite - sprite to render
    // param: color - color
    // param: layerDepth - layer depth
    // param: origin - origin override
    public UISprite(Sprite sprite, Color color, float layerDepth, Vector2 origin) : base(color, layerDepth)
    {
        Sprite = sprite;
        DefaultSprite = sprite;
        Origin = origin;
        OverrideOrigin = true;
    }

    // constructor
    //
    // param: sprite - sprite to render
    // param: color - sprite color
    // param: flipX - flip horizontally
    // param: flipY - flip vertically
    // param: layerDepth - layer depth
    // param: origin - origin override
    public UISprite(Sprite sprite, Color color, bool flipX, bool flipY, float layerDepth, Vector2 origin)
        : base(color, flipX, flipY, layerDepth)
    {
        Sprite = sprite;
        DefaultSprite = sprite;
        Origin = origin;
        OverrideOrigin = true;
    }
}
