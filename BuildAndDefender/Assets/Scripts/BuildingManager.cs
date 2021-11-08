using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { private set; get; }

    private Camera mainCamera;
    private BuildingTypeListSO buildingTypeList;
    private BuildingTypeSO activeBuildingType;

    public event EventHandler<OnActiveBuldingTypeChangedEventArgs> OnActiveBuildingTypeChanged;

    public class OnActiveBuldingTypeChangedEventArgs : EventArgs
    {
        public BuildingTypeSO activeBuildingType;
    }

    private void Awake()
    {
        Instance = this;
        buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
    }

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && 
            !EventSystem.current.IsPointerOverGameObject() && 
            activeBuildingType != null)
        {
            Instantiate(activeBuildingType.prefab, UtilClass.GetMouseWorldPoint(), Quaternion.identity);
        }
    }

    public void SetActiveBuildingType(BuildingTypeSO type)
    {
        activeBuildingType = type;
        OnActiveBuildingTypeChanged?.Invoke(this, new OnActiveBuldingTypeChangedEventArgs
        {
            activeBuildingType = activeBuildingType
        });
    }

    public BuildingTypeSO GetActiveBuildingType()
    {
        return activeBuildingType;
    }
}
