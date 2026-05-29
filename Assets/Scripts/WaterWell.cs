using UnityEngine;

public class WaterWell : MonoBehaviour
{
    public float Level;
    public Inventory Inventory; //this is assigned in waterupdater because i dont know...
    void Start()
    {
        Level = 1;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);

        if (other.gameObject.name == "Player")
        {
            //Inventory.addToInventory(other.gameObject);
        }
    }

    public void UpdateWell()
    {
        Level++;
        
        Debug.Log(Level);
    }
}
