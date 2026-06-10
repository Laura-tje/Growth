using UnityEngine;

public class Press_Key_To_Activate_Sound : MonoBehaviour
{

    [SerializeField] private Animator _Animator;


    void Update()
    {

        if ( Input.GetKeyDown(KeyCode.Space))
        {

            Debug.Log("Space key pressed");

            _Animator.SetBool("Jump", true);

        }

        if ( Input.GetKeyDown(KeyCode.A))
        {
            
            _Instance._Audio_Source.PlayOneShot(_Instance._Sound_Clips[(0), _Sound], _Volume);

        }

    } 

    private void _Jump_Finished()
    {

        _Animator.SetBool("Jump", false);

    }

}
