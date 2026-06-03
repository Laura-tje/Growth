using UnityEngine;

public class Press_Key_To_Activate_Sound : MonoBehaviour
{

    private Sound_Manage_III _Sound_Manager;

    private void update()
    {

        if (Input.GetKeyDown(KeyCode.))
        {

            Sound_Manage_III._Play_Sound(SoundType.RED);

        }

    }


}
