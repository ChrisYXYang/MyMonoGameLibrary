using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MyMonoGameLibrary.Graphics;
using MyMonoGameLibrary.Scenes;

namespace MyMonoGameLibrary.UI;

public class UIAnimator
{
    // variables and properties
    private SpriteUI _parent;
    private int _currentFrame = 0;
    private TimeSpan _elapsed;
    private Animation _animation;
    public Animation Animation
    {
        get => _animation;
        set
        {
            _animation = value;

            if (value != null)
            {
                _parent.Sprite = _animation.Frames[0];
                _currentFrame = 0;
                _elapsed = TimeSpan.Zero;
            }
        }
    }

    // constructor
    //
    // param: parent- the parent
    // param: animation - the animation
    public UIAnimator(SpriteUI parent, Animation animation)
    {
        _parent = parent;
        _animation = animation;
    }

    // updates the animation
    //
    // param: gameTime - game timing values
    public void Update(GameTime gameTime)
    {
        if (Animation != null)
        {
            _elapsed += gameTime.ElapsedGameTime;

            // change to next frame until frame is up to date
            while (_elapsed >= _animation.Delay)
            {
                _elapsed -= _animation.Delay;
                _currentFrame++;

                if (_currentFrame >= _animation.Frames.Count)
                {
                    _currentFrame = 0;
                }

                _parent.Sprite = _animation.Frames[_currentFrame];
            }
        }
        else
        {
            _parent.Sprite = _parent.DefaultSprite;
        }
    }
}
