using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyMonoGameLibrary.Scenes;

namespace MyMonoGameLibrary.UI;

// this class represents a base UI element (sprite, text) in the game.
public abstract class BaseUI : UIElement
{
    // position of UI element
    public Vector2 position = Vector2.Zero;

    // color of UI
    public Color Color { get; set; } = Color.White;

    // rotation of UI
    public float Rotation { get; set; } = 0f;

    // scale of UI element
    public Vector2 Scale { get; set; } = Vector2.One;

    // flip horizontally
    public bool FlipX { get; set; } = false;

    // flip vertically
    public bool FlipY { get; set; } = false;

    // children of this element
    private List<BaseUI> _children;

    // constructor
    //
    // param: children - children UI
    public BaseUI(List<BaseUI> children) : base(children)
    {
    }

    // constructor
    //
    // param: position - position
    // param: color - color
    // param: rotation - rotation
    // param: scale - scale
    // param: flipX - flip horizontally or not
    // param: flipY - flip vertically or not
    // param: children - children UI
    public BaseUI(Vector2 position, Color color, float rotation, Vector2 scale, bool flipX, 
        bool flipY, List<BaseUI> children) : base(children)
    {
        this.position = position;
        Color = color;
        Rotation = rotation;
        Scale = scale;
        FlipX = flipX;
        FlipY = flipY;
    }
}
