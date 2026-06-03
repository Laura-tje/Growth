using UnityEngine;
using TMPro;

public class Search_Audio_Clip_By_Name : MonoBehaviour
{

    [SerializeField] private InputField _Input_Field;

    [SerializeField] private Sound_Manage_III _Sound_Manager;

    private void Start()
    {

        _Input

    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Return))
        {

            string _Audio_Clip_Name = _Input_Field.text;

            for (int i = 0; i < _Sound_Manager._Sound_Clips.Length; i++)
            {

                if (_Sound_Manager._Sound_Clips[i].name == _Audio_Clip_Name)
                {

                    Sound_Manage_III._Play_Sound((SoundType) i);

                }

            }

        }

    }

}
