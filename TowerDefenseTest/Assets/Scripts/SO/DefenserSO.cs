using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DefenserType
{
    Monster
}

[CreateAssetMenu(fileName = "ScriptableObject/Defenser/DefenserSO")]
public class DefenserSO : ScriptableObject
{
    public string defenserName;
    public DefenserType type;
    public Texture2D image;
    public Transform[] prefabs;
}




