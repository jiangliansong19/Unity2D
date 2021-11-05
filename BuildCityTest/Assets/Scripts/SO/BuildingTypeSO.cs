using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject/Buildings/BuildingTypeSO")]
public class BuildingTypeSO : ScriptableObject
{
    public BuildingType type;//类型
    public string buildingName;//建筑名
    public Transform prefab;//UI模型
}
