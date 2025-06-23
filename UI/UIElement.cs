using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonoGameLibrary.UI;

// this class represents every UI element in the game.
public abstract class UIElement
{
    // number of children
    public int ChildCount { get => _children.Count; }

    // children of elemente
    private List<BaseUI> _children = [];

    // add a child
    //
    // param: child - child to add
    public void AddChild(BaseUI child)
    {
        _children.Add(child);
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
