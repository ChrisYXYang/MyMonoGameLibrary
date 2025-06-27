using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MyMonoGameLibrary.Graphics;
using MyMonoGameLibrary.Scenes;

namespace MyMonoGameLibrary.UI;

public class SpriteUI : BaseUI
{
    // sprite to render
    public Sprite Sprite { get; set; }
    
    // default sprite to render
    public Sprite DefaultSprite { get; set; }

    public UIAnimator Animator { get; private set; }

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
    public bool OverrideOrigin { get; set; }

    // constructor.
    //
    // param: sprite - the sprite
    // param: position - position
    public SpriteUI(Sprite sprite, Vector2 position) : base(position)
    {
        Sprite = sprite;
        DefaultSprite = sprite;
        Origin = Vector2.Zero;
        OverrideOrigin = true;
    }

    // constructor. Uses custom origin point
    //
    // param: sprite - the sprite
    // param: origin - origin point for UI
    // param: position - position
    public SpriteUI(Sprite sprite, Vector2 origin, Vector2 position) : base(position)
    {
        Sprite = sprite;
        DefaultSprite = sprite;
        Origin = origin;
        OverrideOrigin = true;
    }

    // constructor. Uses origin point of the sprite
    //
    // param: sprite - the sprite
    // param: position - position
    // param: color - color
    public SpriteUI(Sprite sprite, Vector2 position, Color color) : base(position, color)
    {
        Sprite = sprite;
        DefaultSprite = sprite;
        Origin = Vector2.Zero;
        OverrideOrigin = false;

    }

    // constructor. Uses custom origin point
    //
    // param: sprite - the sprite
    // param: origin - origin point for UI
    // param: position - position
    // param: color - color
    // param: rotation - rotation
    // param: scale - scale
    // param: flipX - flip horizontally or not
    // param: flipY - flip vertically or not
    public SpriteUI(Sprite sprite, Vector2 origin, Vector2 position, Color color) : base(position, color)
    {
        Sprite = sprite;
        DefaultSprite = sprite;
        Origin = origin;
        OverrideOrigin = true;

    }

    // constructor. Uses origin point of the sprite
    //
    // param: sprite - the sprite
    // param: position - position
    // param: color - color
    // param: rotation - rotation
    // param: scale - scale
    // param: flipX - flip horizontally or not
    // param: flipY - flip vertically or not
    public SpriteUI(Sprite sprite, Vector2 position, Color color, float rotation, Vector2 scale, 
        bool flipX, bool flipY) : base(position, color, rotation, scale, flipX, flipY)
    {
        Sprite = sprite;
        DefaultSprite = sprite;
        Origin = Vector2.Zero;
        OverrideOrigin = false;

    }

    // constructor. Uses custom origin point
    //
    // param: sprite - the sprite
    // param: origin - origin point for UI
    // param: position - position
    // param: color - color
    // param: rotation - rotation
    // param: scale - scale
    // param: flipX - flip horizontally or not
    // param: flipY - flip vertically or not
    public SpriteUI(Sprite sprite, Vector2 origin, Vector2 position, Color color, float rotation, 
        Vector2 scale, bool flipX, bool flipY) : base(position, color, rotation, scale, flipX, flipY)
    {
        Sprite = sprite;
        DefaultSprite = sprite;
        Origin = origin;
        OverrideOrigin = true;

    }

    // add the animator
    public void AddAnimator(Animation animation)
    {
        if (Animator == null)
        {
            Animator = new UIAnimator(this, animation);
        }
    }
}
