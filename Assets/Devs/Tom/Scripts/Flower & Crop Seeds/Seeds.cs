using UnityEngine;

public class Seeds : MonoBehaviour
{
    public enum FlowerSeeds
    {
        Rose, 
        Lily,
        Lavender,
        Sunflower
    }

    [SerializeField] public FlowerSeeds flowerSeed; 

    private enum CropSeeds
    {

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Test()
    {
        Debug.Log("Work");
    }

}
