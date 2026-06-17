using UnityEngine;
using System.Collections;

public class Trashcan_Script : MonoBehaviour
{
    private GameObject player;
    private Inventory inventory;
    private bool throwing;
    private void Start()
    {
        throwing = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inventory = other.GetComponentInChildren<Inventory>();
            throwing = true;
            StartCoroutine(Throw_Inventory());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            throwing = false;
        }
    }
    private IEnumerator Throw_Inventory()
    {
        if (throwing == true && inventory.InventoryItems.Count > 0)
        {
            yield return new WaitForSeconds(1.5f);
            Sound_Manage_III.Instance._Play_Sound(8);
            foreach (GameObject item in inventory.InventoryItems)
            {
                Destroy(item);  
            }
        }
        inventory.InventoryItems.Clear();
    }
}
