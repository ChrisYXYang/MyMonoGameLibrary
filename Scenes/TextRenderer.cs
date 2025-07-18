using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyMonoGameLibrary.Graphics;
using MyMonoGameLibrary.UI;

namespace MyMonoGameLibrary.Scenes;

// component for rendering text in the game world
public class TextRenderer : RendererComponent
{
    private Vector2 _textSize;
    private TextCollider _collider;

    // the font
    private SpriteFont _font;
    public SpriteFont Font
    {
        get
        {
            return _font;
        }
        set
        {
            _font = value;
            _textSize = value.MeasureString(Text);
            Origin = AnchorCalc.GetOrigin(Anchor, _textSize);
            _collider?.Update(Anchor, (_textSize / (Camera.SpritePixelsPerUnit * Camera.PixelScale)));
        }
    }

    // the text to render
    // text
    private string _text = "";
    public string Text
    {
        get => _text;
        set
        {
            if (_text == value)
                return;

            _text = value;
            _textSize = Font.MeasureString(value);
            Origin = AnchorCalc.GetOrigin(Anchor, _textSize);
            _collider?.Update(Anchor, (_textSize / (Camera.SpritePixelsPerUnit * Camera.PixelScale)));
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
            _collider?.Update(value, (_textSize / (Camera.SpritePixelsPerUnit * Camera.PixelScale)));
        }
    }

    // origin point
    public Vector2 Origin { get; private set; }

    // constructor
    //
    // param: font - the font to use
    // param: text - the text to render
    // param: anchor - anchoring point mode
    public TextRenderer(SpriteFont font, string text, AnchorMode anchor)
    {
        Font = font;
        Text = text;
        Anchor = anchor;
    }

    // constructor
    //
    // param: font - the font to use
    // param: text - the text to render
    // param: anchor - anchoring point mode
    // param: layerDepth - layer depth
    public TextRenderer(SpriteFont font, string text, AnchorMode anchor, float layerDepth) : base(layerDepth)
    {
        Font = font;
        Text = text;
        Anchor = anchor;
    }

    // constructor
    //
    // param: font - the font to use
    // param: text - the text to render
    // param: anchor - anchoring point mode
    // param: color - color
    public TextRenderer(SpriteFont font, string text, AnchorMode anchor, Color color) : base(color)
    {
        Font = font;
        Text = text;
        Anchor = anchor;
    }

    // constructor
    //
    // param: font - the font to use
    // param: text - the text to render
    // param: anchor - anchoring point mode
    // param: color - color
    // param: layerDepth - layer depth
    public TextRenderer(SpriteFont font, string text, AnchorMode anchor, Color color, float layerDepth) 
        : base(color, layerDepth)
    {
        Font = font;
        Text = text;
        Anchor = anchor;
    }

    // constructor
    //
    // param: font - the font to use
    // param: text - the text to render
    // param: anchor - anchoring point mode
    // param: color - sprite color
    // param: flipX - flip horizontally
    // param: flipY - flip vertically
    // param: layerDepth - layer depth
    public TextRenderer(SpriteFont font, string text, AnchorMode anchor, Color color, bool flipX, 
        bool flipY, float layerDepth) : base(color, flipX, flipY, layerDepth)
    {
        Font = font;
        Text = text;
        Anchor = anchor;
    }

    public override void Initialize(GameObject parent)
    {
        base.Initialize(parent);
        _collider = GetComponent<TextCollider>();
    }
}
