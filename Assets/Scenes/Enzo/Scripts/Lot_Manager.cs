using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lot_Manager : MonoBehaviour
{

    [SerializeField] private GameObject _Plant;
    [SerializeField] private GameObject _Player;

    [SerializeField] private bool _Plant_Still_Growing;
    [SerializeField] private bool _Plant_Done_Growing;
    [SerializeField] private bool _Transfering;

    [SerializeField] private List<ItemForGrowth> flowerItemsForGrowth;

    private ItemForGrowth itemForGrowth;

    private float startScale;

    private float target;

    private Vector3 targetVector3;

    [SerializeField] private float MaxScale;

    public int currentAmountOfSeeds;

    public int currentAmountOfWater;

    public int currentAmountOfSoil;

    public float currentAmountOfAllItems;

    public float currentAmountOfAllItemsPercentage;

    [SerializeField] private Transform SeedPlacement;

    public enum TypeFlowers
    {
        None = -1,
        Rose = 0,
        Lily = 1,
        Lavender = 2,
        Sunflower = 3
    }

    [SerializeField] public TypeFlowers typeFlower;

    //[SerializeField] private int _Current_Amount_Mats;
    //private int _Max_Amount_Mats_Grow;

    private void Start()
    {
        startScale = gameObject.transform.localScale.x;
        CurrentAmountOfItems();

        //_Check_Plant();
        _Plant_Still_Growing = true;
        _Plant_Done_Growing = false;
        _Transfering = false;

    }

    //private void _Check_Plant()
    //{

    //    if ( _Plant == null)
    //    {

    //        Debug.Log("No plant assigned to lot");

    //    }

    //    else if ( _Plant != null)
    //    {

    //        //if ( _Plant.transform.GetChild(0).gameObject.name == "Plant_Type_1")
    //        //{

    //        //    //_Plant_Growth_Plan_1();


    //        //}
    //            Debug.Log("Plant type 1 growth plan assigned to lot");

    //    }

    //}

    //private void _Plant_Growth_Plan_1()
    //{

    //    int Mats_Needed = 50;

    //    _Max_Amount_Mats_Grow = Mats_Needed;

    //}

     
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && _Plant_Still_Growing && !_Transfering)
        {
            StartCoroutine(_Transfer_Mats(other.gameObject));
            Debug.Log("test");
            _Transfering = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if ( other.gameObject.tag == "Player" && _Plant_Still_Growing && _Transfering)
        {
            _Transfering = false;
        }
    }

    private IEnumerator _Transfer_Mats(GameObject Player)
    {
        // Guard clauses.
        //Player_Growth_Corrosponding_Script player_Script = _Player.GetComponent<Player_Growth_Corrosponding_Script>();
        if (Player == null) yield break; // If player is null, we cant do anything.

        Inventory inventory = Player.GetComponentInChildren<Inventory>();
        if (inventory == null) yield break; // If the player does not have an inventory, we cant do anything.
        if (inventory.InventoryItems.Count <= 0) yield break; // Player doesnt have any items, so we cant do anything.

        Debug.Log(currentAmountOfAllItems);

        if(typeFlower == TypeFlowers.None)
        {
            ApplySeedFromPlayerInventory(inventory);
        }

        if (currentAmountOfAllItems >= itemForGrowth.allItemsNeeded)
        {
            _Transfering = false;

            _Plant_Still_Growing = false;

            _Plant_Done_Growing = true;

            Debug.Log("Transfering mats stopped");
        }

        else if (currentAmountOfAllItems <= itemForGrowth.allItemsNeeded)
        {

            while (currentAmountOfAllItems <= itemForGrowth.allItemsNeeded)
            {

                //yield return new WaitForSeconds(0.1f);

                Seeds currentSeeds;
                GameObject InventoryItem;

                for (int i = 0; i < inventory.InventoryItems.Count; i++)
                {
                    if (inventory.InventoryItems[i].GetComponent<Seeds>() != null && itemForGrowth.seedsNeeded > 0 && currentAmountOfSeeds < itemForGrowth.seedsNeeded)
                    {
                        currentSeeds = inventory.InventoryItems[i].GetComponent<Seeds>();
                        InventoryItem = inventory.InventoryItems[i].gameObject;

                        if (currentSeeds.flowerSeed == typeFlower)
                        {
                            InventoryItem.transform.parent = null;
                            inventory.InventoryItems.Remove(InventoryItem);
                            inventory.seeds.Remove(InventoryItem);
                            inventory.ResetItemPlacement();
                            while(InventoryItem.transform.position != gameObject.transform.position)
                            {
                                InventoryItem.transform.position = Vector3.MoveTowards(InventoryItem.transform.position, gameObject.transform.position, Time.deltaTime * 5f);
                                yield return new WaitForEndOfFrame();
                            }
                            if (!_Plant)
                            {
                                startScale = itemForGrowth.plantToGrow.transform.localScale.x;
                                _Plant = Instantiate(itemForGrowth.plantToGrow, SeedPlacement.transform.position, SeedPlacement.transform.rotation, SeedPlacement.transform);
                            }
                        }
                        if(InventoryItem.transform.position == gameObject.transform.position)
                        {
                            currentAmountOfSeeds += 1;
                            CurrentAmountOfItems();
                            Destroy(InventoryItem);
                            while(_Plant.transform.localScale != targetVector3)
                            {
                                _Plant.transform.localScale = Vector3.MoveTowards(_Plant.transform.localScale, targetVector3, Time.deltaTime * 5);
                                Debug.Log("Dies");
                                yield return new WaitForEndOfFrame();
                            }
                        }
                    }

                    else if(inventory.InventoryItems[i].GetComponent<Water>() != null && itemForGrowth.waterNeeded > 0 && currentAmountOfWater < itemForGrowth.waterNeeded)
                    {
                        InventoryItem = inventory.InventoryItems[i].gameObject;

                            InventoryItem.transform.parent = null;
                            inventory.InventoryItems.Remove(InventoryItem);
                            inventory.ResetItemPlacement();
                            while (InventoryItem.transform.position != gameObject.transform.position)
                            {
                                InventoryItem.transform.position = Vector3.MoveTowards(InventoryItem.transform.position, gameObject.transform.position, Time.deltaTime * 5);
                                yield return new WaitForEndOfFrame();
                            }
                            
                        if (InventoryItem.transform.position == gameObject.transform.position)
                        {
                            currentAmountOfWater += 1;
                            CurrentAmountOfItems();
                            Destroy(InventoryItem);
                            while (gameObject.transform.localScale != targetVector3)
                            {
                                _Plant.transform.localScale = Vector3.MoveTowards(_Plant.transform.localScale, targetVector3, Time.deltaTime * 5);
                                yield return new WaitForEndOfFrame();
                            }
                        }
                    }
                }

                _Transfering = true;

                _Plant_Still_Growing = true;

                _Plant_Done_Growing = false;

                _Transfering = false;

                Debug.Log(_Transfering);

                if (_Transfering == false)
                {
                    break;
                }
            }
        }

    }

    private void ApplySeedFromPlayerInventory(Inventory inventory)
    {
        Seeds currentSeeds;
        GameObject InventoryItem;
        for (int i = 0; i < inventory.InventoryItems.Count; i++)
        {
            currentSeeds = inventory.InventoryItems[i].GetComponent<Seeds>();
            InventoryItem = inventory.InventoryItems[i].gameObject;

            typeFlower = currentSeeds.flowerSeed;
            itemForGrowth = flowerItemsForGrowth[(int)typeFlower];
            itemForGrowth.MaxAmountOfItemsNeeded();
            break;
        }
    }

    public void CurrentAmountOfItems()
    {
        currentAmountOfAllItems = currentAmountOfSeeds + currentAmountOfWater + currentAmountOfSoil;

        if(itemForGrowth != null)
        {
            currentAmountOfAllItemsPercentage = currentAmountOfAllItems / itemForGrowth.allItemsNeeded * 1;
            target = currentAmountOfAllItemsPercentage * (MaxScale - startScale);
            target = target + startScale;
            targetVector3 = new Vector3(target, target, target);
        }
    }

}
