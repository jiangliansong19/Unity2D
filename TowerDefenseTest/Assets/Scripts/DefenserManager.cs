using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 防御塔数据管理，比如建造塔的消耗，升级的消耗，销毁的回收价等
/// </summary>
public class DefenserManager : MonoBehaviour
{
    public static DefenserManager sharedManager { private set; get; }

    public Dictionary<Transform, DefenserSO> defenserInfos;










    private void Awake()
    {
        sharedManager = this;
        defenserInfos = new Dictionary<Transform, DefenserSO>();
    }

    private void Update()
    {
        
    }
}
