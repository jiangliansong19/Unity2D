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

}
