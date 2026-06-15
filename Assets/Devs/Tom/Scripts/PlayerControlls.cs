using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControlls : MonoBehaviour
{
    [Header("Player Controlls")]
    [SerializeField] private PlayerInput inputSystem;

    private InputActionMap _currentMap;

    private InputAction _moveAction;
    [SerializeField] private GameObject _playerChild;
    public Vector3 move { get; private set; }
    [SerializeField] private float M_Speed;

    [SerializeField] private GameObject Inventory;

    [SerializeField] private Animator animator;

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
        

           if (move != Vector3.zero)
           {
               Quaternion targetRotation = Quaternion.LookRotation(-move);
               _playerChild.transform.rotation = Quaternion.RotateTowards(_playerChild.transform.rotation, targetRotation, 500f * Time.deltaTime);

                //Enzo here, this is the line I added
                animator.SetBool("Walking", true);
           }
           //Enzo here again, I added this too.
           else
           {
                animator.SetBool("Walking", false);
           }
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

}
