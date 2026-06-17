using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class ItemList : MonoBehaviour
{
    public List<GameObject> Item;

    public enum ObtainablItems 
    {
        Seed,
        Water,
        Soil,
        Flower,
        Crops
    }

    [SerializeField] public ObtainablItems obtainablItems;

    private enum CropSeeds
    {
        
    }
    [SerializeField] private CropSeeds cropSeed;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
