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
    [SerializeField] private GameObject SoilPrefab;

    [SerializeField] private GameObject hive;

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
        
        textAmountSoil.text = GeneratedSoil.ToString();

        SetModelsActive();
    }

    //player getting water
    private void OnTriggerStay(Collider other) 
    {
        if (other.gameObject.name == "Player" && math.round(GeneratedSoil) > 0)
        {
            Debug.Log("Player was hungry and got some soil");
            float number = GeneratedSoil;
            Inventory.AddObjectToInventory(gameObject);
            GeneratedSoil -= number;
            PassedTime = 0f;
        };
    }

    public void UpdateSoil()
    {
        switch (currentState)
        {
            case SoilState.Empty:
                currentState =  SoilState.Hive;
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
}
