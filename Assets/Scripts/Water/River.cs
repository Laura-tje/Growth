using UnityEngine;
using TMPro;
using Unity.Mathematics;

public class River : MonoBehaviour
{
    private float PassedTime;
    public float GeneratedWater;
    [SerializeField] private float TimeNeeded;
    [SerializeField] private TextMeshProUGUI textAmountWater;
    
    public Inventory Inventory;
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
        textAmountWater.text = GeneratedWater.ToString();
    }

    private void RotateTextTowardsPlayer()
    {
        
    }
    
    private void OnTriggerStay(Collider other) 
    {
        if (other.gameObject.name == "Player" && math.round(GeneratedWater) > 0)
        {
            Debug.Log("Player was thirsty and got some water");
            float number = GeneratedWater;
            Inventory.AddObjectToInventory(gameObject);
            GeneratedWater -= number;
            PassedTime = 0f;
        };
    }
    
    public void GenerateWater() 
    {
        if (GeneratedWater >= 1f) return; // Al vol, niks doen

        PassedTime += Time.deltaTime;
    
        if (PassedTime >= TimeNeeded)
        {
            GeneratedWater = 1f;
            PassedTime = 0f;
        }
    }
}
