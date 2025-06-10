using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonoGameLibrary.Scenes;

// interface for objects that can be animated
internal interface IAnimatable
{
    // get the animator
    //
    // return: animator
    public IAnimator GetAnimator();
}
