using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MyMonoGameLibrary.Scenes;

public interface IBehavior
{
    // start the script
    public void Start();

    // update the script
    //
    // param: gameTime - get the game time
    public void Update(GameTime gameTime);

    // update the script after update
    //
    // param: gameTime - get the game time
    public void LateUpdate(GameTime gameTime);
}
