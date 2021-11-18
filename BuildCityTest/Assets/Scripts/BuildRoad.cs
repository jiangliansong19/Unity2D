using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildRoad : MonoBehaviour
{
    private RoadTypeSO roadTypeSO;
    private Vector3 startPosition;
    private Vector3 endPosition;

    public void StartingBuildRoad(Vector3 position)
    {
        startPosition = new Vector3(Mathf.Round(position.x), Mathf.Round(position.y), 0);
        roadTypeSO = BuildingManager.Instance.GetActiveBuildingTypeSO() as RoadTypeSO;

        if (roadTypeSO.partType == RoadPartType.corner ||
            roadTypeSO.partType == RoadPartType.cross)
        {
            BoxCollider2D collider = roadTypeSO.prefab.GetComponent<BoxCollider2D>();
            Collider2D[] colliders = Physics2D.OverlapBoxAll(position + (Vector3)collider.offset, collider.size, 0);
            if (colliders == null || colliders.Length == 0)
            {
                AffordMoneyToBuildRoads(startPosition);
            }
        }
    }

    public void MovingMouse(Vector3 position)
    {
        endPosition = new Vector3(Mathf.Round(position.x), Mathf.Round(position.y), 0);
        BuildRoads();
    }

    public void EndedBuildRoad(Vector3 position)
    {
        endPosition = new Vector3(Mathf.Round(position.x), Mathf.Round(position.y), 0);
    }

    public void BuildRoads()
    {
        float distanceX = Mathf.Abs(startPosition.x - endPosition.x);
        float distanceY = Mathf.Abs(startPosition.y - endPosition.y);

        if (distanceX < 1 && distanceY < 1)
        {
            BuildRoadItemAtPosition(startPosition);
        }
        else
        {
            if (distanceX > distanceY)
            {
                Debug.Log("build road horizontal");
                BuildHorizontalRoads();
            }
            else
            {
                Debug.Log("build road vertical");
                BuildVerticalRoads();
            }
        }
    }

    private void BuildHorizontalRoads()
    {

        for (float i = Mathf.Min(startPosition.x, endPosition.x); i < Mathf.Max(startPosition.x, endPosition.x); i++)
        {
            BuildRoadItemAtPosition(new Vector3(i, startPosition.y, 0));
        }
    }

    private void BuildVerticalRoads()
    {
        for (float i = Mathf.Min(startPosition.y, endPosition.y); i<Mathf.Max(startPosition.y, endPosition.y); i++)
        {
            BuildRoadItemAtPosition(new Vector3(startPosition.x, i, 0));
        }
    }

    private void BuildRoadItemAtPosition(Vector3 position)
    {
        RaycastHit2D castObjc = Physics2D.Raycast(position, Vector2.zero);
        if (castObjc.collider == null)
        {
            AffordMoneyToBuildRoads(position);
        }
    }

    private void AffordMoneyToBuildRoads(Vector3 position)
    {
        Vector3 roundPosition = new Vector3(Mathf.Round(position.x), Mathf.Round(position.y), 0);
        Instantiate(roadTypeSO.prefab.gameObject, roundPosition, Quaternion.identity);

        GameDataManager.Instance.totalMoney -= roadTypeSO.data.constructCost;
        GameDataManager.Instance.IncomePerDay += roadTypeSO.data.originIncomePerDay;
    }

    private bool HasBuildingCollidersOverlap(GameObject prefab, Vector3 origin, Vector3 end)
    {
        BoxCollider2D collider = prefab.GetComponent<BoxCollider2D>();
        Vector3 position = UtilsClass.GetCurrentWorldPoint();
        Collider2D[] colliders = Physics2D.OverlapBoxAll(position + (Vector3)collider.offset, collider.size, 0);
        if (colliders.Length == 0)
        {
            return false;
        }
        foreach (Collider2D co in colliders)
        {
            if (!co.gameObject.tag.StartsWith("Building"))
            {
                return false;
            }
        }
        return true;
    }


}
