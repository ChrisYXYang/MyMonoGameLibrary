using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
            _textSize = Font.MeasureString(_text);
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

            switch (_anchor)
            {
                case AnchorMode.TopLeft:
                    Origin = Vector2.Zero;
                    break;
                case AnchorMode.MiddleLeft:
                    Origin = new Vector2(0, _textSize.Y * 0.5f);
                    break;
                case AnchorMode.BottomLeft:
                    Origin = new Vector2(0, _textSize.Y);
                    break;
                case AnchorMode.TopCenter:
                    Origin = new Vector2(_textSize.X * 0.5f, 0);
                    break;
                case AnchorMode.MiddleCenter:
                    Origin = new Vector2(_textSize.X * 0.5f, _textSize.Y * 0.5f);
                    break;
                case AnchorMode.BottomCenter:
                    Origin = new Vector2(_textSize.X * 0.5f, _textSize.Y);
                    break;
                case AnchorMode.TopRight:
                    Origin = new Vector2(_textSize.X, 0);
                    break;
                case AnchorMode.MiddleRight:
                    Origin = new Vector2(_textSize.X, _textSize.Y * 0.5f);
                    break;
                case AnchorMode.BottomRight:
                    Origin = new Vector2(_textSize.X, _textSize.Y);
                    break;
                default:
                    break;
            }
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
}
