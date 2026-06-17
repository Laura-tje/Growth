using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<GameObject> InventoryItems;

    public List<GameObject> InventorySlots;

    //public List<GameObject> roseSeeds;

    //public List<GameObject> lilySeeds;

    //public List<GameObject> lavenenderSeeds;

    //public List<GameObject> sunFlowerSeeds;

    public List<GameObject> seeds;

    private GameObject currentHarvestedItem;

    private int ChosenItem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddObjectToInventory(GameObject currentObtainableItemInRange)
    {
        if(InventoryItems.Count < InventorySlots.Count)
        {
            ChosenItem = Random.Range(0, currentObtainableItemInRange.GetComponent<ItemList>().Item.Count);

            Debug.Log(currentObtainableItemInRange.GetComponent<ItemList>().Item[ChosenItem].gameObject.transform.rotation.eulerAngles);
            InventoryItems.Add(currentHarvestedItem = Instantiate(currentObtainableItemInRange.GetComponent<ItemList>().Item[ChosenItem], gameObject.transform.position, gameObject.transform.rotation, gameObject.transform));

            if (currentObtainableItemInRange.GetComponent<ItemList>().obtainablItems == ItemList.ObtainablItems.Seed)
            {
                seeds.Add(currentHarvestedItem);

                //currentObtainableItemInRange.GetComponent<Seeds>().Test();
            }

            for (int i = 0; i < InventoryItems.Count; i++)  
            {
                for (int j = 0; j < InventorySlots.Count; j++)
                {
                    InventoryItems[i].gameObject.transform.position = InventorySlots[i].gameObject.transform.position;

                    if (InventoryItems[i].GetComponent<Seeds>() != null)
                    {
                        InventoryItems[i].gameObject.transform.localRotation = Quaternion.Euler(-90, 90, 0);
                    }
                }
            }

            for (int i = 0; i < InventoryItems.Count; i++)
            {
                InventoryItems[i].transform.parent = InventorySlots[i].gameObject.transform;
            }
        }
    }

    public void ResetItemPlacement()
    {
        for (int i = 0; i < InventoryItems.Count; i++)
        {
            for (int j = 0; j < InventorySlots.Count; j++)
            {
                InventoryItems[i].gameObject.transform.position = InventorySlots[i].gameObject.transform.position;
            }
        }

        for (int i = 0; i < InventoryItems.Count; i++)
        {
            InventoryItems[i].transform.parent = InventorySlots[i].gameObject.transform;
        }
    }
}
