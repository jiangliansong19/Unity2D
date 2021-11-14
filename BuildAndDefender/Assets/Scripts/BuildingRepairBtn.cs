using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �����޸�����������ʾ�����أ�����¼�(֧���޸�����)
/// </summary>
public class BuildingRepairBtn : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private ResourceTypeSO goldResourceType;

    // Start is called before the first frame update
    void Start()
    {
        transform.Find("Button").GetComponent<Button>().onClick.AddListener(() =>
        {
            int missingHealth = healthSystem.GetHealthAmountMax() - healthSystem.GetHealthAmount();
            int repaireCost = missingHealth / 2;

            ResourceAmount[] resourceAmounts = new ResourceAmount[]
            {
                new ResourceAmount { resourceType = goldResourceType, amount = repaireCost }
            };

            if (ResourceManager.Instance.CanAffordResources(resourceAmounts))
            {
                ResourceManager.Instance.SpendResources(resourceAmounts);
                healthSystem.HealFull();
            } 
            else
            {
                ToolTipsUI.Instance.Show("Can not afford repaire cost", new ToolTipsUI.ToolTipsUITimer { timer = 2f });
            }
        });
    }
}
