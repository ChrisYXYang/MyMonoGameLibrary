using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MyMonoGameLibrary.Graphics;

namespace MyMonoGameLibrary.Scenes;

public interface IAnimator
{
    public Animation Animation { get; set; }

    // updates the animation
    //
    // param: gameTime - game timing values
    public void Update(GameTime gameTime);
}
