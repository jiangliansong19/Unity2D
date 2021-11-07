using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ScriptableObjects/BuildType")]
public class BuildingTypeSO : ScriptableObject
{
    public string nameString;
    public Transform prefab;

    public ResourceGeneratorData resourceGeneratorData;

    public Sprite sprite;
}
