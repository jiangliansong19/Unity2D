using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenserBuilder : MonoBehaviour
{
    private DefenserListSO defenserList;

    public Transform placeholder;


    // Start is called before the first frame update
    void Start()
    {
        defenserList = Resources.Load<DefenserListSO>(typeof(ScriptableObject).Name + "/" + typeof(DefenserListSO).Name);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 position = GetWorldPointFromScreen();
            RaycastHit2D[] hits = Physics2D.RaycastAll(new Vector2(position.x, position.y), Vector2.zero);
            if (hits.Length == 1 && hits[0].collider.gameObject.tag == "placeholder") 
            {
                DisplayDefensersPanelAroundTransform(hits[0].collider.gameObject.transform);
            }
        }

    }

    /// <summary>
    /// 展示建造面板
    /// </summary>
    /// <param name="position"></param>
    private void DisplayDefensersPanelAroundTransform(Transform t)
    {
        DefenserSO defenser = defenserList.list[0];
        Instantiate(defenser.levels[defenser.currentLevel].prefab, t.position, Quaternion.identity);
        DestroyImmediate(t.gameObject);
    }

    /// <summary>
    /// 升级防御建筑
    /// </summary>
    /// <param name="d"></param>
    private void UpdateDefenserLevel(DefenserSO d)
    {

    }

    /// <summary>
    /// 摧毁防御建筑
    /// </summary>
    /// <param name="d"></param>
    private void DestroyDefenser(DefenserSO d)
    {

    }

    private Vector3 GetWorldPointFromScreen()
    {
        Vector3 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        v.z = 0;
        return v;
    }
}
