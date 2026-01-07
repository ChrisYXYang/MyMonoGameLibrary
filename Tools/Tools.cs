using System;
using Microsoft.Xna.Framework;
using MyMonoGameLibrary.Scenes;

namespace MyMonoGameLibrary.Tools;

// some useful tools
public static class Tools
{
    // converts string to vector2
    //
    // param: input - string to convert
    // return: Vector2 from input string
    public static Vector2 ParseVector2(string input)
    {
        string[] individual = input.Split(",");
        return new Vector2(float.Parse(individual[0].Trim()), float.Parse(individual[1].Trim()));
    }

    // converts sprite pixels to game units
    //
    // param: pixels - number of sprite pixels
    // return: game units
    public static float PixelToUnit(float pixels)
    {
        return pixels / Camera.SpritePixelsPerUnit;
    }
    
    // 1/2 chance
    //
    // return: half chance true/false
    public static bool HalfChance()
    {
        return Core.Random.Next(2) == 1; 
    }

    // random float
    //
    // param: min (inclusive)
    // param: max (exclusive)
    // return: random float between them
    public static float RandomFloat(float min, float max)
    {
        return ((float)Core.Random.NextDouble() * (max - min)) + min;

    }
}
