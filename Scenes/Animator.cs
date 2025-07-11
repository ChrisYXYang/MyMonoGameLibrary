using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MyMonoGameLibrary.Graphics;

namespace MyMonoGameLibrary.Scenes;

// responsible for animating game object
public class Animator : CoreComponent, IAnimator
{
    // variables and properties
    private SpriteRenderer _spriteManager;
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
                _spriteManager.Sprite = _animation.Frames[0];
                _currentFrame = 0;
                _elapsed = TimeSpan.Zero;
            }
        }
    }

    // constructor
    //
    // param: animation - the animation
    public Animator(Animation animation)
    {
        _animation = animation;
    }

    // initialize
    //
    // param: parent - parent game object
    public override void Initialize(GameObject parent)
    {
        base.Initialize(parent);
        _spriteManager = GetComponent<SpriteRenderer>();
        _spriteManager.Sprite = _animation.Frames[0];
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

                _spriteManager.Sprite = _animation.Frames[_currentFrame];
            }
        }
        else
        {
            _spriteManager.Sprite = _spriteManager.DefaultSprite;
        }
    }
}
