using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class BuildMenu : MonoBehaviour
{
    private BuildingTypeListSO buildingTypeList;
    private ArchitectureListSO architectureList;
    private ArchitectureSO currentArchitecture;

    private bool isBuilding = false;

    private Rect buildPanelRect1;
    private Rect buildPanelRect2;

    private Vector3 buildRoadOrigin;
    private Vector3 buildRoadEnd;

    private void Awake()
    {
        string resourceName = typeof(ScriptableObject).Name + "/" + typeof(ArchitectureListSO).Name;
        architectureList = Resources.Load<ArchitectureListSO>(resourceName);
    }

    private void OnGUI()
    {
        float firstLength = 50.0f;
        float secondLength = 40.0f;

        float width = architectureList.list.Count * (firstLength + 4) + 4;
        buildPanelRect1 = new Rect(Screen.width / 2 - width / 2, Screen.height - firstLength - 10, width, firstLength + 10);
        GUILayout.BeginArea(buildPanelRect1);
        GUILayout.BeginHorizontal("box");

        foreach (ArchitectureSO aso in architectureList.list)
        {
            Sprite s = aso.prefab.GetComponent<SpriteRenderer>().sprite;
            if (GUILayout.Button(new GUIContent(UtilsClass.GetTextureFromSprite(s)), GUILayout.Width(firstLength), GUILayout.Height(firstLength)))
            {
                isBuilding = !isBuilding;
                currentArchitecture = aso;
                buildingTypeList = aso.list;
            }
        }

        GUILayout.EndHorizontal();
        GUILayout.EndArea();


        if (currentArchitecture && isBuilding)
        {

            float width1 = buildingTypeList.list.Count * (secondLength + 4) + 4;
            buildPanelRect2 = new Rect(Screen.width / 2 - width1 / 2, Screen.height - secondLength - 10 - 60, width1, secondLength + 10);
            GUILayout.BeginArea(buildPanelRect2);
            GUILayout.BeginHorizontal("box");

            foreach (BuildingTypeSO so in buildingTypeList.list)
            {
                Sprite s1 = so.prefab.GetComponent<SpriteRenderer>().sprite;
                if (GUILayout.Button(new GUIContent(UtilsClass.GetTextureFromSprite(s1)), GUILayout.Width(secondLength), GUILayout.Height(secondLength)))
                {
                    BuildingManager.Instance.buildState = BuildState.build;
                   

                    BuildingManager.Instance.activeBuildingTypeSO = so;
                }
            }

            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) &&
            BuildingManager.Instance.buildState == BuildState.build &&
            BuildingManager.Instance.activeBuildingTypeSO != null &&
            !isTouchDownInPanel())
        {
            if (BuildingManager.Instance.activeBuildingTypeSO.type == BuildingType.Road)
            {
                if ((buildRoadOrigin == Vector3.zero && buildRoadEnd == Vector3.zero) ||
                    (buildRoadOrigin != Vector3.zero && buildRoadEnd != Vector3.zero))
                {
                    buildRoadOrigin = UtilsClass.GetCurrentWorldPoint();
                    buildRoadEnd = Vector3.zero;
                    print("touchFirst " + buildRoadOrigin.ToString());
                }
                else if (buildRoadOrigin != Vector3.zero && buildRoadEnd == Vector3.zero)
                {
                    buildRoadEnd = UtilsClass.GetCurrentWorldPoint();
                    BuildingManager.Instance.BuildRoad(buildRoadOrigin, buildRoadEnd);
                    print("touchEnd " + buildRoadEnd.ToString());
                }
            }
            else
            {
                BuildingManager.Instance.BuildBuilding();

            }
        }


        if (Input.GetMouseButtonDown(1))
        {
            BuildingManager.Instance.buildState = BuildState.scan;
            BuildingManager.Instance.activeBuildingTypeSO = null;

            buildRoadOrigin = Vector3.zero;
            buildRoadEnd = Vector3.zero;
        }




    }

    private bool isTouchDownInPanel()
    {
        return UtilsClass.isTouchPointInScreenRect(buildPanelRect1) ||
               UtilsClass.isTouchPointInScreenRect(buildPanelRect2);
    }
}
