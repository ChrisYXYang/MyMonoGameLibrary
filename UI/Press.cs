using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MyMonoGameLibrary.Graphics;
using MyMonoGameLibrary.Input;
using MyMonoGameLibrary.Scenes;

namespace MyMonoGameLibrary.UI;

public abstract class Press : BehaviorComponent
{
    // variables and properties
    private UISprite _sr;
    public Sprite Normal { get; set; }
    public Sprite Hover { get; set; }

    // constructor
    //
    // param: normal - default sprite
    // param: hover - sprite when hovered
    public Press(Sprite normal, Sprite hover)
    {
        Normal = normal;
        Hover = hover;
    }

    public override void Start()
    {
        _sr = GetComponent<UISprite>();
    }

    public override void Update(GameTime gameTime)
    {
        if (Collisions.MouseInUICollider(Parent.Collider))
        {
            _sr.Sprite = Hover;

            if (!InputManager.Mouse.IsButtonDown(MouseButton.Left))
            {
                Clicked();
            }
        }
        else
        {
            _sr.Sprite = Normal;

        }
    }

    public abstract void Clicked();
}
