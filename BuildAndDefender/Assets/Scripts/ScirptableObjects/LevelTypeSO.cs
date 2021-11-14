using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjects/LevelType")]
public class LevelTypeSO : ScriptableObject
{
    string levelDescription;
    public ResourceAmount[] originResources;
}
