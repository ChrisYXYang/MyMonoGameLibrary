using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MyMonoGameLibrary.UI;

namespace MyMonoGameLibrary.Scenes;

// manages the box collider to fit around text renderer
public class TextCollider : CoreComponent
{
    // variables and properties
    public float XPad { get; set; } = 0;
    public float YPad { get; set; } = 0;
    private BoxCollider _collider;

    // empty constructor
    public TextCollider() { }

    // constructor
    //
    // param: xPad - x padding
    // param: yPad - y padding
    public TextCollider(float xPad, float yPad)
    {
        XPad = xPad;
        YPad = yPad;
    }
    
    public override void Initialize(GameObject parent)
    {
        base.Initialize(parent);
        _collider = GetComponent<BoxCollider>();
    }

    // update box collider to fit text
    //
    // param: anchor - anchor mode
    // param: textSize - size of text
    public void Update(AnchorMode anchor, Vector2 textSize)
    {
        Vector2 offset = AnchorCalc.GetOffset(anchor, textSize);
        _collider.Width = (textSize.X * Transform.TrueScale.X) + XPad;
        _collider.Height = (textSize.Y * Transform.TrueScale.X) + YPad;
        _collider.Offset = new Vector2(offset.X * Transform.TrueScale.X, offset.Y * Transform.TrueScale.Y);
    }
}
