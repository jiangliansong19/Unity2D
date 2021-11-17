using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum BuildState
{
    scan,
    build,
    destruct,
    update,
}


/// <summary>
/// 建筑
/// </summary>
public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { private set; get; }

    [HideInInspector] public BuildState buildState = BuildState.scan;

    private BuildingTypeSO activeBuildingTypeSO;

    [HideInInspector] public Dictionary<BuildingTypeSO, GameObject> buildingInfoDict;

    private GameObject BuildingInfoDialog;

    private BuildMenu buildMenu;
    private BuildRoad buildRoad;

    private List<Vector3> buildRoadVectors;

    private Vector3? buildRoadOrigin = null;

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
        buildMenu = FindObjectOfType<BuildMenu>();
        buildRoad = FindObjectOfType<BuildRoad>();
        buildRoadVectors = new List<Vector3>();
    }

    // Update is called once per frame
    void Update()
    { 
        if (buildState == BuildState.scan && Input.GetMouseButtonDown(0))
        {
            ScanBuilding();
        }

        if (buildState == BuildState.build)
        {
            if (Input.GetMouseButtonDown(0) &&
                activeBuildingTypeSO != null &&
                !buildMenu.isTouchDownInPanel() &&
                activeBuildingTypeSO.type != BuildingType.Road)
            {
                BuildBuilding();
            }
            else if (activeBuildingTypeSO.type == BuildingType.Road)
            {
                if (Input.GetMouseButtonDown(0) && buildRoadOrigin == null)
                {
                    buildRoadOrigin = UtilsClass.GetCurrentWorldPoint();
                    Debug.Log("Build road starting " + UtilsClass.GetCurrentWorldPoint().ToString());
                }

                if (Input.GetMouseButton(0) && buildRoadOrigin != null)
                {
                    Debug.Log("Build road moving " + UtilsClass.GetCurrentWorldPoint().ToString());

                    buildRoad.BuildRoads((Vector3)buildRoadOrigin, UtilsClass.GetCurrentWorldPoint());
                }

                if (Input.GetMouseButtonUp(0) && buildRoadOrigin != null)
                {
                    Debug.Log("Build road ending " + UtilsClass.GetCurrentWorldPoint().ToString());

                    buildRoadOrigin = null;
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            buildState = BuildState.scan;
            activeBuildingTypeSO = null;
        }

    }


    public void SetActiveBuildingTypeSO(BuildingTypeSO typeSO)
    {
        activeBuildingTypeSO = typeSO;
        OnActiveBuildingTypeChangedHandler?.Invoke(this, new OnActiveBuildingTypeChangedHandlerArgs
        {
            Args_TypeSO = activeBuildingTypeSO
        });

        if (typeSO == null)
        {
            buildState = BuildState.scan;
        }
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
            ShowBuildingInfoDialog(obj, position);
        }
    }

    public void BuildBuilding()
    {
        BoxCollider2D collider = activeBuildingTypeSO.prefab.GetComponent<BoxCollider2D>();
        Vector3 position = UtilsClass.GetCurrentWorldPoint();
        Collider2D[] colliders = Physics2D.OverlapBoxAll(position + (Vector3)collider.offset, collider.size, 0);
        bool isAreaClear = colliders.Length == 0;
        if (!isAreaClear)
        {
            return;
        }

        Transform newObj = Instantiate(this.activeBuildingTypeSO.prefab, UtilsClass.GetCurrentWorldPoint(), Quaternion.identity);

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


}
