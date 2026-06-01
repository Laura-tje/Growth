using UnityEngine;
using TMPro;
using Unity.Mathematics;

public class SoilBox : MonoBehaviour
{
    public float Level;
    public Inventory Inventory; //this is assigned in soilupdater because i dont know...
    public float GeneratedSoil;
    [SerializeField] private TextMeshProUGUI textAmountSoil;
    void Start()
    {
        Level = 1;
        GeneratedSoil = 0f;
    }

    void Update()
    {
        GenerateWater();
    }

    //player getting water
    private void OnTriggerStay(Collider other) 
    {
        if (other.gameObject.name == "Player" && math.round(GeneratedSoil) > 0)
        {
            Debug.Log("Player was hungry and got some soil");
            float number = GeneratedSoil;
            //Inventory.addToInventory(other.gameObject);
            GeneratedSoil -= number;
        };
    }

    public void UpdateWell() //called in waterupdater
    {
        Level++;
        Debug.Log(Level);
    }

    public void GenerateWater()
    {
        GeneratedSoil += (Time.deltaTime * (Level));
        textAmountSoil.text = math.round(GeneratedSoil).ToString();
    }
}
