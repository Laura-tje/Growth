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
            
            Sound_Manage_III._Play_Sound(SoundType.RED);
        }

    } 

    private void _Jump_Finished()
    {

        _Animator.SetBool("Jump", false);

    }

}
