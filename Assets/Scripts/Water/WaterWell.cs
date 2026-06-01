using System;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class WaterWell : MonoBehaviour
{
    public float Level;
    public Inventory Inventory; //this is assigned in waterupdater because i dont know...
    public float GeneratedWater;
    [SerializeField] private TextMeshProUGUI textAmountWater;
    [SerializeField] private float StartTime;
    private float PassedTime;
    private GameObject Player;
    [SerializeField] private GameObject WaterPrefab;
    void Start()
    {
        Level = 0;
        GeneratedWater = 0f;
    }

    void Update()
    {
        GenerateWater();
        
        textAmountWater.text = GeneratedWater.ToString();
    }

    //player getting water
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

    public void UpdateWell() //called in waterupdater
    {
        Level++;
        Debug.Log(Level);
    }

    public void GenerateWater() 
    {
        if (GeneratedWater >= 1f) return; // Al vol, niks doen

        PassedTime += Time.deltaTime;

        float timeNeeded = Mathf.Max(0.1f, StartTime - Level); // Nooit negatief of 0
    
        if (PassedTime >= timeNeeded)
        {
            GeneratedWater = 1f;
            PassedTime = 0f;
        }
    }
}