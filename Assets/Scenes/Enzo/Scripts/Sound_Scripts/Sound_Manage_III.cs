using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(AudioSource))]
public class Sound_Manage_III : MonoBehaviour
{
    public enum SoundType
    {
    
        //Sound goes here.
        backgroundMusic = 0,
        click = 1, 
        grab = 2, 
        grow = 3, 
        water = 4, 
        whacking = 5, 
        upgrading = 6, 
        flying = 7,
    
    }
    
    [SerializeField] private AudioClip[] _Sound_Clips;
    public static Sound_Manage_III Instance;
    private AudioSource _Audio_Source;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } 
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        _Audio_Source = GetComponent<AudioSource>();
    }
    public void _Play_Sound(int index)
    {
        _Audio_Source.PlayOneShot(_Sound_Clips[index]);
        Debug.Log(index);
    }
    
    

}