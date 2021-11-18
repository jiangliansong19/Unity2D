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


[CreateAssetMenu(fileName = "ScriptableObject/Roads/RoadTypeSO")]
public class RoadTypeSO : BuildingTypeSO
{
    public RoadPartType partType;
}
