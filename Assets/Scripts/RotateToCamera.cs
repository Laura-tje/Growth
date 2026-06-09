using UnityEngine;

public class RotateToCamera : MonoBehaviour
{

    [SerializeField] private float YWaarde;
    [SerializeField] Camera cam;
    void Start()
    {
        //cam = Camera.main;
        if (cam == null)
        {
            Debug.Log("You fucked up the camera finding you dipshit");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = transform.position - cam.transform.position;
        dir.x = 0; // ignore  angle completely
        transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
    }
}
