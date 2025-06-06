using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MyMonoGameLibrary.Scene;

namespace MyMonoGameLibrary.Behavior;

public class Slime : Component, IBehavior
{
    private SpriteRenderer _spriteRenderer;
    private int _collisions = 0;

    public Slime(Dictionary<string, string> attributes)
    {

    }

    public override void Initialize(GameObject parent)
    {
        base.Initialize(parent);
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnCollisionEnter(IRectCollider other)
    {
        _spriteRenderer.Color = Color.Red;
        _collisions++;
    }

    public void OnCollisionExit(IRectCollider other)
    {
        _collisions--;

        if (_collisions == 0)
        {
            _spriteRenderer.Color = Color.White;
        }
    }

    public void OnCollisionStay(IRectCollider other)
    {
        
    }
}
