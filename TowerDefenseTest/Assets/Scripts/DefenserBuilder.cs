using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 专门用于建造，升级，销毁防御塔的类
/// </summary>
public class DefenserBuilder : MonoBehaviour
{
    public static DefenserBuilder Instance { private set; get; }
    public Transform defenserPlaceholder;

    private DefenserListSO defenserList;



    private Transform clickedGameObject;
    private bool showUpdateAndDeleteButtons;
    private bool showDefenserPanel;

    /// <summary>
    /// 点击屏幕，根据不同状态展示不同的按键。建造，升级，销毁。
    /// </summary>
    private void OnGUI()
    {
        if (showUpdateAndDeleteButtons && clickedGameObject)
        {
            Transform t = clickedGameObject;
            DefenserData data = clickedGameObject.gameObject.GetComponent<DefenserData>();

            Vector3 position = Camera.main.WorldToScreenPoint(clickedGameObject.position);

            if (data.level < 3)
            {
                if (GUI.Button(new Rect(position.x, position.y + 70, 70, 30), "Update"))
                {
                    UpdateDefenserLevel();
                }
            }

            if (GUI.Button(new Rect(position.x, position.y - 30, 70, 30), "Delete"))
            {
                DestroyDefenser();
            }
        }

        if (showDefenserPanel && clickedGameObject)
        {
            Vector3 position = Camera.main.WorldToScreenPoint(clickedGameObject.position);
            float width = defenserList.list.Count * 30;
            GUILayout.BeginArea(new Rect(position.x - width/2, position.y - 70, width + 10, 40));
            GUILayout.BeginHorizontal("box");
            foreach (DefenserSO so in defenserList.list)
            {
                if (GUILayout.Button(so.image, GUILayout.Width(30), GUILayout.Height(30)))
                {
                    BuildDefenser(so, 0);
                }
            }
            GUILayout.EndHorizontal();
            GUILayout.EndArea();

        }
    }

    private void Awake()
    {
        Instance = this;
    }

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
            if (hits.Length == 1) 
            {
                clickedGameObject = hits[0].collider.gameObject.transform;
                if (clickedGameObject.tag == "placeholder")
                {
                    showDefenserPanel = true;
                }
                else if (clickedGameObject.tag.StartsWith("defenser"))
                {
                    DisplayUpdateAndDestroyButton(gameObject.transform);
                }
            }
        }

    }

    /// <summary>
    /// 展示升级，销毁按键
    /// </summary>
    /// <param name="t"></param>
    private void DisplayUpdateAndDestroyButton(Transform t)
    {
        showUpdateAndDeleteButtons = true;
    }

    /// <summary>
    /// 建造防御塔
    /// </summary>
    /// <param name="d"></param>
    /// <param name="old"></param>
    private void BuildDefenser(DefenserSO d, int level)
    {
        Transform prefab = d.prefabs[0];
        Transform newObj = Instantiate(prefab, clickedGameObject.position, Quaternion.identity);
        DestroyImmediate(clickedGameObject.gameObject);

        DefenserManager.sharedManager.defenserInfos.Add(newObj, d);


        
        newObj.gameObject.GetComponent<Animator>().SetTrigger("fireShot");

        clickedGameObject = null;
        showDefenserPanel = false;
    }


    /// <summary>
    /// 升级防御建筑
    /// </summary>
    /// <param name="d"></param>
    private void UpdateDefenserLevel()
    {
        DefenserSO d = DefenserManager.sharedManager.defenserInfos[clickedGameObject];
        DefenserData data = clickedGameObject.GetComponent<DefenserData>();
        Transform prefab = d.prefabs[data.level + 1];
        Transform newObj = Instantiate(prefab, clickedGameObject.position, Quaternion.identity);
        newObj.gameObject.GetComponent<DefenserData>().level = data.level + 1;

        DefenserManager.sharedManager.defenserInfos.Remove(clickedGameObject);
        DefenserManager.sharedManager.defenserInfos.Add(newObj, d);

        DestroyImmediate(clickedGameObject.gameObject);
        showUpdateAndDeleteButtons = false;


    }

    /// <summary>
    /// 摧毁防御建筑
    /// </summary>
    /// <param name="d"></param>
    private void DestroyDefenser()
    {
        Instantiate(gameObject, clickedGameObject.position, Quaternion.identity);

        DefenserManager.sharedManager.defenserInfos.Remove(clickedGameObject);

        DestroyImmediate(clickedGameObject.gameObject);

        showUpdateAndDeleteButtons = false;
    }

    private Vector3 GetWorldPointFromScreen()
    {
        Vector3 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        v.z = 0;
        return v;
    }

}
