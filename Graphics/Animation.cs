using System;
using System.Collections.Generic;

namespace MyMonoGameLibrary.Graphics;

public class Animation
{
    // variables and properties
    public List<Sprite> Frames { get; private set; }
    public TimeSpan Delay { get; set; } = TimeSpan.FromMilliseconds(100);

    // constructor with specified frames
    //
    // param: frames - frames of animation
    public Animation(List<Sprite> frames)
    {
        Frames = frames;
    }

    // constructor with specified frames and delay
    //
    // param: frames - frames of animation
    // param: delay - delay between frames
    public Animation(List<Sprite> frames, TimeSpan delay)
    {
        Frames = frames;
        Delay = delay;
    }


}
