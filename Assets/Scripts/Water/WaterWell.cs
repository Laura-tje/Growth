using System;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

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
    
    [SerializeField] private List<GameObject> Lots;
    private GameObject targetedLot;
    
    [SerializeField] private GameObject UpgradeParticle;

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

        if (GeneratedWater >= 1f)
        {
            GeneratedWater = 0f;
            FindValidLot(Lots);
        }
        
        textAmountWater.text = GeneratedWater.ToString();

        SetModelsActive();
    }
    

    public void UpdateWell() //called in waterupdater
    {
        Instantiate(UpgradeParticle, transform.position, Quaternion.identity );
        switch (currentState)
        {
            case WaterState.Empty:
                Sound_Manage_III.Instance._Play_Sound(6);
                currentState = WaterState.Can;
                updaterScript.UpdateNeededAmount();
                break;
            case WaterState.Can:
                Sound_Manage_III.Instance._Play_Sound(6);
                currentState = WaterState.Hose;
                updaterScript.UpdateNeededAmount();
                break;
            case WaterState.Hose:
                Sound_Manage_III.Instance._Play_Sound(6);
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

    private void FindValidLot(List<GameObject> Lots)
    {
        List<GameObject> ValidLots = new List<GameObject>();
        foreach (GameObject lot in Lots)
        {
            //if valid, add to validlots
            if (lot.GetComponent<Lot_Manager>().currentAmountOfWater < 1)
            {
                ValidLots.Add(lot);
            }
        }

        FindLot(ValidLots);
    }

    private void FindLot(List<GameObject> ValidLots)
    {
        targetedLot = ValidLots[Random.Range(0, ValidLots.Count)];
        WaterLot(targetedLot);
    }

    private void WaterLot(GameObject Lot)
    {
        //water the lot
        Lot.GetComponent<Lot_Manager>().currentAmountOfWater++;
    }
}