using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Mathematics;
using Random = UnityEngine.Random;

public class SoilBox : MonoBehaviour
{
    public float Level;
    public Inventory Inventory; //this is assigned in Soilupdater because i dont know...
    public float GeneratedSoil;
    [SerializeField] private TextMeshProUGUI textAmountSoil;
    [SerializeField] private float StartTime;
    private float PassedTime;
    private GameObject Player;
    [SerializeField] private GameObject SoilPrefab;
    [SerializeField] private SoilUpdater updaterScript;

    [SerializeField] private GameObject hive;
    
    [SerializeField] private List<GameObject> Lots;
    private GameObject targetedLot;

    public enum SoilState
    {
        Empty,
        Hive,
    }

    public SoilState currentState;
    
    void Start()
    {
        Level = 0;
        GeneratedSoil = 0f;
    }

    void Update()
    {
        GenerateSoil();

        if (GeneratedSoil >= 1f)
        {
            GeneratedSoil = 0f;
            FindValidLot(Lots);
        }
        
        textAmountSoil.text = GeneratedSoil.ToString();

        SetModelsActive();
    }


    public void UpdateSoil()
    {
        switch (currentState)
        {
            case SoilState.Empty:
                Sound_Manage_III.Instance._Play_Sound(6);
                currentState =  SoilState.Hive;
                updaterScript.UpdateNeededAmount();
                break;
            case SoilState.Hive:
                break;
                
        }
    }

    private void SetModelsActive()
    {
        hive.SetActive(currentState == SoilState.Hive);
    }

    public void GenerateSoil() 
    {
        if (GeneratedSoil >= 1f || currentState == SoilState.Empty) return; // Emtpy or full, do nothing

        PassedTime += Time.deltaTime;

        float timeNeeded = Mathf.Max(0.1f, StartTime - Level); // Never negative
    
        if (PassedTime >= timeNeeded)
        {
            GeneratedSoil = 1f;
            PassedTime = 0f;
        }
    }

    private void FindValidLot(List<GameObject> Lots)
    {
        List<GameObject> ValidLots = new List<GameObject>();
        foreach (GameObject lot in Lots)
        {
            //if valid, add to validlots
            if (lot.GetComponent<Lot_Manager>().currentAmountOfSoil < 1)
            {
                ValidLots.Add(lot);
            };
        }
        FindLot(ValidLots);
    }

    private void FindLot(List<GameObject> ValidLots)
    {
        targetedLot = ValidLots[Random.Range(0, ValidLots.Count)];
        SoilLot(targetedLot);
    }

    private void SoilLot(GameObject Lot)
    {
        //soil the lot
        Lot.GetComponent<Lot_Manager>().currentAmountOfSoil++;
    }
    
}
