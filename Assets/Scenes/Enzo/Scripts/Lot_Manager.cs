using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Lot_Manager : MonoBehaviour
{

    [SerializeField] private GameObject _Plant;
    [SerializeField] private GameObject _Player;

    [SerializeField] private bool _Plant_Still_Growing;
    [SerializeField] private bool _Plant_Done_Growing;
    [SerializeField] private bool _Transfering;

    [SerializeField] private ItemForGrowth itemForGrowth;

    public enum TypeFlowers
    {
        Rose,
        Lily,
        Laveneder,
        Sunflower
    }

    [SerializeField] public TypeFlowers typeFlower;

    //[SerializeField] private int _Current_Amount_Mats;
    //private int _Max_Amount_Mats_Grow;

    private void Start()
    {
        itemForGrowth.MaxAmountOfItemsNeeded();
        itemForGrowth.CurrentAmountOfItems();

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

            if (itemForGrowth.currentAmountOfAllItems == itemForGrowth.allItemsNeeded)
            {

                yield return null;

                _Transfering = false;

                _Plant_Still_Growing = false;

                _Plant_Done_Growing = true;

                Debug.Log("Transfering mats stopped");

            }

            else if (itemForGrowth.currentAmountOfAllItems <= itemForGrowth.allItemsNeeded)
            {

                while (itemForGrowth.currentAmountOfAllItems <= itemForGrowth.allItemsNeeded)
                {

                    yield return new WaitForSeconds(0.1f);

                    Seeds currentSeeds;
                    GameObject InventoryItem;

                    for (int i = 0; i < Player.GetComponentInChildren<Inventory>().InventoryItems.Count; i++)
                    {
                        if (Player.GetComponentInChildren<Inventory>().InventoryItems[i].GetComponent<Seeds>() != null)
                        {
                            currentSeeds = Player.GetComponentInChildren<Inventory>().InventoryItems[i].GetComponent<Seeds>();
                            InventoryItem = Player.GetComponentInChildren<Inventory>().InventoryItems[i].gameObject;

                            if (currentSeeds.flowerSeed.GetType().GetEnumName(currentSeeds.flowerSeed) == typeFlower.GetType().GetEnumName(typeFlower))
                            {
                                InventoryItem.transform.parent = null;
                                while(InventoryItem.transform.position != gameObject.transform.position)
                                {
                                    InventoryItem.transform.position = new Vector3(Mathf.MoveTowards(InventoryItem.transform.position.x, gameObject.transform.position.x, Time.deltaTime * 5), Mathf.MoveTowards(InventoryItem.transform.position.y, gameObject.transform.position.y, Time.deltaTime * 5), Mathf.MoveTowards(InventoryItem.transform.position.z, gameObject.transform.position.z, Time.deltaTime * 5));
                                    yield return new WaitForEndOfFrame();
                                }
                                Player.GetComponentInChildren<Inventory>().InventoryItems.Remove(InventoryItem);
                                Player.GetComponentInChildren<Inventory>().seeds.Remove(InventoryItem);
                            }
                            if(InventoryItem.transform.position == gameObject.transform.position)
                            {
                                Destroy(InventoryItem);
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

}
