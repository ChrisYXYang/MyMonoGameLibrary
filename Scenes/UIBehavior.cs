using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MyMonoGameLibrary.UI;

namespace MyMonoGameLibrary.Scenes;

// base class for UI element behavior
public abstract class UIBehavior
{
    public BaseUI Parent { get; private set; }

    // initialization for the component
    //
    // param: parent - parent ui element
    public void Initialize(BaseUI parent)
    {
        Parent = parent;
    }

    public virtual void Start()
    {
    }

    public virtual void Update(GameTime gameTime)
    {
        if (Parent is SpriteUI sprite)
        {
            if (sprite.Animator != null)
            {
                sprite.Animator.Update(gameTime);
            }
        }
    }
}
