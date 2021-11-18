using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 记录每个模型运行时的真实数据。
/// </summary>
public class BuildingRunData : MonoBehaviour
{
    public int incomePerDay;//日收入
    public int influenceRadius;//建筑辐射半径

    [HideInInspector] public float incomeCorrectRadio;//收入修正比例
    [HideInInspector] public float influenceCorrctRadio;//辐射修正比例


    public string GetBuildingDescription()
    {
        BuildingTypeSO typeSO = GetComponent<BuildingTypeSOHolder>().buidlingTypeSO;

        string desc = "";

        desc += "Construction Cost " + UtilsClass.GetStringWithColor(typeSO.data.constructCost.ToString(), "#FF0000");

        if (typeSO.data.originIncomePerDay > 0)
        {
            desc += "Income/Day " + UtilsClass.GetStringWithColor(typeSO.data.originIncomePerDay.ToString(), "#00FF00");
        }

        return desc;
    }
}
