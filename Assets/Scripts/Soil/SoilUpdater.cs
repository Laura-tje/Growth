using UnityEngine;
using TMPro;

public class SoilUpdater : MonoBehaviour
{
    [SerializeField] SoilBox Soil;

    [SerializeField] private float neededAmount;
    [SerializeField] private float increaseAmount;

    private Inventory inventoryScript;
    private GameObject player;

    [SerializeField] private TextMeshProUGUI ItemsNeededText;
    [SerializeField] private float[] upgradeRequirements = { 20f };
    private int currentUpgradeIndex = 0;
    
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
            float amountOfFlowers = 0;
            Debug.Log("Player stubbed his toe against the soil updater");
            for (int i = 0; i < inventoryScript.InventoryItems.Count; i++)
            {
                if (inventoryScript.InventoryItems[i].CompareTag("Flower"))
                {
                    amountOfFlowers += 1;
                }
            }
            
            if (amountOfFlowers >= upgradeRequirements[currentUpgradeIndex])
            {
                neededAmount = 0;
                Soil.UpdateSoil();
                //inventory -= neededamount; take them from inventory!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                amountOfFlowers = 0;
                ItemsNeededText.text = upgradeRequirements[currentUpgradeIndex].ToString();
                Debug.Log("The player successfully updated the soil well to convenience himself. How selfish...");
            } 
            else if ( amountOfFlowers < upgradeRequirements[currentUpgradeIndex])
            {
                upgradeRequirements[currentUpgradeIndex] -= amountOfFlowers;
                ItemsNeededText.text = upgradeRequirements[currentUpgradeIndex].ToString();
                //neededamount -= amount; take them from inventory!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                amountOfFlowers = 0;
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
