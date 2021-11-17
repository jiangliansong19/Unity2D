using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildRoad : MonoBehaviour
{
    [SerializeField] RoadTypeSO roadTypeSO;

    public void BuildRoads(Vector3 start, Vector3 end)
    {

        float distanceX = Mathf.Abs(start.x - end.x);
        float distanceY = Mathf.Abs(start.y - end.y);

        if (distanceX > distanceY)
        {
            Debug.Log("build road horizontal");
            for (float i = Mathf.Min(start.x, end.x); i < Mathf.Max(start.x, end.x); i++)
            {
                BuildRoadItemAtPosition(RoadPartType.horizontal, new Vector3(i, start.y, 0));
                Debug.Log("build a road at " + new Vector3(i, start.y, 0).ToString());
            }
        }
        else
        {
            Debug.Log("build road vertical");
            for (float i = Mathf.Min(start.y, end.y); i < Mathf.Max(start.y, end.y); i++)
            {
                BuildRoadItemAtPosition(RoadPartType.vertical, new Vector3(start.x, i, 0));
            }
        }
    }

    private void BuildRoadItemAtPosition(RoadPartType type, Vector3 position)
    {
        if (CanBuildRoadAtPosition(type, position))
        {
            GameObject prefab = roadTypeSO.GetPartInfoByType(type).prefab.gameObject;
            Vector3 roundPosition = new Vector3(Mathf.Round(position.x), Mathf.Round(position.y), 0);
            Instantiate(prefab, roundPosition, Quaternion.identity);

            UpdateIncomePerDay();
        }
    }

    private void UpdateIncomePerDay()
    {
        GameDataManager.Instance.totalMoney -= roadTypeSO.data.constructCost;
        GameDataManager.Instance.IncomePerDay += roadTypeSO.data.originIncomePerDay;
    }


    private bool CanBuildRoadAtPosition(RoadPartType type, Vector3 position)
    {
        RaycastHit2D castObjc = Physics2D.Raycast(position, Vector3.zero);
        if (castObjc.collider == null)
        {
            return true;
        }
        else
        {
            if (castObjc.collider.name != roadTypeSO.GetPartInfoByType(type).partName + "(Clone)")
            {
                return true;
            }
        }

        return false;
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
