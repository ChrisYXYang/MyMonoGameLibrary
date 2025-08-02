using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MyMonoGameLibrary.Scenes;

// component for rendering
public abstract class RendererComponent : CoreComponent
{
    // variables and properties
    public Color Color { get; set; } = Color.White;
    public bool FlipX { get; set; } = false;
    public bool FlipY { get; set; } = false;
    public float LayerDepth { get; set; } = 0.5f;
    public bool IsVisible { get; set; } = true;

    // empty constructor
    public RendererComponent() { }

    // constructor
    //
    // param: layerDepth - layer depth
    public RendererComponent(float layerDepth)
    {
        LayerDepth = layerDepth;
    }

    // constructor
    //
    // param: color - color
    public RendererComponent(Color color)
    {
        Color = color;
    }

    // constructor
    //
    // param: color - color
    // param: layerDepth - layer depth
    public RendererComponent(Color color, float layerDepth)
    {
        Color = color;
        LayerDepth = layerDepth;
    }

    // constructor
    //
    // param: color - sprite color
    // param: flipX - flip horizontally
    // param: flipY - flip vertically
    public RendererComponent(Color color, bool flipX, bool flipY)
    {
        Color = color;
        FlipX = flipX;
        FlipY = flipY;
    }

    // constructor
    //
    // param: color - sprite color
    // param: flipX - flip horizontally
    // param: flipY - flip vertically
    // param: layerDepth - layer depth
    public RendererComponent(Color color, bool flipX, bool flipY, float layerDepth)
    {
        Color = color;
        FlipX = flipX;
        FlipY = flipY;
        LayerDepth = layerDepth;
    }
}
