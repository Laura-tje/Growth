using UnityEngine;

public class Seeds : MonoBehaviour
{ 

    [SerializeField] public Lot_Manager.TypeFlowers flowerSeed; 

    private enum CropSeeds
    {

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Test()
    {
        Debug.Log("Work");
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.GetComponentInChildren<Inventory>().AddObjectToInventory(gameObject);
        }
    }
}
