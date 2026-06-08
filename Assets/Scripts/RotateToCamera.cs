using UnityEngine;

public class RotateToCamera : MonoBehaviour
{
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
        transform.LookAt(cam.transform);
        transform.Rotate(0, 180, 0);
    }
}
