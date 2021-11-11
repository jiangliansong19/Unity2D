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
            if (CanSetUpBuilding(activeBuildingType, UtilClass.GetMouseWorldPoint()))
            {
                Instantiate(activeBuildingType.prefab, UtilClass.GetMouseWorldPoint(), Quaternion.identity);
            }
            
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

    public bool CanSetUpBuilding(BuildingTypeSO typeSO, Vector3 position)
    {
        //建筑重叠，不允许建造
        BoxCollider2D boxCollider2D = typeSO.prefab.GetComponent<BoxCollider2D>();
        Collider2D[] boxColliders = Physics2D.OverlapBoxAll(position + (Vector3)boxCollider2D.offset, boxCollider2D.size, 0);
        if (boxColliders != null && boxColliders.Length > 0)
        {
            return false;
        }

        //在建筑最小间距内，如果有一个相同的建筑，则不允许建造
        Collider2D[] circleColliders = Physics2D.OverlapCircleAll(position, typeSO.minConstructionRadius);
        foreach (Collider2D item in circleColliders)
        {
            BuildingTypeHolder holder = item.GetComponent<BuildingTypeHolder>();
            if (holder != null && holder.type == typeSO)
            {
                return false;
            }
        }

        //建筑间距25以内没有其它建筑，则不允许建造
        float maxConstructionRadius = 25;
        circleColliders = Physics2D.OverlapCircleAll(position.normalized, maxConstructionRadius);
        foreach (Collider2D item in circleColliders)
        {
            BuildingTypeHolder holder = item.GetComponent<BuildingTypeHolder>();
            if (holder != null)
            {
                return true;
            }
        }

        return false;
    }
}
