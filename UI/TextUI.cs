using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyMonoGameLibrary.Scenes;

namespace MyMonoGameLibrary.UI;

// text UI element
public class TextUI : BaseUI
{
    // font
    public SpriteFont Font { get; set; }

    // text
    private string _text;
    private Vector2 _textSize;
    public string Text 
    {
        get => _text;
        set
        {
            _text = value;
            _textSize = Font.MeasureString(value);
            Origin = AnchorCalc.GetOrigin(Anchor, _textSize);
        }
    }

    // anchor mode
    private AnchorMode _anchor;
    public AnchorMode Anchor
    {
        get => _anchor;
        set
        {
            _anchor = value;
            Origin = AnchorCalc.GetOrigin(value, _textSize);
            
        }
    }

    // origin point
    public Vector2 Origin { get; private set; }

    // constructor
    //
    // param: font - the font
    // param: text - the text
    // param: anchorMode - anchoring point mode
    // param: children - children UI
    public TextUI(SpriteFont font, string text, AnchorMode anchorMode)
    {
        Font = font;
        Text = text;
        Anchor = anchorMode;
    }

    // constructor
    //
    // param: font - the font
    // param: text - the text
    // param: anchorMode - anchoring point mode
    // param: children - children UI
    public TextUI(SpriteFont font, string text, AnchorMode anchorMode, Vector2 position)
    {
        Font = font;
        Text = text;
        Anchor = anchorMode;
        this.position = position;
    }

    // constructor
    //
    // param: font - the font
    // param: text - the text
    // param: anchorMode - anchoring point mode
    // param: position - position
    // param: color - color
    // param: rotation - rotation
    // param: scale - scale
    // param: flipX - flip horizontally or not
    // param: flipY - flip vertically or not
    // param: children - children UI
    public TextUI(SpriteFont font, string text, AnchorMode anchorMode, Vector2 position, 
        Color color, float rotation, Vector2 scale, bool flipX, bool flipY) : base(position, color, rotation, scale, flipX, flipY)
    {
        Font = font;
        Text = text;
        Anchor = anchorMode;
    }

    // add text collider (box collider) to text UI
    public void AddTextCollider()
    {
        Vector2 offset = AnchorCalc.GetOffset(Anchor, _textSize);
        float scale = SceneTools.GetCanvas().Scale;
        AddBoxCollider(_textSize.X * scale, _textSize.Y * scale, offset.X * scale, offset.Y * scale);
    }
}
