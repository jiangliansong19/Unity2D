using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject/EnermyListSO")]
public class EnermyListSO : ScriptableObject
{
    public List<EnermySO> list;
}
