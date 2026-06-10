using UnityEngine;

[CreateAssetMenu(fileName = "ItemForGrowth", menuName = "Scriptable Objects/ItemForGrowth")]
public class ItemForGrowth : ScriptableObject
{
    public int seedsNeeded;

    public int waterNeeded;

    public int soilNeeded;

    public float allItemsNeeded;

    public GameObject plantToGrow; 

    public void MaxAmountOfItemsNeeded()
    {
        allItemsNeeded = seedsNeeded + waterNeeded + soilNeeded;
    }

}
