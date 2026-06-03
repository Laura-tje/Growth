using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public enum SoundType
{

    //Sound goes here.
    RED, BLUE, GREEN, ORANGE, YELLOW, WHITE, BLACK

}

[RequireComponent(typeof(AudioSource)), ExecuteInEditMode]
public class Sound_Manage_III : MonoBehaviour
{

    public Sound_Clips[] _Sound_Clips;
    private static Sound_Manage_III _Instance;
    private AudioSource _Audio_Source;

    private void Awake()
    {

        _Instance = this;

    }

    private void Start()
    {

        _Audio_Source = GetComponent<AudioSource>();

    }

    public static void _Play_Sound(SoundType _Sound, float _Volume = 1.0f)
    {

        AudioClip[] clips = _Instance._Sound_Clips[(int) _Sound]._Sounds;
        AudioClip _Random_Clip = clips[UnityEngine.Random.Range(0, clips.Length)];
        _Instance._Audio_Source.PlayOneShot(_Random_Clip, _Volume);

        //_Instance._Audio_Source.PlayOneShot(_Instance._Sound_Clips[(int) _Sound], _Volume);

    }

    private void OnEnable()
    {

        string[] _Names = Enum.GetNames(typeof(SoundType));
        
        Array.Resize(ref _Sound_Clips, _Names.Length);

        for (int i = 0; i < _Sound_Clips.Length; i++)
        {

            _Sound_Clips[i]._Name = _Names[i];

        }

    } 

}

[Serializable]
public struct Sound_Clips
{

    public AudioClip[] _Sounds { get => _Sound_Clips; }

    [HideInInspector] public String _Name;

    [SerializeField] private AudioClip[] _Sound_Clips;

}