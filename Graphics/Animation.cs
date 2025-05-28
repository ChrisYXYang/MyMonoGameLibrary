using System;
using System.Collections.Generic;

namespace MyMonoGameLibrary.Graphics;

public class Animation
{
    // variables and properties
    public List<Sprite> Frames { get; set; }
    public TimeSpan Delay { get; set; }

    // constructor
    //
    // param: frames - frames of animation
    // param: delay - delay between frames
    public Animation(List<Sprite> frames, TimeSpan delay)
    {
        Frames = frames;
        Delay = delay;
    }
}
