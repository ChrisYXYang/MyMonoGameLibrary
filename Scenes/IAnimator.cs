using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MyMonoGameLibrary.Scenes;

// interface for animators
public interface IAnimator
{
    // updates the animation
    //
    // param: gameTime - game timing values
    public void Update(GameTime gameTime);
}
