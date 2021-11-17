using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum RoadPartType
{
    horizontal,
    vertical,
    corner,
    cross,
}

[System.Serializable]
public class RoadPartTypeInfo
{
    public RoadPartType type;
    public Transform prefab;
    public string partName;
}


[CreateAssetMenu(fileName = "ScriptableObject/Roads/RoadTypeSO")]
public class RoadTypeSO : BuildingTypeSO
{

    public List<RoadPartTypeInfo> partDatas;

    public RoadPartTypeInfo GetPartInfoByType(RoadPartType type)
    {
        foreach (RoadPartTypeInfo item in partDatas)
        {
            if (item.type == type)
            {
                return item;
            }
        }
        return null;
    }
}
