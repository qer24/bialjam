using FMODUnity;
using System;
using FMOD.Studio;
using UnityEngine;

public static class AudioManager
{
    /// <summary>Creates a 2D Oneshot Sound Instance, use 'trimBeginning = true' if you are using [FMODUnity.EventRef] attribute</summary>
    public static void Play(string sound, bool trimBeginning = true)
    {
        if(string.IsNullOrEmpty(sound)) return;
        
        if (trimBeginning)
            sound = sound.Substring(7);

        RuntimeManager.PlayOneShot($"event:/{sound}");
    }

    /// <summary>Creates a 3D Oneshot Sound Instance, use 'trimBeginning = true' if you are using [FMODUnity.EventRef] attribute</summary>
    public static void Play(string sound, Vector3 position, bool trimBeginning = true)
    {
        if(string.IsNullOrEmpty(sound)) return;

        if (trimBeginning)
            sound = sound.Substring(7);

        RuntimeManager.PlayOneShot($"event:/{sound}", position);
    }
    
    /// <summary>Creates a 3D Oneshot Sound Instance, use 'trimBeginning = true' if you are using [FMODUnity.EventRef] attribute</summary>
    public static void PlayAttached(string sound, GameObject gameObject, bool trimBeginning = true)
    {
        if(string.IsNullOrEmpty(sound)) return;

        if (trimBeginning)
            sound = sound.Substring(7);

        RuntimeManager.PlayOneShotAttached($"event:/{sound}", gameObject);
    }
    
    /// <summary>Creates a 2D Oneshot Sound Instance, use 'trimBeginning = true' if you are using [FMODUnity.EventRef] attribute</summary>
    public static EventInstance CreateInstance(string sound, bool trimBeginning = true)
    {
        if(string.IsNullOrEmpty(sound)) return new EventInstance();
        
        if (trimBeginning)
            sound = sound.Substring(7);

        var instance = RuntimeManager.CreateInstance($"event:/{sound}");
        instance.start();
        instance.release();
        return instance;
    }

    /// <summary>Creates a 3D Oneshot Sound Instance, use 'trimBeginning = true' if you are using [FMODUnity.EventRef] attribute</summary>
    public static EventInstance CreateInstance(string sound, Vector3 position, bool trimBeginning = true)
    {
        if(string.IsNullOrEmpty(sound)) return new EventInstance();
        
        if (trimBeginning)
            sound = sound.Substring(7);

        var instance = RuntimeManager.CreateInstance($"event:/{sound}");
        instance.set3DAttributes(position.To3DAttributes());
        instance.start();
        instance.release();
        return instance;
    }
}

public static class EventReferenceExtensions
{
    /// <summary>Creates a 2D Oneshot Sound Instance</summary>
    public static void Play(this EventReference eventRef)
    {
        if (eventRef.IsNull) return;
        
        RuntimeManager.PlayOneShot(eventRef);
    }

    /// <summary>Creates a 3D Oneshot Sound Instance</summary>
    public static void Play(this EventReference eventRef, Vector3 position)
    {
        if (eventRef.IsNull) return;
        
        RuntimeManager.PlayOneShot(eventRef, position);
    }
    
    /// <summary>Creates an attached 3D Oneshot Sound Instance</summary>
    public static void PlayAttached(this EventReference eventRef, GameObject gameObject)
    {
        if (eventRef.IsNull) return;
        
        RuntimeManager.PlayOneShotAttached(eventRef.Guid, gameObject);
    }
    
    public static EventInstance CreateInstance(this EventReference eventRef)
    {
        var instance = RuntimeManager.CreateInstance(eventRef);
        instance.start();
        instance.release();
        return instance;
    }
}