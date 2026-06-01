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
    void Start()
    {
        Level = 1;
        GeneratedWater = 0f;
    }

    void Update()
    {
        GenerateWater();
    }

    //player getting water
    private void OnTriggerStay(Collider other) 
    {
        if (other.gameObject.name == "Player" && math.round(GeneratedWater) > 0)
        {
            Debug.Log("Player was thirsty and got some water");
            float number = GeneratedWater;
            //Inventory.addToInventory(other.gameObject);
            GeneratedWater -= number;
        };
    }

    public void UpdateWell() //called in waterupdater
    {
        Level++;
        Debug.Log(Level);
    }

    public void GenerateWater()
    {
        GeneratedWater += (Time.deltaTime * (Level));
        textAmountWater.text = math.round(GeneratedWater).ToString();
    }
}
