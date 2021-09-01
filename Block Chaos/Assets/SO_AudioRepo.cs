using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "SoundRepo", menuName = "ScriptableObjects/SoundRepo")]

public class SO_AudioRepo : ScriptableObject
{
    public SoundStructure[] soundRepo;

    public SoundStructure getSound(string soundName)
    {

        SoundStructure sound = Array.Find(soundRepo, element => element.name == soundName);
        return sound;
    }
    
}
