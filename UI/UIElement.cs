using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonoGameLibrary.UI;

// this class represents every UI element in the game.
public abstract class UIElement
{
    private List<BaseUI> _children;

    // constructor
    //
    // param: children - children UI elements
    public UIElement(List<BaseUI> children)
    {
        _children = children;

        if (children == null)
        {
            _children = new List<BaseUI>();
        }
    }

    // get all children
    //
    // return: array of children
    public BaseUI[] GetChildren()
    {
        BaseUI[] output = new BaseUI[_children.Count];
        _children.CopyTo(output);
        return output;
    }

    // get a child
    //
    // param: index - index of child in list
    // return: chosen child based on index
    public BaseUI GetChild(int index)
    {
        return _children[index];
    }
}
