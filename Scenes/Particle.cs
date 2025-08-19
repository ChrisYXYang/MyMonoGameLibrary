using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MyMonoGameLibrary.Scenes;

// behavior for a particle
public class Particle : BehaviorComponent
{
    // variables and properties
    public float Timer { get; set; }

    public override void Update(GameTime gameTime)
    {
        // destroy timer
        Timer -= SceneTools.DeltaTime;

        if (Timer <= 0)
        {
            SceneTools.Destroy(Parent);
        }
    }
}
