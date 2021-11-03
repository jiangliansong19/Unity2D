using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ScriptableObject/Enermy/EnermySO")]
public class EnermySO : ScriptableObject
{
    public string enermyName;
    public Transform prefab;
    public int health;
    public float moveSpeed;

}


