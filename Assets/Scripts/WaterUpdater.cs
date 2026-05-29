using UnityEngine;

public class WaterUpdater : MonoBehaviour
{
    [SerializeField] WaterWell Well;

    [SerializeField] private float neededAmount;
    [SerializeField] private float increaseAmount;

    private Inventory inventoryScript;
    private GameObject player;
    
    void Start()
    {
        Well = GetComponentInParent<WaterWell>();

        if (Well == null)
        {
            Debug.Log("you fucked up getting the water well from updater");
        }
        
        inventoryScript = player.GetComponent<Inventory>();
        
        Well.Inventory = inventoryScript;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            Debug.Log("Player stubbed his toe against the water updater");

            if ( /*inventoryScript.amountofcrops >= neededAmount*/ true) //FIX THISSSSS WHEN TOM MAKES IT!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            {
                Well.UpdateWell();
                //inventory -= neededamount
                Debug.Log("The player successfully updated the water well to convenience himself. How selfish...");
            } else if ( /*inventoryScript.amountofcrops < neededAmount*/ false) //FIX THISSSS AS WELLLLLLLLLLL (WATERWELL)!!!!!!!!!!!!!!!!!!!!!!
            {
                /*let amount = inventoryScript.amountofcrops
                inventory -= amount
                neededamount -= amount*/
                Debug.Log("The player has miscounted their crops and has to go get more. Until then the crops will be safely putt away.");

            }
            
        }
    }
}
