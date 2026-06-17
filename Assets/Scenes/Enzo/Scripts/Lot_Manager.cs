using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

public class Lot_Manager : MonoBehaviour
{

    [SerializeField] private GameObject _Plant;
    [SerializeField] private GameObject _Player;
    [SerializeField] private Animator animator;

    [SerializeField] private bool _Plant_Still_Growing;
    [SerializeField] private bool _Plant_Done_Growing;
    [SerializeField] private bool _Transfering;

    [SerializeField] private List<ItemForGrowth> flowerItemsForGrowth;

    private ItemForGrowth itemForGrowth;

    private float startScale;

    private float target;

    private Vector3 targetVector3;

    [SerializeField] private float maxScale;

    public int currentAmountOfSeeds;

    public int currentAmountOfWater;

    public int currentAmountOfSoil;

    public float currentAmountOfAllItems;

    public float currentAmountOfAllItemsPercentage;

    [SerializeField] private Transform seedPlacement;

    [SerializeField] private GameObject UpgradedObject;

    private Coroutine storedCoroutine;

    public enum TypePlant
    {
        None = -1,
        Tulip = 0,
        Lily = 1, 
        Forget_Me_not = 2,
        Sunflower = 3,
        Carrots = 4,
        CaulliFlower = 5,
        Kurku = 6,
        StrawBerryBush = 7,
        WaterMelon = 8
    }

    [SerializeField] public TypePlant typePlant;

    //[SerializeField] private int _Current_Amount_Mats;
    //private int _Max_Amount_Mats_Grow;

    private void Start()
    {
        //Saving Start Size of Plant that will grow
        startScale = gameObject.transform.localScale.x;

        //Checking how many Items the plant needs to be fully grown
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
        if (other.gameObject.tag == "Player" && _Plant_Still_Growing && !_Transfering && other.GetComponentInChildren<Inventory>().InventoryItems.Count != 0)
        {
            //Save currently activated Coroutine to be able to stop it later on
            _Transfering = true;
            storedCoroutine = StartCoroutine(_Transfer_Mats(other.gameObject));

            Debug.Log("Beep");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && _Plant_Still_Growing && _Transfering)
        {
            //if (storedCoroutine != null)
            //{
            //    //Stopping coroutine to prevent bugs
            //    StopCoroutine(storedCoroutine);
            //}

            _Transfering = false;

            _Plant_Still_Growing = true;

            _Plant_Done_Growing = false;
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

        if (itemForGrowth != null)
        {
            if (currentAmountOfAllItems >= itemForGrowth.allItemsNeeded) yield break;
        }

        // 

        //When the plant has not been planted yet, this will activate to give it a plant identity only ones
        if (typePlant == TypePlant.None && itemForGrowth == null)
        {
            ApplySeedFromPlayerInventory(inventory, Player);
        }

        //if (currentAmountOfAllItems >= itemForGrowth.allItemsNeeded && currentAmountOfSeeds > 0)
        //{
        //    _Transfering = false;

        //    _Plant_Still_Growing = false;

        //    _Plant_Done_Growing = true;

        //    Debug.Log("Transfering mats stopped");
        //}

        //To make it so that Water or Soil can not be added to the plant before a plant is even planted

        if (itemForGrowth != null && currentAmountOfAllItems <= itemForGrowth.allItemsNeeded)
        {
            while (currentAmountOfAllItems <= itemForGrowth.allItemsNeeded && inventory.InventoryItems.Count != 0 && _Transfering)
            {
                //yield return new WaitForSeconds(0.1f);

                Seeds currentSeeds;
                GameObject InventoryItem;

                for (int i = 0; i < inventory.InventoryItems.Count; i++)
                {
                    if (inventory.InventoryItems[i].GetComponent<Seeds>() != null && itemForGrowth.seedsNeeded > 0 && currentAmountOfSeeds < itemForGrowth.seedsNeeded && inventory.InventoryItems[i].gameObject.tag == "Items")
                    {
                        currentSeeds = inventory.InventoryItems[i].GetComponent<Seeds>();
                        InventoryItem = inventory.InventoryItems[i].gameObject;

                        //if the seed is the same as the seed that the plant is looking for then it can be added to the plant if it doesn't have the max amount of seeds it needs
                        if (currentSeeds.typePlant == typePlant)
                        {
                            //Disconnect the item from the inventory slot to make it be able to move to the plant for animation
                            InventoryItem.transform.parent = null;

                            inventory.InventoryItems.Remove(InventoryItem);
                            inventory.seeds.Remove(InventoryItem);
                            inventory.ResetItemPlacement();

                            while (InventoryItem.transform.position != gameObject.transform.position)
                            {
                                //Move the focused item object towards the plant slowly
                                InventoryItem.transform.position = Vector3.MoveTowards(InventoryItem.transform.position, gameObject.transform.position, Time.deltaTime * 5f);
                                yield return new WaitForEndOfFrame();
                            }

                            if (!_Plant)
                            {
                                //make the actual plant appear in the game that will be growing and get the start size of the plant before it grows
                                startScale = itemForGrowth.plantToGrow.transform.localScale.x;
                                _Plant = Instantiate(itemForGrowth.plantToGrow, seedPlacement.transform.position, itemForGrowth.plantToGrow.transform.rotation, seedPlacement.transform);

                                GetComponentInChildren<ItemList>().enabled = false;
                                GetComponentInChildren<Seeds>().enabled = false;
                                GetComponentInChildren<Seeds>().mainCanvas.gameObject.SetActive(false);
                            }
                        }


                        if (InventoryItem.transform.position == gameObject.transform.position)
                        {
                            //Increase the current amount of seeds the plant has and increase it's size equal to the amount of items currently taken
                            currentAmountOfSeeds += 1;
                            CurrentAmountOfItems();
                            Destroy(InventoryItem);
                            while (_Plant.transform.localScale != targetVector3)
                            {
                                _Plant.transform.localScale = Vector3.MoveTowards(_Plant.transform.localScale, targetVector3, Time.deltaTime * 1000);
                                Sound_Manage_III.Instance._Play_Sound(3);
                                yield return new WaitForEndOfFrame();
                            }
                        }
                    }


                    else if (inventory.InventoryItems[i].GetComponent<Water>() != null && itemForGrowth.waterNeeded > 0 && currentAmountOfWater < itemForGrowth.waterNeeded && currentAmountOfSeeds > 0)
                    {
                        InventoryItem = inventory.InventoryItems[i].gameObject;
                        animator.SetBool("Watering", true);
                        Sound_Manage_III.Instance._Play_Sound(4);
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
                            while (_Plant.transform.localScale != targetVector3)
                            {
                                _Plant.transform.localScale = Vector3.MoveTowards(_Plant.transform.localScale, targetVector3, Time.deltaTime * 1000);
                                Sound_Manage_III.Instance._Play_Sound(3);
                                yield return new WaitForEndOfFrame();
                            }
                        }
                        Debug.Log("CheckPoint 02");
                    }

                    else if (inventory.InventoryItems[i].GetComponent<Soil>() != null && itemForGrowth.soilNeeded > 0 && currentAmountOfSoil < itemForGrowth.soilNeeded && currentAmountOfSeeds > 0)
                    {
                        InventoryItem = inventory.InventoryItems[i].gameObject;
                        Debug.Log("Soil");
                        //animator.SetBool("Watering", true);
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
                            currentAmountOfSoil += 1;
                            CurrentAmountOfItems();
                            Destroy(InventoryItem);
                            while (_Plant.transform.localScale != targetVector3)
                            {
                                _Plant.transform.localScale = Vector3.MoveTowards(_Plant.transform.localScale, targetVector3, Time.deltaTime * 1000);
                                Sound_Manage_III.Instance._Play_Sound(3);
                                yield return new WaitForEndOfFrame();
                            }
                        }
                       
                        Debug.Log("CheckPoint 03");
                    }
                }

                //Check if the plant has the max amount of plants
                if (currentAmountOfAllItems >= itemForGrowth.allItemsNeeded)
                {
                    _Plant_Still_Growing = false;
                        
                    _Plant_Done_Growing = true;

                    if (UpgradedObject != null)
                    {
                        UpgradeObject(UpgradedObject);
                    }

                    animator.SetBool("Celebrate", true);

                    Sound_Manage_III.Instance._Play_Sound(7);

                    GetComponentInChildren<ItemList>().enabled = true;
                    GetComponentInChildren<Seeds>().enabled = true;

                    yield break;
                }
                else
                {
                    _Plant_Still_Growing = true;

                    _Plant_Done_Growing = false;
                }
                    
                _Transfering = false;

            }
        }
    }

    public virtual void UpgradeObject(GameObject UpgradedObject)
    {
        //used if the current object getting the items has a upgraded state
        Destroy(gameObject);
        Instantiate(UpgradedObject, transform.position, transform.rotation);
    }

    private void ApplySeedFromPlayerInventory(Inventory inventory, GameObject Player)
    {
        //for the first time you plant a seed in the ground for the plant to grow

        Seeds currentSeeds;
        GameObject InventoryItem;
        for (int i = 0; i < inventory.InventoryItems.Count; i++)
        {
            if (inventory.InventoryItems[i].GetComponent<Seeds>() != null)
            {
                currentSeeds = inventory.InventoryItems[i].GetComponent<Seeds>();
                InventoryItem = inventory.InventoryItems[i].gameObject;

                typePlant = currentSeeds.typePlant;
                itemForGrowth = flowerItemsForGrowth[(int)typePlant];
                itemForGrowth.MaxAmountOfItemsNeeded();
                break;
            }
        }

        animator = Player.GetComponentInChildren<Animator>();
    }

    public void CurrentAmountOfItems()
    {
        //Get the current amount of items the player has to compare it later to the max amount of items the plant needs
        currentAmountOfAllItems = currentAmountOfSeeds + currentAmountOfWater + currentAmountOfSoil;

        if (itemForGrowth != null)
        {
            currentAmountOfAllItemsPercentage = currentAmountOfAllItems / itemForGrowth.allItemsNeeded * 1;
            target = currentAmountOfAllItemsPercentage * ((startScale * maxScale) - startScale);
            target = target + startScale;
            targetVector3 = new Vector3(target, target, target);
        }
    }

    public void ResetAmountOfItems()
    {
        currentAmountOfAllItems = 0;

        currentAmountOfSeeds = 0;

        currentAmountOfSoil = 0;

        currentAmountOfWater = 0;

        currentAmountOfAllItemsPercentage = 0;

        target = 0;

        _Plant = null;

        _Plant_Still_Growing = true;

        _Plant_Done_Growing = false;

        _Transfering = false;

        itemForGrowth = null;

        typePlant = TypePlant.None;
    }
}
