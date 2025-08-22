using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MyMonoGameLibrary.Graphics;
using MyMonoGameLibrary.Input;
using MyMonoGameLibrary.Scenes;

namespace MyMonoGameLibrary.UI;

// this class contains base funcionalities for a button (might be useless we'll see)
public class Button : BehaviorComponent
{
    // properties and variables
    private Action _click;

    private UISprite _sr;
    public Sprite Normal { get; set; }
    public Sprite Hover { get; set; }
    public Sprite Press { get; set; }

    private bool _pressed = false;
    private bool _hover = false;

    // constructor
    //
    // param: normal - default sprite
    // param: hover - sprite when hovered
    // para: press - sprite when pressing
    public Button(Sprite normal, Sprite hover, Sprite press)
    {
        Normal = normal;
        Hover = hover;
        Press = press;
    }

    // constructor
    //
    // param: normal - default sprite
    // param: hover - sprite when hovered
    public Button(Sprite normal, Sprite hover)
    {
        Normal = normal;
        Hover = hover;
        Press = normal;
    }

    // constructor
    //
    // param: normal - default sprite
    // param: hover - sprite when hovered
    // para: press - sprite when pressing
    // param: click - method to do when clicked
    public Button(Sprite normal, Sprite hover, Sprite press, Action click)
    {
        Normal = normal;
        Hover = hover;
        Press = press;
        _click = click;
    }

    // constructor
    //
    // param: normal - default sprite
    // param: hover - sprite when hovered
    // param: click - method to do when clicked
    public Button(Sprite normal, Sprite hover, Action click)
    {
        Normal = normal;
        Hover = hover;
        Press = normal;
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
            if (!InputManager.Mouse.IsButtonDown(MouseButton.Left))
            {
                _hover = true;
            }
            else
            {
                _hover = false;
            }

            if (InputManager.Mouse.WasButtonJustPressed(MouseButton.Left))
            {
                _pressed = true;
            }

            if (InputManager.Mouse.WasButtonJustReleased(MouseButton.Left) && _pressed)
            {
                if (_click != null)
                {
                    _click.Invoke();
                }
                else
                {
                    Clicked();
                }

                _hover = false;
                _pressed = false;
                _sr.Sprite = Normal;
            }
        }
        else
        {
            _sr.Sprite = Normal;
            _pressed = false;
            _hover = false;
        }

        if (_hover)
        {
            _sr.Sprite = Hover;
        }

        if (_pressed)
        {
            _sr.Sprite = Press;

        }
    }

    // when clicked
    public virtual void Clicked() { }
}
