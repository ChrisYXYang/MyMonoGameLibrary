using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace MyMonoGameLibrary.Audio;

public class AudioController : IDisposable
{
    // Tracks sound effect instances created so they can be paused, unpaused, and/or disposed.
    private readonly List<SoundEffectInstance> _activeSoundEffectInstances;

    private float _previousMasterVol;

    /// <summary>
    /// Gets a value that indicates if audio is muted.
    /// </summary>
    public bool IsMuted { get; private set; }

    // gets or sets global volume of all sound
    private float _masterVolume = 1f;
    public float MasterVolume
    {
        get
        {
            if (IsMuted)
            {
                return 0f;
            }

            return _masterVolume;
        }
        set
        {
            if (IsMuted)
            {
                return;
            }

            _masterVolume = Math.Clamp(value, 0f, 1f);
            MediaPlayer.Volume = SongVolume * MasterVolume;
            SoundEffect.MasterVolume = SoundEffectVolume * MasterVolume;
        }
    }

    /// <summary>
    /// Gets or Sets the global volume of songs.
    /// </summary>
    /// <remarks>
    /// If IsMuted is true, the getter will always return back 0.0f and the
    /// setter will ignore setting the volume.
    /// </remarks>
    private float _songVolume = 1;
    public float SongVolume
    {
        get
        {
            if (IsMuted)
            {
                return 0.0f;
            }

            return _songVolume;
        }
        set
        {
            if (IsMuted)
            {
                return;
            }

            _songVolume = Math.Clamp(value, 0f, 1f);
            MediaPlayer.Volume = _songVolume * MasterVolume;
        }
    }

    /// <summary>
    /// Gets or Sets the global volume of sound effects.
    /// </summary>
    /// <remarks>
    /// If IsMuted is true, the getter will always return back 0.0f and the
    /// setter will ignore setting the volume.
    /// </remarks>
    private float _sfxVolume = 1;
    public float SoundEffectVolume
    {
        get
        {
            if (IsMuted)
            {
                return 0.0f;
            }

            return _sfxVolume;
        }
        set
        {
            if (IsMuted)
            {
                return;
            }

            _sfxVolume = Math.Clamp(value, 0f, 1f);
            SoundEffect.MasterVolume = _sfxVolume * MasterVolume;
        }
    }

    /// <summary>
    /// Gets a value that indicates if this audio controller has been disposed.
    /// </summary>
    public bool IsDisposed { get; private set; }

    /// <summary>
    /// Creates a new audio controller instance.
    /// </summary>
    public AudioController()
    {
        _activeSoundEffectInstances = new List<SoundEffectInstance>();

    }

    // Finalizer called when object is collected by the garbage collector.
    ~AudioController() => Dispose(false);

    /// <summary>
    /// Updates this audio controller.
    /// </summary>
    public void Update()
    {
        for (int i = _activeSoundEffectInstances.Count - 1; i >= 0; i--)
        {
            SoundEffectInstance instance = _activeSoundEffectInstances[i];

            if (instance.State == SoundState.Stopped)
            {
                if (!instance.IsDisposed)
                {
                    instance.Dispose();
                }
                _activeSoundEffectInstances.RemoveAt(i);
            }
        }
    }

    /// <summary>
    /// Plays the given sound effect.
    /// </summary>
    /// <param name="soundEffect">The sound effect to play.</param>
    /// <returns>The sound effect instance created by this method.</returns>
    public SoundEffectInstance PlaySoundEffect(SoundEffect soundEffect)
    {
        return PlaySoundEffect(soundEffect, 1.0f, 1.0f, 0.0f, false);
    }

    /// <summary>
    /// Plays the given sound effect with the specified properties.
    /// </summary>
    /// <param name="soundEffect">The sound effect to play.</param>
    /// <param name="volume">The volume, ranging from 0.0 (silence) to 1.0 (full volume).</param>
    /// <param name="pitch">The pitch adjustment, ranging from -1.0 (down an octave) to 0.0 (no change) to 1.0 (up an octave).</param>
    /// <param name="pan">The panning, ranging from -1.0 (left speaker) to 0.0 (centered), 1.0 (right speaker).</param>
    /// <param name="isLooped">Whether the the sound effect should loop after playback.</param>
    /// <returns>The sound effect instance created by playing the sound effect.</returns>
    /// <returns>The sound effect instance created by this method.</returns>
    public SoundEffectInstance PlaySoundEffect(SoundEffect soundEffect, float volume, float pitch, float pan, bool isLooped)
    {
        // Create an instance from the sound effect given.
        SoundEffectInstance soundEffectInstance = soundEffect.CreateInstance();

        // Apply the volume, pitch, pan, and loop values specified.
        soundEffectInstance.Volume = volume;
        soundEffectInstance.Pitch = pitch;
        soundEffectInstance.Pan = pan;
        soundEffectInstance.IsLooped = isLooped;

        // Tell the instance to play
        soundEffectInstance.Play();

        // Add it to the active instances for tracking
        _activeSoundEffectInstances.Add(soundEffectInstance);

        return soundEffectInstance;
    }

    /// <summary>
    /// Plays the given song.
    /// </summary>
    /// <param name="song">The song to play.</param>
    /// <param name="isRepeating">Optionally specify if the song should repeat.  Default is true.</param>
    public void PlaySong(Song song, bool isRepeating = true)
    {
        // Check if the media player is already playing, if so, stop it.
        // If we do not stop it, this could cause issues on some platforms
        if (MediaPlayer.State == MediaState.Playing)
        {
            MediaPlayer.Stop();
        }

        MediaPlayer.Play(song);
        MediaPlayer.IsRepeating = isRepeating;
    }

    /// <summary>
    /// Pauses all audio.
    /// </summary>
    public void PauseAudio()
    {
        // Pause any active songs playing.
        MediaPlayer.Pause();

        // Pause any active sound effects.
        foreach (SoundEffectInstance soundEffectInstance in _activeSoundEffectInstances)
        {
            soundEffectInstance.Pause();
        }
    }

    /// <summary>
    /// Resumes play of all previous paused audio.
    /// </summary>
    public void ResumeAudio()
    {
        // Resume paused music
        MediaPlayer.Resume();

        // Resume any active sound effects.
        foreach (SoundEffectInstance soundEffectInstance in _activeSoundEffectInstances)
        {
            soundEffectInstance.Resume();
        }
    }

    /// <summary>
    /// Mutes all audio.
    /// </summary>
    public void MuteAudio()
    {
        _previousMasterVol = MasterVolume;
        MasterVolume = 0;
        IsMuted = true;
    }

    /// <summary>
    /// Unmutes all audio to the volume level prior to muting.
    /// </summary>
    public void UnmuteAudio()
    {
        MasterVolume = _previousMasterVol;
        IsMuted = false;
    }

    /// <summary>
    /// Toggles the current audio mute state.
    /// </summary>
    public void ToggleMute()
    {
        if (IsMuted)
        {
            UnmuteAudio();
        }
        else
        {
            MuteAudio();
        }
    }

    /// <summary>
    /// Disposes of this audio controller and cleans up resources.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Disposes this audio controller and cleans up resources.
    /// </summary>
    /// <param name="disposing">Indicates whether managed resources should be disposed.</param>
    protected void Dispose(bool disposing)
    {
        if (IsDisposed)
        {
            return;
        }

        if (disposing)
        {
            foreach (SoundEffectInstance soundEffectInstance in _activeSoundEffectInstances)
            {
                soundEffectInstance.Dispose();
            }
            _activeSoundEffectInstances.Clear();
        }

        IsDisposed = true;
    }
}
