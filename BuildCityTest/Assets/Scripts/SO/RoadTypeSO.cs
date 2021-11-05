using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum RoadPartType
{
    horizontal,
    vertical,
    corner,
    cross,
}

[CreateAssetMenu(fileName = "ScriptableObject/Buildings/RoadTypeSO")]
public class RoadTypeSO : BuildingTypeSO
{
    [HeaderAttribute("horizontal, vertical, corner, corss")]
    public Transform[] partPrefabs;
}
