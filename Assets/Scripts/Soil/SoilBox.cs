using UnityEngine;
using TMPro;
using Unity.Mathematics;

public class SoilBox : MonoBehaviour
{
    public float Level;
    public Inventory Inventory; //this is assigned in Soilupdater because i dont know...
    public float GeneratedSoil;
    [SerializeField] private TextMeshProUGUI textAmountSoil;
    [SerializeField] private float StartTime;
    private float PassedTime;
    private GameObject Player;
    //[SerializeField] private GameObject SoilPrefab;
    void Start()
    {
        Level = 0;
        GeneratedSoil = 0f;
    }

    void Update()
    {
        GenerateWater();
        
        textAmountSoil.text = GeneratedSoil.ToString();
    }

    //player getting water
    private void OnTriggerStay(Collider other) 
    {
        if (other.gameObject.name == "Player" && math.round(GeneratedSoil) > 0)
        {
            Debug.Log("Player was thirsty and got some water");
            float number = GeneratedSoil;
            Inventory.AddObjectToInventory(gameObject);
            GeneratedSoil -= number;
            PassedTime = 0f;
        };
    }

    public void UpdateWell() //called in waterupdater
    {
        Level++;
        Debug.Log(Level);
    }

    public void GenerateWater() 
    {
        if (GeneratedSoil >= 1f) return; // Al vol, niks doen

        PassedTime += Time.deltaTime;

        float timeNeeded = Mathf.Max(0.1f, StartTime - Level); // Nooit negatief of 0
    
        if (PassedTime >= timeNeeded)
        {
            GeneratedSoil = 1f;
            PassedTime = 0f;
        }
    }
}
