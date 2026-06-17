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
    [SerializeField] WaterUpdater updaterScript;
    
    [SerializeField] private GameObject can;
    [SerializeField] private GameObject hose;
    [SerializeField] private GameObject well;

    public enum WaterState
    {
        Empty,
        Can,
        Hose,
        Well,
    }
    
    public WaterState currentState;

    void Start()
    {
        Level = 0;
        GeneratedWater = 0f;
    }

    void Update()
    {
        GenerateWater();
        
        textAmountWater.text = GeneratedWater.ToString();

        SetModelsActive();
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
        switch (currentState)
        {
            case WaterState.Empty:
                currentState = WaterState.Can;
                updaterScript.UpdateNeededAmount();
                break;
            case WaterState.Can:
                currentState = WaterState.Hose;
                updaterScript.UpdateNeededAmount();
                break;
            case WaterState.Hose:
                currentState = WaterState.Well;
                updaterScript.UpdateNeededAmount();
                break;
            case WaterState.Well:
                break;
        }
    }

    private void SetModelsActive()
    {
        can.SetActive(currentState == WaterState.Can);
        hose.SetActive(currentState == WaterState.Hose);
        well.SetActive(currentState == WaterState.Well);
    }

    public void GenerateWater() 
    {
        if (GeneratedWater >= 1f || currentState == WaterState.Empty) return; // Emtpy or full, do nothing

        PassedTime += Time.deltaTime;

        float timeNeeded = Mathf.Max(0.1f, StartTime - Level); // Never negative
    
        if (PassedTime >= timeNeeded)
        {
            GeneratedWater = 1f;
            PassedTime = 0f;
        }
    }
}