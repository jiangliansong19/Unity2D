using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTest : MonoBehaviour
{
    public GameObject person;
    public GameObject laserPrefab;

    bool isClickedToDestory = false;

    bool isShootToDestory = false;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(person, new Vector2(3, 0), Quaternion.identity);
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 150, 50), "clickToDestory"))
        {
            isClickedToDestory = true;
            isShootToDestory = false;
        }

        if (GUI.Button(new Rect(160, 0 ,150, 50), "shootToDestory"))
        {
            isClickedToDestory = false;
            isShootToDestory = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

        //点击删除物体
        if (Input.GetMouseButtonDown(0))
        {
            if (isClickedToDestory)
            {
                ClickOnObjectToDestroy();
            }

            if (isShootToDestory)
            {
                ShootOnObjectToDestory();
            }
        }
    }

    private void ClickOnObjectToDestroy()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        print("has click on left mouse: " + worldPoint.ToString());

        //RaycastAll(射线起点，射线方向）。
        RaycastHit2D[] hits = Physics2D.RaycastAll(worldPoint, Vector2.zero);

        foreach (RaycastHit2D item in hits)
        {
            if (item.collider != null)
            {
                print("item's tag = " + item.collider.gameObject.tag);
                DestroyImmediate(item.collider.gameObject);

            }
        }
    }

    private void ShootOnObjectToDestory()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Instantiate(this.laserPrefab, new Vector3(worldPoint.x, worldPoint.y, 0), Quaternion.AngleAxis(90, Vector3.forward));
    }
}
