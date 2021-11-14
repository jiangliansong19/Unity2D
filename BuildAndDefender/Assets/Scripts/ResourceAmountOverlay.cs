using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 建造建筑时，显示建筑生产效率
/// </summary>
public class ResourceAmountOverlay : MonoBehaviour
{
    private float detectTimer;
    private float detectTimeMax = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //只有资源生产建筑，才会显示生产效率
        BuildingTypeSO buildingType = BuildingManager.Instance.GetActiveBuildingType();
        if (buildingType.resourceGeneratorData.resourceType != null)
        {
            detectTimer -= Time.deltaTime;
            if (detectTimer <= 0)
            {
                detectTimer += detectTimeMax;
                DetectResourceAmount(buildingType);
            }

            if (Input.GetMouseButtonDown(0))
            {
                detectTimer += 2.5f;
            }
        }
    }

    private void DetectResourceAmount(BuildingTypeSO typeSO)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(UtilClass.GetMouseWorldPoint(), typeSO.resourceGeneratorData.resourceDetectionRadius);
        int amount = 0;
        foreach (Collider2D item in colliders)
        {
            ResourceNode node = item.gameObject.GetComponent<ResourceNode>();
            if (node != null && node.resourceType == typeSO.resourceGeneratorData.resourceType)
            {
                amount++;
            }
        }

        float amountPercent = (float)amount / typeSO.resourceGeneratorData.maxResourceAmount * 1.00f;

        ToolTipsUI.Instance.Show((int)(amountPercent * 100) + "%", new ToolTipsUI.ToolTipsUITimer { timer = 2f });
    }

}
