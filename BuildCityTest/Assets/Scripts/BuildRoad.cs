using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildRoad : MonoBehaviour
{
    private GameObject prefab_cross;
    private GameObject prefab_corner;
    private GameObject prefab_horizontal;
    private GameObject prefab_vertical;

    public void BuildRoads(Vector3 origin, Vector3 end)
    {



        BuildingTypeSO so = BuildingManager.Instance.GetActiveBuildingTypeSO();
        prefab_horizontal = ((RoadTypeSO)so).partPrefabs[0].gameObject;
        prefab_vertical = ((RoadTypeSO)so).partPrefabs[1].gameObject;
        prefab_corner = ((RoadTypeSO)so).partPrefabs[2].gameObject;
        prefab_cross = ((RoadTypeSO)so).partPrefabs[3].gameObject;







        float distance_x = Mathf.Abs(origin.x - end.x);
        float distance_y = Mathf.Abs(origin.y - end.y);

        //水平建造
        if (distance_x > distance_y)
        {
            origin = new Vector3(Mathf.Round(origin.x), Mathf.Round(origin.y), 0);
            end = new Vector3(Mathf.Round(end.x), Mathf.Round(origin.y), 0);

            SpriteRenderer renderer = prefab_horizontal.transform.GetComponent<SpriteRenderer>();
            renderer.size = new Vector2(Mathf.Round(distance_x), 1);

            BoxCollider2D collider = prefab_horizontal.transform.GetComponent<BoxCollider2D>();
            collider.size = new Vector2(Mathf.Round(distance_x), 1);


            


            if (!HasBuildingCollidersOverlap(prefab_horizontal, origin, end))
            {
                Instantiate(prefab_horizontal, (origin + end) / 2, Quaternion.identity);

                GameDataManager.Instance.totalMoney -= so.data.constructCost * Mathf.Round(distance_x);
                GameDataManager.Instance.IncomePerDay += so.data.originIncomePerDay * Mathf.Round(distance_x);
            }
            




        }

        //垂直建造
        else
        {
            origin = new Vector3(Mathf.Round(origin.x), Mathf.Round(origin.y), 0);
            end = new Vector3(Mathf.Round(origin.x), Mathf.Round(end.y), 0);

            SpriteRenderer renderer = prefab_vertical.transform.GetComponent<SpriteRenderer>();
            renderer.size = new Vector2(1, Mathf.Round(distance_y));

            BoxCollider2D collider = prefab_vertical.transform.GetComponent<BoxCollider2D>();
            collider.size = new Vector2(1, Mathf.Round(distance_y));

            if (!HasBuildingCollidersOverlap(prefab_vertical, origin, end))
            {
                Instantiate(prefab_vertical, (origin + end) / 2, Quaternion.identity);

                GameDataManager.Instance.totalMoney -= so.data.constructCost * Mathf.Round(distance_x);
                GameDataManager.Instance.IncomePerDay += so.data.originIncomePerDay * Mathf.Round(distance_x);
            }
        }
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
