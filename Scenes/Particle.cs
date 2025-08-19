using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MyMonoGameLibrary.Scenes;

public class Particle : BehaviorComponent
{
    public float Timer { get; set; }

    public override void Update(GameTime gameTime)
    {
        Timer -= SceneTools.DeltaTime;

        if (Timer <= 0)
        {
            SceneTools.Destroy(Parent);
        }
    }
}
