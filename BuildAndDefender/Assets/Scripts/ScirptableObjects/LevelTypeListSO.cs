using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjects/LevelTypeListSO")]
public class LevelTypeListSO : ScriptableObject
{
    public List<LevelTypeSO> list;
}
