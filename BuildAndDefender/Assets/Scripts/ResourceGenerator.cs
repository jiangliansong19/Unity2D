using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    private float maxTime = 2.0f;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        BuildingTypeHolder holder = GetComponent<BuildingTypeHolder>();
        maxTime = holder.type.resourceGeneratorData.timerMax;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <=0)
        {
            timer += maxTime;
            ResourceManager.Instance.AddResources(ResourceManager.Instance.resourceTypeListSO.list[0], 2);
        }
    }
}
