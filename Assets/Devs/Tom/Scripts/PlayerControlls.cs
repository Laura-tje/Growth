using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControlls : MonoBehaviour
{
    [Header("Player Controlls")]
    [SerializeField] private PlayerInput inputSystem;

    private InputActionMap _currentMap;

    private InputAction _moveAction;
    public Vector3 move { get; private set; }
    [SerializeField] private float M_Speed;

    [SerializeField] private GameObject Inventory;

    private GameObject currentHarvestedItem;

    private void Awake()
    {
        _currentMap = inputSystem.currentActionMap;
        _moveAction = _currentMap.FindAction("Move");
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        move = new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            transform.Translate(move * M_Speed * Time.deltaTime);
           // transform.GetComponent<Rigidbody>().AddForce()
    }

    private void OnTriggerEnter(Collider other)
    {
        //if(other.gameObject.tag == "Harvestable Object" && Inventory.GetComponent<Inventory>().InventoryItems.Count < 5)
        //{
        //    GameObject currentObtainableItemInRange;

        //    currentObtainableItemInRange = other.gameObject;
            
        //    Inventory.GetComponent<Inventory>().InventoryItems.Add(currentHarvestedItem = Instantiate(currentObtainableItemInRange.GetComponent<ItemList>().Item, Inventory.transform.position, Inventory.transform.rotation, Inventory.gameObject.transform));

        //    if(currentObtainableItemInRange.GetComponent<ItemList>().obtainablItems == ItemList.ObtainablItems.Seed)
        //    {
        //        Inventory.GetComponent<Inventory>().seeds.Add(currentHarvestedItem);

        //        //currentObtainableItemInRange.GetComponent<Seeds>().Test();
        //    }

        //    AddObjectToInventory();
        //}
    }

    public void AddObjectToInventory(GameObject currentObtainableItemInRange)
    {
        Inventory.GetComponent<Inventory>().InventoryItems.Add(currentHarvestedItem = Instantiate(currentObtainableItemInRange.GetComponent<ItemList>().Item, Inventory.transform.position, Inventory.transform.rotation, Inventory.gameObject.transform));

        if (currentObtainableItemInRange.GetComponent<ItemList>().obtainablItems == ItemList.ObtainablItems.Seed)
        {
            Inventory.GetComponent<Inventory>().seeds.Add(currentHarvestedItem);

            //currentObtainableItemInRange.GetComponent<Seeds>().Test();
        }

        for (int i = 0; i < Inventory.GetComponent<Inventory>().InventoryItems.Count; i++)
        {
            for (int j = 0; j < Inventory.GetComponent<Inventory>().InventorySlots.Count; j++)
            {
                Inventory.GetComponent<Inventory>().InventoryItems[i].gameObject.transform.position = Inventory.GetComponent<Inventory>().InventorySlots[i].gameObject.transform.position;
            }
        }

        for (int i = 0; i < Inventory.GetComponent<Inventory>().InventoryItems.Count; i++)
        {
            Inventory.GetComponent<Inventory>().InventoryItems[i].transform.parent = Inventory.GetComponent<Inventory>().InventorySlots[i].gameObject.transform;
        }
    }
}
