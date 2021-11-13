using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ScriptableObjects/BuildType")]
public class BuildingTypeSO : ScriptableObject
{
    public string nameString;
    public Transform prefab;

    public ResourceGeneratorData resourceGeneratorData;

    public float minConstructionRadius;
    public Sprite sprite;

    public ResourceAmount[] constructionResourceCostArray;

    public DefenderData defenderData;

    public int health;

    public string GetConstructionResourceCostString()
    {
        string str = "";
        foreach (ResourceAmount resourceAmount in constructionResourceCostArray)
        {
            str += "<color=#" + resourceAmount.resourceType.colorHex + ">" 
                + resourceAmount.resourceType.nameShort + resourceAmount.amount + "</color> ";
        }
        return str;
    }
}
