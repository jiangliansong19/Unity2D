using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject/DefenserListSO")]
public class DefenserListSO : ScriptableObject
{
    public List<DefenserSO> list;
}
