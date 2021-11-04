using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 控制敌人的数量，等级，出现频率等，受到难度，关卡等因素影响
/// </summary>
public class EnermyManager : MonoBehaviour
{
    public static EnermyManager shared { private get; set; }
    

    private EnermyListSO enermyList;

    private float maxTime = 3.0f;
    private float currentTime;

    private void Awake()
    {
        shared = this;
        enermyList = Resources.Load<EnermyListSO>(typeof(ScriptableObject).Name + "/" + typeof(EnermyListSO).Name);
    }

    // Start is called before the first frame update
    void Start()
    {
        currentTime = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            createEnermy();
            currentTime = maxTime;
        }
    }


    private void createEnermy()
    {
        Transform obj = Instantiate(enermyList.list[0].prefab, new Vector3(-20.0f, 2.3f), Quaternion.identity);
    }
}
