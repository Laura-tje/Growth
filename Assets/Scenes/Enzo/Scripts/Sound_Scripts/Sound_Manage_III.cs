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
    public static Sound_Manage_III _Instance;
    private AudioSource _Audio_Source;

    private void Awake()
    {

        _Instance = this;

    }

    private void Start()
    {

        _Audio_Source = GetComponent<AudioSource>();

    }

    public static void _Play_Sound(SoundType _Sound, int _Clip_Index = -1, float _Volume = 1.0f)
    {

        /*AudioClip[] clips = _Instance._Sound_Clips[(int) _Sound]._Sounds;
        AudioClip _Random_Clip = clips[UnityEngine.Random.Range(0, clips.Length)];
        _Instance._Audio_Source.PlayOneShot(_Random_Clip, _Volume);*/

        AudioClip[] _Clips = _Instance._Sound_Clips[(int) _Sound]._Sounds;

        if (_Clips.Length == 0)
        {

            return;

        }

        AudioClip _Clip;

        if (_Clip_Index < 0)
        {

            _Clip = _Clips[UnityEngine.Random.Range(0, _Clips.Length)];

        }
        else
        {

            _Clip = _Clips[Mathf.Clamp(_Clip_Index, 0, _Clips.Length - 1)];

        }

        _Instance._Audio_Source.PlayOneShot(_Clip, _Volume);


        //Different ways to call this function.

        //#Random clip if the array has more than one sound.
        //_Sound_Manage_III._Play_Sound(SoundType.X);
        ///# X = Specific sound clip.

        //#The first sound clip of an array.
        //_Sound_Manage_III._Play_Sound(SoundType.X, 0);
        //# X = Specific sound clip.

        //# Specific sound clip in a single array element.
        //_Sound_Manage_III._Play_Sound(SoundType.X, Y);
        //# X = Specific sound clip. & Y = Specific sound clip in X.

        //# Specific sound clip with with volume control options.
        //_Sound_Manage_III._Play_Sound(SoundType.X, Y, Z);
        //# X = Specific sound clip. & Y = Specific sound clip in X. & Z = Specific volume setting.



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