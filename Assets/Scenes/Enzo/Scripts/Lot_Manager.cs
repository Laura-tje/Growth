using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Lot_Manager : MonoBehaviour
{

    [SerializeField] private GameObject _Plant;
    [SerializeField] private GameObject _Player;

    [SerializeField] private bool _Plant_Still_Growing;
    [SerializeField] private bool _Plant_Done_Growing;
    [SerializeField] private bool _Transfering;

    [SerializeField] private ItemForGrowth roseItemForGrowth;

    [SerializeField] private ItemForGrowth lilyItemForGrowth;

    [SerializeField] private ItemForGrowth lavenderItemForGrowth;

    [SerializeField] private ItemForGrowth sunflowerItemForGrowth;

    private ItemForGrowth itemForGrowth;


    [SerializeField] private List<int> GrowthRanks;


    public int currentAmountOfSeeds;

    public int currentAmountOfWater;

    public int currentAmountOfSoil;

    public int currentAmountOfAllItems;

    public int currentAmountOfAllItemsPercentage;

    public enum TypeFlowers
    {
        None,
        Rose,
        Lily,
        Lavender,
        Sunflower
    }

    [SerializeField] public TypeFlowers typeFlower;

    //[SerializeField] private int _Current_Amount_Mats;
    //private int _Max_Amount_Mats_Grow;

    private void Start()
    {
        CurrentAmountOfItems();

        _Check_Plant();
        _Plant_Still_Growing = true;
        _Plant_Done_Growing = false;
        _Transfering = false;

    }

    private void _Check_Plant()
    {

        if ( _Plant == null)
        {

            Debug.Log("No plant assigned to lot");

        }

        else if ( _Plant != null)
        {

            //if ( _Plant.transform.GetChild(0).gameObject.name == "Plant_Type_1")
            //{

            //    //_Plant_Growth_Plan_1();


            //}
                Debug.Log("Plant type 1 growth plan assigned to lot");

        }

    }

    //private void _Plant_Growth_Plan_1()
    //{

    //    int Mats_Needed = 50;

    //    _Max_Amount_Mats_Grow = Mats_Needed;

    //}

     
    private void OnTriggerStay(Collider other)
    {

        if ( other.gameObject.tag == "Player" && _Plant_Still_Growing && !_Transfering)
        {
            StartCoroutine(_Transfer_Mats(other.gameObject));
            _Transfering = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {

        if ( other.gameObject.tag == "Player" && _Plant_Still_Growing && _Transfering)
        {
            Debug.Log("works");
            _Transfering = false;
        }

    }

    private IEnumerator _Transfer_Mats(GameObject Player)
    {

        //Player_Growth_Corrosponding_Script player_Script = _Player.GetComponent<Player_Growth_Corrosponding_Script>();

        if ( Player != null && Player.GetComponentInChildren<Inventory>().InventoryItems.Count > 0)
        {
            if(typeFlower == TypeFlowers.None)
            {
                Seeds currentSeeds;
                GameObject InventoryItem;
                for (int i = 0; i < Player.GetComponentInChildren<Inventory>().InventoryItems.Count; i++)
                {
                    currentSeeds = Player.GetComponentInChildren<Inventory>().InventoryItems[i].GetComponent<Seeds>();
                    InventoryItem = Player.GetComponentInChildren<Inventory>().InventoryItems[i].gameObject;

                    switch (currentSeeds.flowerSeed)
                    {
                        case Seeds.FlowerSeeds.Rose:
                            typeFlower = TypeFlowers.Rose;
                            itemForGrowth = roseItemForGrowth;
                            itemForGrowth.MaxAmountOfItemsNeeded();
                            break;
                        case Seeds.FlowerSeeds.Lily:
                            typeFlower = TypeFlowers.Lily;
                            itemForGrowth = lilyItemForGrowth;
                            itemForGrowth.MaxAmountOfItemsNeeded();
                            break;
                        case Seeds.FlowerSeeds.Lavender:
                            typeFlower = TypeFlowers.Lavender;
                            itemForGrowth = lavenderItemForGrowth;
                            itemForGrowth.MaxAmountOfItemsNeeded();
                            break;
                        case Seeds.FlowerSeeds.Sunflower:
                            typeFlower = TypeFlowers.Sunflower;
                            itemForGrowth = sunflowerItemForGrowth;
                            itemForGrowth.MaxAmountOfItemsNeeded();
                            break;
                    }
                }
            }

            if (currentAmountOfAllItems == itemForGrowth.allItemsNeeded)
            {

                yield return null;

                _Transfering = false;

                _Plant_Still_Growing = false;

                _Plant_Done_Growing = true;

                Debug.Log("Transfering mats stopped");

            }

            else if (currentAmountOfAllItems <= itemForGrowth.allItemsNeeded)
            {

                while (currentAmountOfAllItems <= itemForGrowth.allItemsNeeded)
                {

                    yield return new WaitForSeconds(0.1f);

                    Seeds currentSeeds;
                    GameObject InventoryItem;

                    for (int i = 0; i < Player.GetComponentInChildren<Inventory>().InventoryItems.Count; i++)
                    {
                        if (Player.GetComponentInChildren<Inventory>().InventoryItems[i].GetComponent<Seeds>() != null && itemForGrowth.seedsNeeded > 0 && currentAmountOfSeeds < itemForGrowth.seedsNeeded)
                        {
                            currentSeeds = Player.GetComponentInChildren<Inventory>().InventoryItems[i].GetComponent<Seeds>();
                            InventoryItem = Player.GetComponentInChildren<Inventory>().InventoryItems[i].gameObject;

                            if (currentSeeds.flowerSeed.GetType().GetEnumName(currentSeeds.flowerSeed) == typeFlower.GetType().GetEnumName(typeFlower))
                            {
                                InventoryItem.transform.parent = null;
                                Player.GetComponentInChildren<Inventory>().InventoryItems.Remove(InventoryItem);
                                Player.GetComponentInChildren<Inventory>().seeds.Remove(InventoryItem);
                                Player.GetComponentInChildren<Inventory>().ResetItemPlacement();
                                while(InventoryItem.transform.position != gameObject.transform.position)
                                {
                                    InventoryItem.transform.position = new Vector3(Mathf.MoveTowards(InventoryItem.transform.position.x, gameObject.transform.position.x, Time.deltaTime * 5), Mathf.MoveTowards(InventoryItem.transform.position.y, gameObject.transform.position.y, Time.deltaTime * 5), Mathf.MoveTowards(InventoryItem.transform.position.z, gameObject.transform.position.z, Time.deltaTime * 5));
                                    yield return new WaitForEndOfFrame();
                                }
                            }
                            if(InventoryItem.transform.position == gameObject.transform.position)
                            {
                                currentAmountOfSeeds += 1;
                                CurrentAmountOfItems();
                                Debug.Log(currentAmountOfSeeds);
                                Destroy(InventoryItem);
                            }

                            if(currentAmountOfAllItemsPercentage >= GrowthRanks[0] && GrowthRanks != null)
                            {
                                GrowthRanks.Remove(0);
                                gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x + 5, gameObject.transform.localScale.y + 5, gameObject.transform.localScale.z + 5);
                            }
                        }

                        if(Player.GetComponentInChildren<Inventory>().InventoryItems[i].GetComponent<Water>() != null && itemForGrowth.waterNeeded > 0 && currentAmountOfWater < itemForGrowth.waterNeeded)
                        {
                            InventoryItem = Player.GetComponentInChildren<Inventory>().InventoryItems[i].gameObject;

                                InventoryItem.transform.parent = null;
                                Player.GetComponentInChildren<Inventory>().InventoryItems.Remove(InventoryItem);
                                Player.GetComponentInChildren<Inventory>().ResetItemPlacement();
                                while (InventoryItem.transform.position != gameObject.transform.position)
                                {
                                    InventoryItem.transform.position = new Vector3(Mathf.MoveTowards(InventoryItem.transform.position.x, gameObject.transform.position.x, Time.deltaTime * 5), Mathf.MoveTowards(InventoryItem.transform.position.y, gameObject.transform.position.y, Time.deltaTime * 5), Mathf.MoveTowards(InventoryItem.transform.position.z, gameObject.transform.position.z, Time.deltaTime * 5));
                                    yield return new WaitForEndOfFrame();
                                }
                            
                            if (InventoryItem.transform.position == gameObject.transform.position)
                            {
                                currentAmountOfWater += 1;
                                CurrentAmountOfItems();
                                Debug.Log(currentAmountOfWater);
                                Destroy(InventoryItem);
                                Player.GetComponentInChildren<Inventory>().ResetItemPlacement();
                            }
                        }
                    }

                    Debug.Log("Transferring mats");

                    _Transfering = true;

                    _Plant_Still_Growing = true;

                    _Plant_Done_Growing = false;

                    _Transfering = false;

                    if (_Transfering == false)
                    {
                        break;
                    }
                }
            }
        }

    }

    private void Update()
    {

        //if (itemForGrowth.currentAmountOfAllItems >= itemForGrowth.allItemsNeeded)
        //{

        //    //_Plant_Still_Growing = false;

        //    //_Plant_Done_Growing = true;

        //    //Debug.Log("Plant done growing");

        //}

    }


    public void CurrentAmountOfItems()
    {
        currentAmountOfAllItems = currentAmountOfSeeds + currentAmountOfWater + currentAmountOfSoil;

        if(itemForGrowth != null)
        {
            currentAmountOfAllItemsPercentage = (currentAmountOfAllItems / itemForGrowth.allItemsNeeded) * 100;
        }
    }

}
