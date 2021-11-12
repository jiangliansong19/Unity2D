using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{

    public static ResourceManager Instance { private set; get; }

    public event EventHandler OnResourceAmountChanged;

    public ResourceTypeListSO resourceTypeListSO;
    private Dictionary<ResourceTypeSO, int> resourceAmountDictionary;

    private void Awake()
    {
        Instance = this;
        resourceTypeListSO = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
        resourceAmountDictionary = new Dictionary<ResourceTypeSO, int>();

        foreach (ResourceTypeSO so in resourceTypeListSO.list)
        {
            resourceAmountDictionary[so] = 0;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.I))
        {
            resourceTypeListSO = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);

            //AddResources(resourceTypeListSO.list[0], 2);
        }
    }

    public void AddResources(ResourceTypeSO so, int count)
    {
        resourceAmountDictionary[so] += count;
        OnResourceAmountChanged?.Invoke(this, EventArgs.Empty);
        //Debug.Log(so.nameString + " resource count = " + resourceAmountDictionary[so].ToString());
    }

    public void SpendResources(ResourceAmount[] resourceAmounts)
    {
        foreach (ResourceAmount item in resourceAmounts)
        {
            resourceAmountDictionary[item.resourceType] -= item.amount;
        }
    }

    public bool CanAffordResources(ResourceAmount[] resourceAmounts)
    {
        foreach (ResourceAmount item in resourceAmounts)
        {
            if (resourceAmountDictionary[item.resourceType] < item.amount)
            {
                return false;
            }
        }
        return true;
    }

    public int getResourceAmount(ResourceTypeSO so)
    {
        return resourceAmountDictionary[so];
    }
}
