using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace MyMonoGameLibrary;

// controller for the player
public class PlayerController : Component
{
    // variables and properties
    private float _moveSpeed;

    private Transform _transform;
    private Vector2 _direction = new Vector2();

    // constructor
    //
    // param: attributes - attributes of component
    public PlayerController(Dictionary<string, string> attributes)
    {
        _moveSpeed = float.Parse(attributes["moveSpeed"]);
        Debug.WriteLine("hi");
    }

    // initialize
    //
    // param: parent - parent game object
    public override void Initialize(GameObject parent)
    {
        base.Initialize(parent);
        _transform = GetComponent<Transform>();
    }

    // cmove the player
    //
    // param: gameTime - get the game time
    public void Move(GameTime gameTime)
    {
        float speed = _moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

        // get movement direction
        if (Core.Input.Keyboard.IsKeyDown(Keys.W) && Core.Input.Keyboard.IsKeyDown(Keys.S))
        {
            _direction.Y = 0;
        }
        else if (Core.Input.Keyboard.IsKeyDown(Keys.W))
        {
            _direction.Y = -1;
        }
        else if (Core.Input.Keyboard.IsKeyDown(Keys.S))
        {
            _direction.Y = 1;
        }
        else
        {
            _direction.Y = 0;
        }

        if (Core.Input.Keyboard.IsKeyDown(Keys.A) && Core.Input.Keyboard.IsKeyDown(Keys.D))
        {
            _direction.X = 0;
        }
        else if (Core.Input.Keyboard.IsKeyDown(Keys.A))
        {
            _direction.X = -1;
        }
        else if (Core.Input.Keyboard.IsKeyDown(Keys.D))
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
