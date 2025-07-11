using System;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MyMonoGameLibrary.Scenes;
using static System.Formats.Asn1.AsnWriter;

namespace MyMonoGameLibrary.UI;

public class UIText : RendererComponent
{
    // the font
    public SpriteFont Font { get; set; }

    // the text to render
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
    // param: font - the font to use
    // param: text - the text to render
    // param: anchor - anchoring point mode
    public UIText(SpriteFont font, string text, AnchorMode anchor)
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
    public UIText(SpriteFont font, string text, AnchorMode anchor, float layerDepth) : base(layerDepth)
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
    public UIText(SpriteFont font, string text, AnchorMode anchor, Color color) : base(color)
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
    public UIText(SpriteFont font, string text, AnchorMode anchor, Color color, float layerDepth)
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
    public UIText(SpriteFont font, string text, AnchorMode anchor, Color color, bool flipX,
        bool flipY, float layerDepth) : base(color, flipX, flipY, layerDepth)
    {
        Font = font;
        Text = text;
        Anchor = anchor;
    }

    public override void Initialize(GameObject parent)
    {
        base.Initialize(parent);

        Vector2 offset = AnchorCalc.GetOffset(Anchor, _textSize);
        
        if (parent.Collider is UIBoxCollider box)
        {
            box.Width = _textSize.X * Transform.Scale.X;
            box.Height = _textSize.Y * Transform.Scale.X;
            box.Offset = new Vector2(offset.X * Transform.Scale.X, offset.Y * Transform.Scale.Y);
        }
    }
}
