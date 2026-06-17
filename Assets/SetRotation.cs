using UnityEngine;

public class SetRotation : MonoBehaviour
{
    [SerializeField] private Vector3 ObjectRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      //this.gameObject.transform.eulerAngles = new Vector3(-90, ObjectRotation.y, ObjectRotation.z);
    }

    // Update is called once per frame
    void Update()
    {
        //this.gameObject.transform.eulerAngles = new Vector3(0,0,0);
    }

    public void ResetRotation()
    {
        this.gameObject.transform.eulerAngles = new Vector3(-90, ObjectRotation.y, ObjectRotation.z);

    }
}
