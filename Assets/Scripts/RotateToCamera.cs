using UnityEngine;

public class RotateToCamera : MonoBehaviour
{
    //[SerializeField] private float YWaarde;
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
    //    Vector3 dir = transform.position - cam.transform.position;
    //    dir.x = 0; // ignore  angle completely
    //    transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
    //}

    [SerializeField] private BillboardType billboardType;

    [Header("Lock Rotation")]
    [SerializeField] private bool lockX;
    [SerializeField] private bool lockY;
    [SerializeField] private bool lockZ;

    private Vector3 originalRotation;

    public enum BillboardType { LookAtCamera, CameraForward }

    private void Start()
    {
        originalRotation = transform.rotation.eulerAngles;
    }

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

        //Modify the rotation in Euler space to lock certain dimensions
        Vector3 rotation = transform.rotation.eulerAngles;

        if (lockX) { rotation.x = originalRotation.x; }
        if (lockY) { rotation.y = originalRotation.y; }
        if (lockZ) { rotation.z = originalRotation.z; }

    }
}
