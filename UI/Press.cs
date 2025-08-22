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

public  class Press : BehaviorComponent
{
    // variables and properties
    private Action _click;
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

    // constructor
    //
    // param: normal - default sprite
    // param: hover - sprite when hovered
    // param: click - method to do when clicked
    public Press(Sprite normal, Sprite hover, Action click)
    {
        Normal = normal;
        Hover = hover;
        _click = click;
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

            if (InputManager.Mouse.WasButtonJustPressed(MouseButton.Left))
            {
                if (_click != null)
                {
                    _click.Invoke();
                }
                else
                {
                    Clicked();
                }
            }
        }
        else
        {
            _sr.Sprite = Normal;

        }
    }

    public virtual void Clicked() { }
}
