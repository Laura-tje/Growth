using UnityEngine;
using TMPro;

public class Search_Audio_Clip_By_Name : MonoBehaviour
{

    [SerializeField] private TMP_InputField _Input_Field;

    [SerializeField] private Sound_Manage_III _Sound_Manager;

    private void Start()
    {

        _Input_Field.onSubmit.AddListener(_Search_For_Audio_Clip);

    }

    private void _Search_For_Audio_Clip(string _Audio_Clip_Name)
    {

        /*for (int i = 0; i < _Sound_Manager._Sound_Clips.Length; i++)
        {

            if (_Sound_Manager._Sound_Clips[i].name.Equals(_Audio_Clip_Name, System.StringComparison.OrdinalIgnoreCase))
            {

                Sound_Manage_III._Play_Sound((SoundType) i);

                Debug.Log("Playing sound: " + _Audio_Clip_Name);

                break;

            }

        }*/

    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Return))
        {

            Debug.Log("Return key pressed");

            string input = _Input_Field.text.Trim();

            if ( System.Enum.TryParse(input, true, out SoundType sound))
            {

                Debug.Log($"Parsed sound: {sound}");

                Sound_Manage_III._Play_Sound(sound);

            }

            else 
            {
                
                Debug.Log($"Sound '{input}' not found.");

            }


        }

    }

}
