using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 显示游戏资源数量
/// </summary>
public class ResourceUI : MonoBehaviour
{
    private ResourceTypeListSO resourceTypeList;
    private Dictionary<ResourceTypeSO, Transform> resourceTypeTransfromDict;

    private void Awake()
    {
        resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
        resourceTypeTransfromDict = new Dictionary<ResourceTypeSO, Transform>();

        Transform template = transform.Find("ResourceTemplate");
        template.gameObject.SetActive(false);

        int index = 0;
        foreach (ResourceTypeSO so in resourceTypeList.list)
        {
            Transform resourceTransform = Instantiate(template, transform);
            resourceTransform.gameObject.SetActive(true);

            float offsetAmount = -160f;
            resourceTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);

            resourceTransform.Find("Image").GetComponent<Image>().sprite = so.sprite;

            resourceTypeTransfromDict[so] = resourceTransform;

            index++;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        ResourceManager.Instance.OnResourceAmountChanged += ResourceManager_OnResourceAmountChanged; 
    }

    private void ResourceManager_OnResourceAmountChanged(object sender, System.EventArgs e)
    {
        UpdateResourceAmount();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateResourceAmount()
    {
        foreach (ResourceTypeSO so in resourceTypeList.list)
        {
            Transform resourceTransform = resourceTypeTransfromDict[so];

            int resourceAmount = ResourceManager.Instance.GetResourceTypeAmount(so);
            resourceTransform.Find("Text").GetComponent<TextMeshProUGUI>().SetText(resourceAmount.ToString());
        }
    }

}
