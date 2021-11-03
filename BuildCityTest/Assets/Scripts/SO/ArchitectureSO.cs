using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ScriptableObject/Architecture")]
public class ArchitectureSO : ScriptableObject
{
    public string typeName;
    public Transform prefab;
    public BuildingTypeListSO list;
}
