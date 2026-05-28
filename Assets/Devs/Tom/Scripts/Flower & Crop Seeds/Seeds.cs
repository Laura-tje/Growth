using UnityEngine;

public class Seeds : MonoBehaviour
{
    public GameObject Seed;

    private enum FlowerSeeds 
    {
        Rose,
        Lily,
        Laveneder,
        Sunflower
    }

    [SerializeField] private FlowerSeeds flowerSeed;

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
