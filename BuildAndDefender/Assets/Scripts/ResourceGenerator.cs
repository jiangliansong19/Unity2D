using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    private ResourceGeneratorData resourceGeneratorData;

    private float maxTime = 2.0f;

    private float timer;

    private void Awake()
    {
        resourceGeneratorData = GetComponent<BuildingTypeHolder>().type.resourceGeneratorData;
        maxTime = resourceGeneratorData.timerMax;
    }

    // Start is called before the first frame update
    void Start()
    {
        //检测范围内的资源
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, resourceGeneratorData.resourceDetectionRadius);

        int nearByResourAmount = 0;
        foreach (Collider2D item in collider2DArray)
        {
            ResourceNode resourceNode = item.GetComponent<ResourceNode>();
            if (resourceNode != null)
            {
                if (resourceNode.resourceType == resourceGeneratorData.resourceType)
                {
                    nearByResourAmount++;
                }
            }
        }

        nearByResourAmount = Mathf.Clamp(nearByResourAmount, 0, 10);

        if (nearByResourAmount == 0)
        {
            enabled = false;
        }
        else
        {
            maxTime = (resourceGeneratorData.timerMax / 2f) + resourceGeneratorData.timerMax * (1 - (float)nearByResourAmount / 10);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <=0)
        {
            timer += maxTime;
            ResourceManager.Instance.AddResources(resourceGeneratorData.resourceType, 1);
        }
    }

    public ResourceGeneratorData GetResourceGeneratorData()
    {
        return resourceGeneratorData;
    }

    public float GetTimerNormalized()
    {
        return timer / maxTime;
    }

    public float GetAmountGeneratorPerSecond()
    {
        return 1 / maxTime;
    }
}
