using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonoGameLibrary.Scene;

// interface for objects that can be drawn
public interface IRenderable
{
    // get the renderer
    //
    // return: the renderer
    public IRenderer GetRenderer();
}
