using UnityEngine;

public class Wateringcanscript : MonoBehaviour
{
    public Animator animator;
    public GameObject wateringCan;
    private void Update()
    {
        if (animator.GetBool("Watering") == true)
        {
            Hoe.SetActive(true);
        }
        else if (animator.GetBool("Watering") == false)
        {
            Hoe.SetActive(false);
        }
    }

}
