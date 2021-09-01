using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Listener_Audio : MonoBehaviour
{
    public List<string> soundList;
    public SO_AudioRepo audioRepo;
    public List<string> playOnStart;

    [HideInInspector]
    public string dieAfterPlaySound;
    private List<SoundStructure> createdSoundList;
    // Start is called before the first frame update
    private void Awake()
    {
        createdSoundList = new List<SoundStructure>(); 

        if (soundList == null)
        {
            soundList = new List<string>();
        }

        if (playOnStart == null)
        {
            playOnStart = new List<string>();
        }
    }
    private void Start()
    {
        if (soundList == null)
        {
            if (DebugMode.debugMode)
            {
                Debug.LogWarning("Sound List Empty on GO: " + gameObject.name);
            }
            
        }

        foreach (string soundName in soundList)
        {
            SoundStructure sound = audioRepo.getSound(soundName);
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.spatialBlend = sound.spatialBlend;
            sound.source.pitch = sound.pitch;
            sound.source.playOnAwake = sound.playOnAwake;
            sound.source.loop = sound.loop;
            createdSoundList.Add(sound);
        }

        //Play starting sound
        if (playOnStart.Any())
        {
            foreach (string soundName in playOnStart)
            {
                PlayOneShotSound(gameObject, soundName);
            }
        }

        if (!String.IsNullOrEmpty(dieAfterPlaySound))
        {
            
            KillYourselfAfterPlay();
        }
        
    }
    private void OnEnable()
    {
        AudioManager.onPlaySound += PlaySound;
        AudioManager.onPlayOneShotSound += PlayOneShotSound;
        AudioManager.onStopSound += StopSound;
    }

    private void OnDisable()
    {
        AudioManager.onPlaySound -= PlaySound;
        AudioManager.onPlayOneShotSound -= PlayOneShotSound;
        AudioManager.onStopSound -= StopSound;
    }

    private void StopSound(GameObject gameObj, string soundName)
    {
        if (gameObj != gameObject)
        {
            return;
        }
        SoundStructure audio = createdSoundList.Find(sound => sound.name == soundName);

        if (audio == null || audio.source == null)
        {
            Debug.LogWarning("Sound : " + soundName + " not found!");
            return;
        }

        audio.source.Stop();
    }

    private void PlayOneShotSound(GameObject gameObj, string soundName)
    {
        if (gameObj != gameObject)
        {
            return;
        }
        SoundStructure audio = createdSoundList.Find(sound => sound.name == soundName);

        if (audio == null || audio.source == null)
        {
            Debug.LogWarning("Sound : " + soundName + " not found!");
            return;
        }

        audio.source.volume = audio.volume;
        audio.source.pitch = audio.pitch;
        audio.source.loop = audio.loop;
        audio.source.spatialBlend = audio.spatialBlend;

        audio.source.PlayOneShot(audio.source.clip);
    }

    private void PlaySound(GameObject gameObj, string soundName)
    {

        if (gameObj != gameObject)
        {
            return;
        }
        SoundStructure audio = createdSoundList.Find(sound => sound.name == soundName);

        if (audio == null || audio.source == null)
        {
            Debug.LogWarning("Sound : " + soundName + " not found!");
            return;
        }

        audio.source.volume = audio.volume;
        audio.source.pitch = audio.pitch;
        audio.source.loop = audio.loop;
        audio.source.spatialBlend = audio.spatialBlend;

        audio.source.Play();
    }

    public void KillYourselfAfterPlay()
    {
        SoundStructure audio = createdSoundList.Find(sound => sound.name == dieAfterPlaySound);
        Destroy(gameObject, audio.clip.length);
    }
}
