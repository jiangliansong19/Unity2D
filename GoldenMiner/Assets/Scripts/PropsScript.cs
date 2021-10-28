using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PropsType
{
    None,
    Fraction, //分数道具
    Boom,  //炸弹
    Potion, //双倍药剂
}

public class PropsScript : MonoBehaviour
{
    public int fraction; //当前的道具分数
    public PropsType nowType; //当前道具类型
    public int scaleLevel = 1; //当前道具缩放，大小影响hook的速度

    public void UseProps()
    {
        switch (nowType) {
            case PropsType.Fraction:
                GameMode.Instance.AddFraction(fraction);
                break;
            case PropsType.Potion:
                GameMode.Instance.isDouble = true;
                break;
            case PropsType.Boom:
                GameMode.Instance.AddBoomProps();
                break;
        }
    }
    
}
