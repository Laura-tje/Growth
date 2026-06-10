using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Seeds : MonoBehaviour
{ 
    [SerializeField] public Lot_Manager.TypeFlowers flowerSeed;

    [SerializeField] private int hitLives;


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
            //other.GetComponentInChildren<Inventory>().AddObjectToInventory(gameObject);
            StartCoroutine(WhackSeeds());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //other.GetComponentInChildren<Inventory>().AddObjectToInventory(gameObject);
            StopCoroutine(WhackSeeds());
        }
    }

    private IEnumerator WhackSeeds()
    {
        while(hitLives > 0)
        {
            hitLives -= 1;
            yield return new WaitForSeconds(1f);
        }
        Debug.Log("Dies");
       
        yield return null;
    }
}
