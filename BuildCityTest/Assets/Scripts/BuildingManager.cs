using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

    [HideInInspector] public BuildingTypeSO activeBuildingTypeSO;

    [HideInInspector] public Dictionary<BuildingTypeSO, GameObject> buildingInfoDict;


    private void Awake()
    {
        Instance = this;
        buildingInfoDict = new Dictionary<BuildingTypeSO, GameObject>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ScanBuilding(GameObject obj, Vector3 position)
    {

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

        Instantiate(this.activeBuildingTypeSO.prefab, UtilsClass.GetCurrentWorldPoint(), Quaternion.identity);
    }

    /// 修路，起点到终点
    public void BuildRoad(Vector3 origin, Vector3 end)
    {
        BuildRoad build = GameObject.FindObjectOfType<BuildRoad>();
        build.BuildRoads(origin, end);

    }

    public void DestructBuilding(GameObject obj, Vector3 position)
    {
        GameObject gameObj = UtilsClass.GetObjectByRay(position);
        Destroy(gameObj);
    }

    public void UpdateBuilding(GameObject obj, Vector3 position)
    {

    }

}
