using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MyMonoGameLibrary.Scenes;

// this is the base class for behavior components, and contains all the methods needed.
public abstract class BehaviorComponent : Component, IBehavior
{
    public virtual void Start()
    {
    }

    public virtual void Update(GameTime gameTime)
    {
    }

    public virtual void LateUpdate(GameTime gameTime)
    {
    }

    // what to do when just collided
    //
    // param: other - other collider
    public virtual void OnCollisionEnter(ICollider other)
    {

    }

    // what to do when collision exit
    //
    // param: other - other collider
    public virtual void OnCollisionExit(ICollider other)
    {

    }

    // what to do when ongoing collision
    //
    // param: other - other collider
    public virtual void OnCollisionStay(ICollider other)
    {

    }
}
