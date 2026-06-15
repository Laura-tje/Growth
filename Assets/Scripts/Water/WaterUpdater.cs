using UnityEngine;
using TMPro;

public class WaterUpdater : MonoBehaviour
{
    [SerializeField] WaterWell Well;

    private float neededAmount;
    [SerializeField] private float increaseAmount;

    private Inventory inventoryScript;
    private GameObject player;

    //public float ItemsNeeded;
    [SerializeField] TextMeshProUGUI ItemsNeededText;
    [SerializeField] private float[] upgradeRequirements = { 5f, 10f, 20f };
    private int currentUpgradeIndex = 0;
    
    void Start()
    {
        Well = GetComponentInParent<WaterWell>();

        if (Well == null)
        {
            Debug.Log("you fucked up getting the water well from updater");
        }
        
        player = GameObject.Find("Player");
        
        inventoryScript = player.GetComponentInChildren<Inventory>();

        if (inventoryScript == null)
        {
            Debug.Log("you once again fucked up your inventory getcomponent, you fuckup!");
        }
        
        Well.Inventory = inventoryScript;
        
        neededAmount = upgradeRequirements[currentUpgradeIndex];
        ItemsNeededText.text = neededAmount.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {


    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            float amountOfCrops = 0;
            Debug.Log("Player stubbed his toe against the water updater");
            for (int i = 0; i < inventoryScript.InventoryItems.Count; i++)
            {
                if (inventoryScript.InventoryItems[i].CompareTag("Crop"))
                {
                    amountOfCrops += 1;
                }
            }

            if ( amountOfCrops >= neededAmount)
            {
                neededAmount = 0;
                Well.UpdateWell();
                //inventory -= neededamount; take them from inventory!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                amountOfCrops = 0;
                ItemsNeededText.text = neededAmount.ToString();
                Debug.Log("The player successfully updated the water well to convenience himself. How selfish...");
            } 
            else if ( amountOfCrops < neededAmount)
            {
                neededAmount -= amountOfCrops;
                ItemsNeededText.text = neededAmount.ToString();
                //neededamount -= amount; take them from inventory!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                amountOfCrops = 0;
                Debug.Log("The player has miscounted their crops and has to go get more. Until then the crops will be safely putt away.");

            }
            
        }
    }

    public void UpdateNeededAmount()
    {
        currentUpgradeIndex++;

        if (currentUpgradeIndex < upgradeRequirements.Length)
        {
            neededAmount = upgradeRequirements[currentUpgradeIndex];
            ItemsNeededText.text = neededAmount.ToString();
        }
        else
        {
            ItemsNeededText.text = "MAX";
        }
    }
}
