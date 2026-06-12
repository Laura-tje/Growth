using UnityEngine;

public class Hoe_script : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject Hoe;
    private void Update()
    {
        if (animator.GetBool("Hoe") == true)
        {
            Hoe.SetActive(true);
        }
        else if (animator.GetBool("Hoe") == false)
        {
            Hoe.SetActive(false);
        }
    }
}
