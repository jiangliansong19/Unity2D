using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTypeSelectUI : MonoBehaviour
{
    private Dictionary<BuildingTypeSO, Transform> buildingTypeTransformDict;
    private BuildingTypeListSO buldingTypeList;
    private Transform arrowButton;
    [SerializeField] private Sprite arrowSprite;

    private void Awake()
    {
        buildingTypeTransformDict = new Dictionary<BuildingTypeSO, Transform>();
        buldingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
        Transform template = transform.Find("BuildingTemplate");
        template.gameObject.SetActive(false);

        int index = 0;
        arrowButton = Instantiate(template, transform);
        arrowButton.gameObject.SetActive(true);

        float offsetAmount = 130f;
        arrowButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);

        arrowButton.Find("BuildingImage").GetComponent<Image>().sprite = arrowSprite;
        arrowButton.Find("BuildingImage").GetComponent<RectTransform>().sizeDelta = new Vector2(0, -30);

        arrowButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            BuildingManager.Instance.SetActiveBuildingType(null);
        });

        index++;


        foreach (BuildingTypeSO so in buldingTypeList.list)
        {
            Transform buildingTransform = Instantiate(template, transform);
            buildingTransform.gameObject.SetActive(true);

            buildingTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);

            buildingTransform.Find("BuildingImage").GetComponent<Image>().sprite = so.sprite;

            buildingTransform.GetComponent<Button>().onClick.AddListener(() =>
            {
                BuildingManager.Instance.SetActiveBuildingType(so);
            });

            buildingTypeTransformDict[so] = buildingTransform;

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
        UpdateActiveBuilgdingTypeButton();
    }

    private void UpdateActiveBuilgdingTypeButton()
    {
        foreach (BuildingTypeSO typeSO in buildingTypeTransformDict.Keys)
        {
            Transform transform = buildingTypeTransformDict[typeSO];
            transform.Find("Selected").gameObject.SetActive(false);
        }

        BuildingTypeSO activieTypeSO = BuildingManager.Instance.GetActiveBuildingType();
        if (activieTypeSO != null)
        {
            buildingTypeTransformDict[activieTypeSO].Find("Selected").gameObject.SetActive(true);
            arrowButton.Find("Selected").gameObject.SetActive(false);
        } 
        else
        {
            arrowButton.Find("Selected").gameObject.SetActive(true);
        }
    }
}
