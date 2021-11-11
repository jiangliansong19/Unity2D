using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResourceGeneratorData
{
    [HeaderAttribute("How many seconds to generate one.")]
    public float timerMax;

    public ResourceTypeSO resourceType;

    public float resourceDetectionRadius;

    public int maxResourceAmount;
}
