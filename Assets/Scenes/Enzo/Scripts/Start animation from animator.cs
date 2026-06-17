using UnityEngine;

public class Startanimationfromanimator : MonoBehaviour
{
    [SerializeField] private Animator animation;

    private void ChangeBool()
    {
        animation.SetBool("Celebrate", false);
    }

    private void ChangeBoolII()
    {
        animation.SetBool("Watering", false);
    }


    public void PlayWhackSound()
    {
        Sound_Manage_III.Instance._Play_Sound(5);
    }
}
