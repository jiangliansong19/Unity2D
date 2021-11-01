using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class BuildMenu : MonoBehaviour
{
    public GameObject buildingsPanel;
    public Button smallHouseButton;

    private GameObject currentBuilding;

    private void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 100, 50), "Buildings"))
        {
            buildingsPanel.SetActive(true);
            smallHouseButton.onClick.AddListener(BuildSmallHouse);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        buildingsPanel.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (currentBuilding != null)
        //{

        //    Vector2 mouseDirection = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));


        //    currentBuilding.transform.Translate(mouseDirection);
        //}

        if (Input.GetMouseButtonDown(0))
        {
            print("click on postion: " + Input.mousePosition.ToString());
        }
    }


    private void BuildSmallHouse()
    {
        GameObject obj = Resources.Load<GameObject>("Prefabs/Shop");
        currentBuilding = Instantiate(obj, new Vector3(100, 100, 0), Quaternion.identity);
    }
}
