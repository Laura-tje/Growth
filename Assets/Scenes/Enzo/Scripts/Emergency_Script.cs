using UnityEngine;

public class Emergency_Script : MonoBehaviour
{

    [SerializeField] Vector3 _Parent;
    [SerializeField] GameObject _Child;

    private void Start()
    {
        
        _Child.transform.rotation = Quaternion.LookRotation(_Parent);

    }

}
