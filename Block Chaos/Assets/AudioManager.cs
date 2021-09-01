using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;
public class AudioManager : MonoBehaviour
{
    public static Action<GameObject, string> onPlaySound;
    public static void PlaySound(GameObject gameObj, string soundName)
    {
        onPlaySound?.Invoke(gameObj, soundName);
    }

    public static Action<GameObject, string> onPlayOneShotSound;

    public static void PlayOneShotSound(GameObject gameObj, string soundName)
    {
        onPlayOneShotSound?.Invoke(gameObj, soundName);
    }

    public static Action<GameObject, string> onStopSound;

    public static void StopSound(GameObject gameObj, string soundName)
    {
        onStopSound?.Invoke(gameObj, soundName);
    }



}


[System.Serializable]
public class SoundStructure
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)] //Adds slider between the range
    public float volume = 1f;
    [Range(0f, 1f)]
    public float spatialBlend;
    [Range(.1f, 3f)]
    public float pitch = 1f;
    public bool playOnAwake;
    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
