using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;



/// <summary>
/// 建筑
/// </summary>
public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { private set; get; }

    [SerializeField] private GameObject cityCenter;

    private BuildingTypeSO activeBuildingTypeSO;

    [HideInInspector] public Dictionary<BuildingTypeSO, GameObject> buildingInfoDict;

    private GameObject BuildingInfoDialog;


    public event EventHandler<OnActiveBuildingTypeChangedHandlerArgs> OnActiveBuildingTypeChangedHandler;

    public class OnActiveBuildingTypeChangedHandlerArgs
    {
        public BuildingTypeSO Args_TypeSO;
    }

    private void Awake()
    {
        Instance = this;
        buildingInfoDict = new Dictionary<BuildingTypeSO, GameObject>();

    }

    // Start is called before the first frame update
    void Start()
    {
        CreateWorld();
    }

    // Update is called once per frame
    void Update()
    { 

        if (Input.GetMouseButtonDown(0) && activeBuildingTypeSO != null)

        {

            if (activeBuildingTypeSO.buildingName == "DigHole")
            {
                GameObject obj = UtilsClass.GetObjectByRay(UtilsClass.GetCurrentWorldPoint());
                if (obj != null && obj.tag == "Soil")
                {
                    Destroy(obj);
                }
            }
            else if (!EventSystem.current.IsPointerOverGameObject())
            {
                BuildBuilding();
            }

        }

        if (Input.GetMouseButtonDown(1))
        {
            SetActiveBuildingTypeSO(null);
        }

    }


    public void SetActiveBuildingTypeSO(BuildingTypeSO typeSO)
    {
        activeBuildingTypeSO = typeSO;
        OnActiveBuildingTypeChangedHandler?.Invoke(this, new OnActiveBuildingTypeChangedHandlerArgs
        {
            Args_TypeSO = activeBuildingTypeSO
        });
    }

    public BuildingTypeSO GetActiveBuildingTypeSO()
    {
        return activeBuildingTypeSO;
    }



    public void ScanBuilding()
    {
        Vector3 position = UtilsClass.GetCurrentWorldPoint();
        GameObject obj = UtilsClass.GetObjectByRay(position);
        if (obj != null && obj.tag.StartsWith("Building"))
        {
            //ShowBuildingInfoDialog(obj, position);
            BuildingRunData runData = obj.GetComponent<BuildingRunData>();
            BuildingTypeSOHolder holder = obj.GetComponent<BuildingTypeSOHolder>();

            string message = holder.buidlingTypeSO.buildingName + "\n" + runData.GetBuildingDescription();
            ToolTipsUI.Instance.ShowMessage(message, new ToolTipsUI.ShowTimer { time = 3 });
        }
    }

    public void BuildBuilding()
    {
        //BoxCollider2D collider = activeBuildingTypeSO.prefab.GetComponent<BoxCollider2D>();
        //Vector3 position = UtilsClass.GetCurrentWorldPoint();
        //Collider2D[] colliders = Physics2D.OverlapBoxAll(position + (Vector3)collider.offset, collider.size, 0);

        GameObject rayCastObj = UtilsClass.GetObjectByRay(UtilsClass.getRoundCurrentWorldPoint());

        bool isAreaClear = rayCastObj == null;
        if (!isAreaClear)
        {
            Debug.Log("Place is not clear");
            return;
        }

        Transform newObj = Instantiate(this.activeBuildingTypeSO.prefab, UtilsClass.getRoundCurrentWorldPoint(), Quaternion.identity);

        //受到道路影响，建筑辐射面积增加
        float radio = 1.0f;

        //计算建筑辐射范围内，受到的收入增幅是多少
        float income = activeBuildingTypeSO.data.originIncomePerDay * radio;


        //游戏总数据，资金减少，日收入增加
        GameDataManager.Instance.totalMoney -= activeBuildingTypeSO.data.constructCost;
        GameDataManager.Instance.IncomePerDay += income;
    }


    public void DestructBuilding(GameObject obj, Vector3 position)
    {
        GameObject gameObj = UtilsClass.GetObjectByRay(position);
        Destroy(gameObj);
    }

    public void UpdateBuilding(GameObject obj, Vector3 position)
    {

    }


    public void ShowBuildingInfoDialog(GameObject building, Vector3 position)
    {
        if (BuildingInfoDialog == null)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/Others/BuildingInfoDialog").gameObject;
            BuildingInfoDialog = Instantiate(prefab, position, Quaternion.identity);
        }
        else
        {
            BuildingInfoDialog.transform.position = position;
        }

        BuildingInfoDialog.GetComponent<SpriteRenderer>().sortingOrder = building.GetComponent<SpriteRenderer>().sortingOrder + 1;
        BuildingRunData data = building.GetComponent<BuildingRunData>();
        BuildingInfoDialog.transform.Find("Content").GetComponent<TMPro.TextMeshPro>().SetText(data.incomePerDay.ToString());
    }

    private void CreateWorld()
    {
        Transform waterTransform = Resources.Load<Transform>("Prefabs/pfWater");
        Transform soilTransform = Resources.Load<Transform>("Prefabs/pfSoil");

        for (int i = -50; i < 50; i++)
        {
            for (int j = -50; j < 0; j++)
            {
                Instantiate(soilTransform, new Vector3(i, j, 0), Quaternion.identity);
            }
        }
    }
}
