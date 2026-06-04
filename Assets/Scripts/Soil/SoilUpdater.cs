using UnityEngine;

public class SoilUpdater : MonoBehaviour
{
    [SerializeField] SoilBox Soil;

    [SerializeField] private float neededAmount;
    [SerializeField] private float increaseAmount;

    private Inventory inventoryScript;
    private GameObject player;
    
    void Start()
    {
        Soil = GetComponentInParent<SoilBox>();

        if (Soil == null)
        {
            Debug.Log("you fucked up getting the soil well from updater");
        }
        
        player = GameObject.Find("Player");
        
        inventoryScript = player.GetComponentInChildren<Inventory>();

        if (inventoryScript == null)
        {
            Debug.Log("you once again fucked up your inventory getcomponent, you fuckup!");
        }
        
        Soil.Inventory = inventoryScript;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            Debug.Log("Player stubbed his toe against the soil updater");

            if ( /*inventoryScript.amountofcrops >= neededAmount*/ true) //FIX THISSSSS WHEN TOM MAKES IT!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            {
                Soil.UpdateWell();
                //inventory -= neededamount
                Debug.Log("The player successfully updated the soil well to convenience himself. How selfish...");
            } else if ( /*inventoryScript.amountofcrops < neededAmount*/ false) //FIX THISSSS AS WELLLLLLLLLLL (SoilBox)!!!!!!!!!!!!!!!!!!!!!!
            {
                /*let amount = inventoryScript.amountofcrops
                inventory -= amount
                neededamount -= amount*/
                Debug.Log("The player has miscounted their crops and has to go get more. Until then the crops will be safely putt away.");

            }
            
        }
    }
}
