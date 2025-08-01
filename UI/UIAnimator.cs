using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MyMonoGameLibrary.Graphics;
using MyMonoGameLibrary.Scenes;

namespace MyMonoGameLibrary.UI;

public class UIAnimator : CoreComponent, IAnimator
{
    // variables and properties
    public Animation Animation
    {
        get => _animation;
        set
        {
            if (_animation == value)
                return;

            _animation = value;

            if (value != null)
            {
                _image.Sprite = _animation.Frames[0];
                _currentFrame = 0;
                _elapsed = TimeSpan.Zero;
            }
        }
    }

    public float Speed { get; set; }

    private UISprite _image;
    private int _currentFrame = 0;
    private TimeSpan _elapsed;
    private Animation _animation;

    // constructor
    //
    // param: parent- the parent
    // param: animation - the animation
    public UIAnimator(Animation animation)
    {
        _animation = animation;
    }

    // initialize
    //
    // param: parent - parent game object
    public override void Initialize(GameObject parent)
    {
        base.Initialize(parent);
        _image = GetComponent<UISprite>();
        _image.Sprite = _animation.Frames[0];
    }

    // updates the animation
    //
    // param: gameTime - game timing values
    public void Update(GameTime gameTime)
    {
        if (Animation != null)
        {
            _elapsed += gameTime.ElapsedGameTime * Speed;

            // change to next frame until frame is up to date
            while (_elapsed >= _animation.Delay)
            {
                _elapsed -= _animation.Delay;
                _currentFrame++;

                if (_currentFrame >= _animation.Frames.Count)
                {
                    _currentFrame = 0;
                }

                _image.Sprite = _animation.Frames[_currentFrame];
            }
        }
        else
        {
            _image.Sprite = _image.DefaultSprite;
        }
    }
}
