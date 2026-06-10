using UnityEngine;
using TMPro;

public class SoilFree : MonoBehaviour
{
    private float PassedTime;
    public float GeneratedSoil;
    [SerializeField] private float TimeNeeded;
    [SerializeField] private TextMeshProUGUI textAmountWater;
    
    private Inventory Inventory;
    private GameObject player;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");

        if (player == null)
        {
            Debug.Log("Player not found you dumb fuck");
        }
        
        Inventory = player.GetComponentInChildren<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        GenerateWater();
        textAmountWater.text = GeneratedSoil.ToString();
    }

    private void RotateTextTowardsPlayer()
    {
        
    }
    
    private void OnTriggerStay(Collider other) 
    {
        if (other.gameObject.name == "Player" && GeneratedSoil >= 1f)
        {
            Debug.Log("Player was hungry and got some soile");
            Inventory.AddObjectToInventory(gameObject);
            GeneratedSoil = 0f;
            PassedTime = 0f;
        };
    }
    
    public void GenerateWater() 
    {
        if (GeneratedSoil >= 1f) return; // Al vol, niks doen

        PassedTime += Time.deltaTime;
    
        if (PassedTime >= TimeNeeded)
        {
            GeneratedSoil = 1f;
            PassedTime = 0f;
        }
    }
}
