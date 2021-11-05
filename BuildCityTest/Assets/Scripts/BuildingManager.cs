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
        Instantiate(this.activeBuildingTypeSO.prefab, UtilsClass.GetCurrentWorldPoint(), Quaternion.identity);
    }

    /// <summary>
    /// 修路，起点到终点。有其他障碍物处无法修路
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="end"></param>
    public void BuildRoad(Vector3 origin, Vector3 end)
    {
        BuildRoad build = GameObject.FindObjectOfType<BuildRoad>();
        build.BuildRoads(origin, end);

    }

    public void DestructBuilding(GameObject obj, Vector3 position)
    {

    }

    public void UpdateBuilding(GameObject obj, Vector3 position)
    {

    }

}
