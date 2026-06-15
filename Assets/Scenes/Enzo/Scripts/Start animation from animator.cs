using UnityEngine;

public class Startanimationfromanimator : MonoBehaviour
{
    [SerializeField] private Animator animation;

    private void ChangeBool()
    {
        animation.SetBool("Celebrate", false);
    }
}
