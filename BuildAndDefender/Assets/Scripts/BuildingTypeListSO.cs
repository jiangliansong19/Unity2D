using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ScriptableObjects/BuildingTypeListSO")]
public class BuildingTypeListSO : ScriptableObject
{
    public List<BuildingTypeSO> list;
}
