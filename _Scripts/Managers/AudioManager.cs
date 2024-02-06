using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource[] _audioSources;
    [SerializeField] private AudioMixer _audioMixer;

    void Start()
    {
        foreach(AudioSource a in _audioSources)
        {
            a.volume = .3f;
        }
        _audioSources[0].volume = .3f;
        _audioSources[1].volume = .5f;
        _audioSources[2].volume = .55f;
    }
}
