using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMouse : MonoBehaviour
{
    private Sprite private_mousePrite;

    private void Awake()
    {
        private_mousePrite = GetComponent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
    private void Update()
    {
        this.transform.position = UtilsClass.GetCurrentWorldPoint();

        if (BuildingManager.Instance.activeBuildingTypeSO != null &&
            BuildingManager.Instance.buildState == BuildState.build)
        {
            Sprite s = BuildingManager.Instance.activeBuildingTypeSO.prefab.GetComponent<SpriteRenderer>().sprite;
            GetComponent<SpriteRenderer>().sprite = s;
            GetComponent<SpriteRenderer>().color = new Color(1,1,1,0.5f);
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = private_mousePrite;
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}
