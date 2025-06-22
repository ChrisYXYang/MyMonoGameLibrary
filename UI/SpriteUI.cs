using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MyMonoGameLibrary.Graphics;

namespace MyMonoGameLibrary.UI;

public class SpriteUI : UI
{
    // sprite
    public Sprite Sprite;

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
}
