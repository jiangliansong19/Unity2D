using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMouse : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        BuildingManager.Instance.OnActiveBuildingTypeChangedHandler += BuildingManager_OnActiveBuildingTypeChangedHandler;
    }

    private void Update()
    {
        transform.position = UtilsClass.GetCurrentWorldPoint();
    }

    private void BuildingManager_OnActiveBuildingTypeChangedHandler(object sender, BuildingManager.OnActiveBuildingTypeChangedHandlerArgs e)
    {
        if (e != null)
        {
            spriteRenderer.sprite = e.Args_TypeSO.prefab.GetComponent<SpriteRenderer>().sprite;
            spriteRenderer.color = new Color(1, 1, 1, 0.5f);
        }
        else
        {
            spriteRenderer.sprite = null;
            spriteRenderer.color = new Color(1, 1, 1, 1.0f);
        }
    }
}
