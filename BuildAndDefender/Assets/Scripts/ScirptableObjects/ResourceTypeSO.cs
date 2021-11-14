using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="ScriptableObjects/ResourceType")]
public class ResourceTypeSO : ScriptableObject
{
    public string nameString;
    public string nameShort;
    public Sprite sprite;
    public string colorHex;
}
