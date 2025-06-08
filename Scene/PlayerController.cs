using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using MyMonoGameLibrary.Input;

namespace MyMonoGameLibrary.Scene;

// controller for the player
public class PlayerController : Component
{
    // variables and properties
    public float MoveSpeed { get; set; }

    private Transform _transform;
    private Vector2 _direction = new Vector2();

    // constructor
    //
    // param: speed - movement speed
    public PlayerController(float speed)
    {
        MoveSpeed = speed;
    }

    // initialize
    //
    // param: parent - parent game object
    public override void Initialize(GameObject parent)
    {
        base.Initialize(parent);
        _transform = GetComponent<Transform>();
    }

    // move the player
    //
    // param: gameTime - get the game time
    public void Move(GameTime gameTime)
    {
        float speed = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

        // get movement direction
        if (InputManager.Keyboard.IsKeyDown(Keys.W) && InputManager.Keyboard.IsKeyDown(Keys.S))
        {
            _direction.Y = 0;
        }
        else if (InputManager.Keyboard.IsKeyDown(Keys.W))
        {
            _direction.Y = -1;
        }
        else if (InputManager.Keyboard.IsKeyDown(Keys.S))
        {
            _direction.Y = 1;
        }
        else
        {
            _direction.Y = 0;
        }

        if (InputManager.Keyboard.IsKeyDown(Keys.A) && InputManager.Keyboard.IsKeyDown(Keys.D))
        {
            _direction.X = 0;
        }
        else if (InputManager.Keyboard.IsKeyDown(Keys.A))
        {
            _direction.X = -1;
        }
        else if (InputManager.Keyboard.IsKeyDown(Keys.D))
        {
            _direction.X = 1;
        }
        else
        {
            _direction.X = 0;
        }

        // move player
        if (!_direction.Equals(Vector2.Zero))
            _direction = Vector2.Normalize(_direction);

        _transform.position += _direction * speed;
    }
}
