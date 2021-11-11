using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BuildingType
{
    House,
    Shop,
    Hall,
    Road,
    SuperMarket,

}


[CreateAssetMenu(fileName = "ScriptableObject/Buildings/BuildingTypeSO")]
public class BuildingTypeSO : ScriptableObject
{
    public BuildingType type;//类型
    public string buildingName;//建筑名
    public Transform prefab;//UI模型

    public BuildingTypeData data;


    [System.Serializable]
    public class BuildingTypeData
    {
        public int constructCost;//建造费用
        public int originIncomePerDay;//每日收支

        public int influenceRadius;//建筑辐射半径
    }
}
