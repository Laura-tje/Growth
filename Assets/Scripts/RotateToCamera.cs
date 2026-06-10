using UnityEngine;

public class RotateToCamera : MonoBehaviour
{
    //[SerializeField] Camera cam;
    //void Start()
    //{
    //    //cam = Camera.main;
    //    if (cam == null)
    //    {
    //        Debug.Log("You fucked up the camera finding you dipshit");
    //    }
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    transform.LookAt(cam.transform);
    //    transform.Rotate(0, 180, 0);
    //}

    [Header("Lock Rotation")]
    [SerializeField] private bool lockX;
    [SerializeField] private bool lockY;
    [SerializeField] private bool lockZ;

    private Vector3 originalRotation;

    [SerializeField] private BillboardType billboardType;

    private void Awake()
    {
        originalRotation = transform.rotation.eulerAngles;
    }

    public enum BillboardType { LookAtCamera, CameraForward }

    private void LateUpdate()
    {
        switch (billboardType)
        {
            case BillboardType.LookAtCamera:
                transform.LookAt(Camera.main.transform.position, Vector3.up);
                break;

            case BillboardType.CameraForward:
                transform.forward = Camera.main.transform.forward;
                break;
            default: break;
        }

        //Modify the rotation in Euler Space to lock certain dimensions
        Vector3 rotation = transform.rotation.eulerAngles;
        if (lockX) { rotation.x = originalRotation.x; }
        if (lockY) { rotation.y = originalRotation.y; }
        if (lockZ) { rotation.z = originalRotation.z; }

    }
}
