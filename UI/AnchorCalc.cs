using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MyMonoGameLibrary.UI;

// this class runs anchor mode calculations
public static class AnchorCalc
{
    // calculate origin point of a text according to anchor mode
    //
    // param: anchor - anchor mode
    // param: size - size of text
    // return: origin point
    public static Vector2 GetOrigin(AnchorMode anchor, Vector2 size)
    {
        switch (anchor)
        {
            case AnchorMode.TopLeft:
                return Vector2.Zero;
            case AnchorMode.MiddleLeft:
                return new Vector2(0, size.Y * 0.5f);
            case AnchorMode.BottomLeft:
                return new Vector2(0, size.Y);
            case AnchorMode.TopCenter:
                return new Vector2(size.X * 0.5f, 0);
            case AnchorMode.MiddleCenter:
                return new Vector2(size.X * 0.5f, size.Y * 0.5f);
            case AnchorMode.BottomCenter:
                return new Vector2(size.X * 0.5f, size.Y);
            case AnchorMode.TopRight:
                return new Vector2(size.X, 0);
            case AnchorMode.MiddleRight:
                return new Vector2(size.X, size.Y * 0.5f);
            case AnchorMode.BottomRight:
                return new Vector2(size.X, size.Y);
            default:
                return Vector2.Zero;
        }
    }

    // get offset for text collider eon text
    //
    // param: anchor - anchor mode
    // param: size - size of text
    // return: offset point
    public static Vector2 GetOffset(AnchorMode anchor, Vector2 size)
    {
        switch (anchor)
        {
            case AnchorMode.TopLeft:
                return new Vector2(size.X * 0.5f, size.Y * 0.5f);
            case AnchorMode.MiddleLeft:
                return new Vector2(size.X * 0.5f, 0);
            case AnchorMode.BottomLeft:
                return new Vector2(size.X * 0.5f, -(size.Y * 0.5f));
            case AnchorMode.TopCenter:
                return new Vector2(0, size.Y * 0.5f);
            case AnchorMode.MiddleCenter:
                return new Vector2(0, 0);
            case AnchorMode.BottomCenter:
                return new Vector2(0, -(size.Y * 0.5f));
            case AnchorMode.TopRight:
                return new Vector2(-(size.X * 0.5f), size.Y * 0.5f);
            case AnchorMode.MiddleRight:
                return new Vector2(-(size.X * 0.5f), 0);
            case AnchorMode.BottomRight:
                return new Vector2(-(size.X * 0.5f), -(size.Y * 0.5f));
            default:
                return Vector2.Zero;
        }
    }
}
