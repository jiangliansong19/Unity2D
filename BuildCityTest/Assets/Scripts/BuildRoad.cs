using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildRoad : MonoBehaviour
{
    private GameObject prefab_cross;
    private GameObject prefab_corner;
    private GameObject prefab_horizontal;
    private GameObject prefab_vertical;

    private void Start()
    {
        BuildingTypeSO so = BuildingManager.Instance.activeBuildingTypeSO;
        prefab_horizontal = ((RoadTypeSO)so).partPrefabs[0].gameObject;
        prefab_vertical = ((RoadTypeSO)so).partPrefabs[1].gameObject;
        prefab_corner = ((RoadTypeSO)so).partPrefabs[2].gameObject;
        prefab_cross = ((RoadTypeSO)so).partPrefabs[3].gameObject;
    
    }


    public void BuildRoads(Vector3 origin, Vector3 end)
    {
        origin = new Vector3(Mathf.Round(origin.x), Mathf.Round(origin.y), 0);
        end = new Vector3(Mathf.Round(end.x), Mathf.Round(end.y), 0);

        float distance_x = Mathf.Abs(origin.x - end.x);
        float distance_y = Mathf.Abs(origin.y - end.y);

        //水平建造
        if (distance_x > distance_y)
        {
            Vector3 position = origin;

            //从左往右 
            if (origin.x < end.x)
            {
                for (int i = (int)origin.x; i < end.x; i++)
                {
                    BuildRoadItem(prefab_horizontal, position);
                    position += new Vector3(1, 0, 0);
                }
            }

            //从右往左
            else
            {
                for (int i = (int)origin.x; i > end.x; i--)
                {
                    BuildRoadItem(prefab_horizontal, position);
                    position -= new Vector3(1, 0, 0);
                }
            }

        }

        //垂直建造
        else
        {
            Vector3 position = origin;
            if (origin.y < end.y)
            {
                for (int i = (int)origin.y; i < end.y; i++)
                {
                    BuildRoadItem(prefab_vertical, position);
                    position += new Vector3(0, 1, 0);
                }
            }
            else
            {
                for (int i = (int)origin.y; i > end.y; i--)
                {
                    BuildRoadItem(prefab_vertical, position);
                    position -= new Vector3(0, 1, 0);
                }
            }
        }
    }

    public void BuildRoadItem(GameObject prefab, Vector3 position)
    {
        GameObject existObject = UtilsClass.GetObjectByRay(position);
        if (existObject == null)
        {
            Instantiate(prefab, position, Quaternion.identity);
        }
        else if (existObject.tag.StartsWith("Road"))
        {
            Instantiate(prefab_cross, position, Quaternion.identity);
            DestroyImmediate(existObject);
        }
    }


}
