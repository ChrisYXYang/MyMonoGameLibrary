using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using MyMonoGameLibrary.Scenes;

namespace MyMonoGameLibrary.UI;

// the canvas will contain UI elemnts to be drawn on the screen. This will there can be layering of 
// which elements to draw over the other.
public class Canvas
{
    // scale of canvas
    public float Scale { get; set; } = Camera.PixelScale;
    
    private List<UIElement> _children;

    // constructor
    //
    // param: children - children UI elements
    public Canvas(List<UIElement> children)
    {
        _children = children;

        if (children == null)
        {
            _children = new List<UIElement>();
        }
    }

    // constructor
    //
    // param: children - chidlren UI elements
    // param: scale - scale of canvas
    public Canvas(List<UIElement> children, float scale)
    {
        _children = children;
        Scale = scale;

        if (children == null)
        {
            _children = new List<UIElement>();
        }
    }

    public UIElement[] GetChildren()
    {
        UIElement[] output = new UIElement[ _children.Count ];
        _children.CopyTo(output);
        return output;
    }
}
