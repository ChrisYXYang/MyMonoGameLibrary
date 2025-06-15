using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonoGameLibrary.Scenes;

// for core components like sprite renderer, colliders, etc. (not behavior scripts). Gives the 
// functionality of initialization.
public class CoreComponent : Component
{
    // intitialize the component
    //
    // parent - parent game object
    public new virtual void Initialize(GameObject parent)
    {
        base.Initialize(parent);
    }
}
