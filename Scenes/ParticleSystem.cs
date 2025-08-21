using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MyMonoGameLibrary.Tools;
using MyMonoGameLibrary;
using MyMonoGameLibrary.Graphics;
using System.Linq;
using System.Diagnostics;

namespace MyMonoGameLibrary.Scenes;

// base class for particle system, will not have rigidbody
public class ParticleSystem : BehaviorComponent
{
    // variables and properties
    public Sprite[] Sprites { get;  set; }
    public bool Gravity { get; set; }
    public bool Manual {  get; set; }
    public bool Infinite { get; set; }
    public int Reps { get; set; }
    public float Rate { get;  set; }
    public int Particles { get;  set; }
    public float Rotation { get;  set; }
    public float Spread {  get;  set; }
    public float Velocity { get;  set; }
    public float Time { get; set; }
    public float XOffset { get; set; }
    public float YOffset { get; set; }
    public bool FlipX { get; set; }
    public bool FlipY { get; set; }

    private float _playTimer = 0;
    
    // standard constructor
    public ParticleSystem(Sprite[] sprites, bool gravity, bool manual, bool infinite, int reps, 
        float rate, int particles, float rotation, float spread, float velocity, float time, 
        float xOffset, float yOffset, bool flipX, bool flipY)
    {
        Sprites = sprites;
        Gravity = gravity;
        Manual = manual;
        Infinite = infinite;
        Reps = reps;
        Rate = rate;
        Particles = particles;
        Rotation = rotation;
        Spread = spread;
        Velocity = velocity;
        Time = time;
        XOffset = xOffset;
        YOffset = yOffset;
        FlipX = flipX;
        FlipY = flipY;
    }

    // constructor for projectiles
    public ParticleSystem(Sprite[] sprites, bool gravity, int particles, float spread, float velocity, float time)
    {
        Sprites = sprites;
        Gravity = gravity;
        Manual = true;
        Particles = particles;
        Spread = spread;
        Velocity = velocity;
        Time = time;
    }

    // constructor for footsteps
    public ParticleSystem(Sprite[] sprites, bool gravity, float rate, int particles, float rotation, float spread, float velocity, float time,
        float xOffset, float yOffset)
    {
        Sprites = sprites;
        Gravity = gravity;
        Manual = false;
        Infinite = true;
        Rate = rate;
        Particles = particles;
        Rotation = rotation;
        Spread = spread;
        Velocity = velocity;
        Time = time;
        XOffset = xOffset;
        YOffset = yOffset;
    }

    // the base particle the system uses
    //
    // return: the base particle
    private PrefabInstance BaseParticle()
    {
        Component[] components =
        [
            new Transform(),
            new SpriteRenderer(),
            new Rigidbody(false, false),
            new Particle()
        ];

        return new PrefabInstance("particle", components);
    }

    public override void Update(GameTime gameTime)
    {
        // automatically play particles
        if (Core.Particles)
        {
            if (!Manual)
            {
                _playTimer -= SceneTools.DeltaTime;

                if (_playTimer <= 0)
                {
                    PlayParticles();

                    // destroy game object if needed
                    if (!Infinite)
                    {
                        Reps--;
                        if (Reps <= 0)
                        {
                            SceneTools.Destroy(Parent);
                        }
                    }

                    _playTimer = Rate;
                }
            }
        }
    }

    // manually play particles
    public void Play()
    {
        if (Manual && Core.Particles)
            PlayParticles();
    }

    // play the particles
    private void PlayParticles()
    {
        // apply flips
        float rot = Rotation;

        float x = XOffset;
        if (FlipX)
        {
            x = -XOffset;
            rot = 180 - Rotation; 
        }

        float y = YOffset;
        if (FlipY)
        {
            y = -YOffset;
            rot = -Rotation;
        }

        // play particles
        for (int i = 0; i < Particles; i++)
        {
            // calcualte direction of particle
            float rotation = MathHelper.ToRadians(rot + (((float)Core.Random.NextDouble() - 0.5f) * Spread));
            Vector2 direction = new((float)MathF.Cos(rotation), (float)MathF.Sin(rotation));

            // particle settings
            Particle particle = SceneTools.Instantiate(BaseParticle(),
                new Vector2(Transform.position.X + x, Transform.position.Y + y), (float)Core.Random.NextDouble() * 360).GetComponent<Particle>();

            particle.Parent.Rigidbody.XVelocity = direction.X * Velocity;
            particle.Parent.Rigidbody.YVelocity = direction.Y * Velocity;
            particle.Parent.Rigidbody.UseGravity = Gravity;
            particle.Timer = Time;

            // choose the random sprite
            int sprite = Core.Random.Next(Sprites.Length);
            ((SpriteRenderer)particle.Parent.Renderer).Sprite = Sprites[sprite];
        }
    }
}
