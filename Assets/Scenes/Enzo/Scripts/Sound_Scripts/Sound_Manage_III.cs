using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SoundType
{

    //Sound goes here.
    RED, BLUE, GREEN, ORANGE, YELLOW, WHITE, BLACK

}

[RequireComponent(typeof(AudioSource))]
public class Sound_Manage_III : MonoBehaviour
{

    [SerializeField] private AudioClip[] _Sound_Clips;
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

        _Instance._Audio_Source.PlayOneShot(_Instance._Sound_Clips[(int) _Sound], _Volume);

    }

}
