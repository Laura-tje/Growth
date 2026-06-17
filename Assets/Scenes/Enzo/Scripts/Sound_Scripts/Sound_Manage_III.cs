using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Sound_Manage_III : MonoBehaviour
{
    private int RandomPitch;
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
        deleting = 8,
    
    }
    
    [SerializeField] private AudioClip[] _Sound_Clips;
    public static Sound_Manage_III Instance;
    [SerializeField] private AudioSource _Audio_Source;
    [SerializeField] private AudioSource musicSource;
    private bool SoundOn = true;


    public void SwitchSoundToggle()
    {
        if (SoundOn)
        {
            SoundOn = false;
            musicSource.Pause();
        }
        else
        {
            SoundOn = true;
            musicSource.Play();
        }
    }
    
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
        //_Audio_Source = GetComponent<AudioSource>();
        musicSource.loop = true;
        musicSource.clip = _Sound_Clips[0];
        musicSource.Play();
    }
    public void _Play_Sound(int index)
    {
        if (SoundOn)
        {
            _Audio_Source.PlayOneShot(_Sound_Clips[index]);
        }
    }
        
    

}