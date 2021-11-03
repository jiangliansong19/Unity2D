using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject/Defenser/DefenserSO")]
public class DefenserSO : ScriptableObject
{
    public string defenserName;
    public int currentLevel;
    public List<DefenserLevel> levels;
}




