using System;
using Microsoft.Xna.Framework;

namespace MyMonoGameLibrary.Tools;

// converts variables to other variables
public class Converter
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
}
