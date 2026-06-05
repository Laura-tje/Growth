using UnityEngine;

public class ItemList : MonoBehaviour
{
    public GameObject Item;

    public enum ObtainablItems 
    {
        Seed,
        Water,
        Soil
    }

    [SerializeField] public ObtainablItems obtainablItems;

    private enum CropSeeds
    {

    }
    [SerializeField] private CropSeeds cropSeed;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponentInChildren<Inventory>().AddObjectToInventory(gameObject);
        }
    }
}
