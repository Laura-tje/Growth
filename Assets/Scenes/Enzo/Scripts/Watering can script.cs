using UnityEngine;

public class Wateringcanscript : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject wateringCan;
    private void Update()
    {
        if (animator.GetBool("Watering") == true)
        {
            wateringCan.SetActive(true);
        }
        else if (animator.GetBool("Watering") == false)
        {
            wateringCan.SetActive(false);
        }
    }

}
