using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using MyMonoGameLibrary.Scenes;
using MyMonoGameLibrary.Tilemap;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using MyMonoGameLibrary.Input;
using MyMonoGameLibrary.Graphics;

namespace MyMonoGameLibrary;

// Core is an extension of the Game class that simplifies the code needed in Game1 by handling
// important tasks such as updating.
public class Core : Game
{
    // variables and properties
    internal static Core s_instance;
    public static Core Instance => s_instance;

    private static Scene s_activeScene;
    private static Scene s_nextScene;

    public static GraphicsDeviceManager Graphics { get; private set; }
    public static SpriteBatch SpriteBatch { get; private set; }
    public static new GraphicsDevice GraphicsDevice { get; private set; }
    public static new ContentManager Content { get; private set; }

    public static SpriteLibrary GlobalSpriteLibrary { get; private set; }

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

        // mouse visible by default
        IsMouseVisible = true;

        // window title
        Window.Title = title;

        // set content manager to base Game's
        Content = base.Content;

        // root directory for content
        Content.RootDirectory = "Content";

        // set sprite library
        GlobalSpriteLibrary = new SpriteLibrary();
    }

    protected override void Initialize()
    {
        // set graphics device to base Game's
        GraphicsDevice = base.GraphicsDevice;

        // create sprite batch instance
        SpriteBatch = new SpriteBatch(GraphicsDevice);

        base.Initialize();
    }

    protected override void Update(GameTime gameTime)
    {
        InputManager.Update();

        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
        {
            Exit();
        }

        // if there is next scene waiting to switch to, transition to that scene
        if (s_nextScene != null)
        {
            TransitionScene();
        }

        // if active scene, update it
        if (s_activeScene != null)
        {
            s_activeScene.Update(gameTime);
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        // if there is active scene, draw it
        if (s_activeScene != null)
        {
            s_activeScene.Draw(gameTime);
            SpriteBatch.End();
        }

        base.Draw(gameTime);
    }

    public static void ChangeScene(Scene next)
    {
        // Only set the next scene value if it is not the same
        // instance as the currently active scene.
        if (s_activeScene != next)
        {
            s_nextScene = next;
        }
    }

    private static void TransitionScene()
    {
        // If there is an active scene, dispose of it
        if (s_activeScene != null)
        {
            s_activeScene.Dispose();
        }

        // Force the garbage collector to collect to ensure memory is cleared
        GC.Collect();

        // Change the currently active scene to the new scene
        s_activeScene = s_nextScene;

        // Null out the next scene value so it does not trigger a change over and over.
        s_nextScene = null;

        // If the active scene now is not null, initialize it.
        // Remember, just like with Game, the Initialize call also calls the
        // Scene.LoadContent
        if (s_activeScene != null)
        {
            s_activeScene.Initialize();
        }
    }
}
