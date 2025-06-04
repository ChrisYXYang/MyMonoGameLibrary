using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using MyMonoGameLibrary.Input;  

namespace MyMonoGameLibrary;

// Core is an extension of the Game class that simplifies the code needed in Game1 by handling
// tasks such as creating GraphicsDeviceManager and SpriteBatch
public class Core : Game
{
    // variables and properties
    internal static Core s_instance;
    
    public static Core Instance => s_instance;
    public static GraphicsDeviceManager Graphics { get; private set; }
    public static SpriteBatch SpriteBatch { get; private set; }
    public static new GraphicsDevice GraphicsDevice { get; private set; }
    public static new ContentManager Content { get; private set; }

    // constructor
    //
    // param: title - title of window
    // param: width - width of screen
    // param: height - height of screen
    // param: fullScreen - full screen or not
    public Core(string title, int width, int height, bool fullScreen)
    {
        // ensure only one core
        if (s_instance != null)
        {
            throw new InvalidOperationException("only single core");
        }

        // store reference for core
        s_instance = this;

        // create new graphics device manager
        Graphics = new GraphicsDeviceManager(this);

        // set grpahics defaults
        Graphics.PreferredBackBufferWidth = width;
        Graphics.PreferredBackBufferHeight = height;
        Graphics.IsFullScreen = fullScreen;
        Graphics.ApplyChanges();

        // window title
        Window.Title = title;

        // set content manager to base Game's
        Content = base.Content;

        // root directory for content
        Content.RootDirectory = "Content";

        // mouse visible by default
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        base.Initialize();

        // set graphics device to base Game's
        GraphicsDevice = base.GraphicsDevice;

        // create sprite batch instance
        SpriteBatch = new SpriteBatch(GraphicsDevice);
    }
}
