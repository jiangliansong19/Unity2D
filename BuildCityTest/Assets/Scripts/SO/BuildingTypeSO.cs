using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject/Buildings/BuildingTypeSO")]
public class BuildingTypeSO : ScriptableObject
{
    public string buildingName;//建筑名
    public Transform prefab;//UI模型
    public int constructCost;//建造费用
    public int dayIncome;//每日收支
}
