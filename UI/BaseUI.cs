using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyMonoGameLibrary.Scenes;

namespace MyMonoGameLibrary.UI;

// this class represents a base UI element (sprite, text) in the game.
public abstract class BaseUI : UIElement
{
    // collider for the UI
    public UICollider Collider { get; private set; }
    
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

    // empty constructor
    public BaseUI() {}

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
        bool flipY)
    {
        this.position = position;
        Color = color;
        Rotation = rotation;
        Scale = scale;
        FlipX = flipX;
        FlipY = flipY;
    }

    // add box collider to UI
    //
    // param: width - width of collider
    // param: height - height of collider
    // param: xOffset - x offset of collider
    // param: yOffset - y offset of collider
    public void AddBoxCollider(float width, float height, float xOffset, float yOffset)
    {
        Collider ??= new UIBoxCollider(this, width, height, xOffset, yOffset);
    }

    // add box collider to UI
    //
    // param: width - width of collider
    // param: height - height of collider
    public void AddBoxCollider(float width, float height)
    {
        Collider ??= new UIBoxCollider(this, width, height);
    }

    // add circle collider to UI
    //
    // param: diameter - diameter of collider
    // param: xOffset - x offset
    // param: yOffset - y offset
    public void AddCircleCollider(float diameter, float xOffset, float yOffset)
    {
        Collider ??= new UICircleCollider(this, diameter, xOffset, yOffset);
    }

    // add circle collider to UI
    //
    // param: diameter - diameter of collider
    public void AddCircleCollider(float diameter)
    {
        Collider ??= new UICircleCollider(this, diameter);
    }
}
