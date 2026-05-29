using UnityEngine;

public class WaterWell : MonoBehaviour
{
    public float Level;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Level = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);

        if (other.gameObject.name == "Player")
        {
            //put the water in inventory!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        }
    }

    public void UpdateWell()
    {
        Level++;
        
        Debug.Log(Level);
    }
}
