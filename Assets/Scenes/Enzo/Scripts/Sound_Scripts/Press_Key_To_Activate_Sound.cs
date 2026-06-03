using UnityEngine;

public class Press_Key_To_Activate_Sound : MonoBehaviour
{

    private Sound_Manage_III _Sound_Manager;

    [SerializeField] private Animator _Animator;


    private void Update()
    {

        if ( Input.GetKeyDown(KeyCode.Space))
        {

            Debug.Log("Space key pressed");

            _Animator.SetBool("Jump", true);

        }

    } 

    private void _Jump_Finished()
    {

        _Animator.SetBool("Jump", false);

    }

}
