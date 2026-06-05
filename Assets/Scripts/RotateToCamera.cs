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
        gameObject.transform.LookAt(cam.transform);
        gameObject.transform.rotation = Quaternion.Euler(
            gameObject.transform.eulerAngles.x,
            180,
            gameObject.transform.eulerAngles.z
        );
    }
}
