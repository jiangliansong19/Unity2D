using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTypeSelectUI : MonoBehaviour
{
    private BuildingTypeListSO buldingTypeList;

    private void Awake()
    {
        buldingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);

        Transform template = transform.Find("BuildingTemplate");
        template.gameObject.SetActive(false);

        int index = 0;
        foreach (BuildingTypeSO so in buldingTypeList.list)
        {
            Transform buildingTransform = Instantiate(template, transform);
            buildingTransform.gameObject.SetActive(true);

            float offsetAmount = 130f;
            buildingTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);

            buildingTransform.Find("BuildingImage").GetComponent<Image>().sprite = so.sprite;

            buildingTransform.GetComponent<Button>().onClick.AddListener(() =>
            {
                BuildingManager.Instance.SetActiveBuildingType(so);
            });

            index++;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
