using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class BuildMenu : MonoBehaviour
{
    private BuildingTypeListSO buildingTypeList;
    private ArchitectureListSO architectureList;

    private ArchitectureSO currentArchitecture;
    private BuildingTypeSO currentBuilding;


    private float itemLength = 45.0f;
    private bool isBuilding = false;

    private bool isAbleToBuild = false;

    private void Awake()
    {
        architectureList = Resources.Load<ArchitectureListSO>(typeof(ScriptableObject).Name + "/" + typeof(ArchitectureListSO).Name);
    }

    private void OnGUI()
    {

        float firstLength = 50.0f;
        float secondLength = 40.0f;

        float width = architectureList.list.Count * (firstLength + 4) + 4;
        GUILayout.BeginArea(new Rect(Screen.width / 2 - width/2, Screen.height - firstLength - 10, width, firstLength + 10));
        GUILayout.BeginHorizontal("box");

        foreach (ArchitectureSO aso in architectureList.list)
        {
            Sprite s = aso.prefab.GetComponent<SpriteRenderer>().sprite;
            if (GUILayout.Button(new GUIContent(GetTextureFromSprite(s)), GUILayout.Width(firstLength), GUILayout.Height(firstLength)))
            {
                isBuilding = !isBuilding;
                currentArchitecture = aso;
                buildingTypeList = aso.list;
            }
        }

        GUILayout.EndHorizontal();
        GUILayout.EndArea();


        if (currentArchitecture && isBuilding)
        {

            float width1 = buildingTypeList.list.Count * (secondLength + 4) + 4;
            GUILayout.BeginArea(new Rect(Screen.width / 2 - width1 / 2, Screen.height - secondLength - 10 - 60, width1, secondLength + 10));
            GUILayout.BeginHorizontal("box");

            foreach (BuildingTypeSO so in buildingTypeList.list)
            {
                Sprite s1 = so.prefab.GetComponent<SpriteRenderer>().sprite;
                if (GUILayout.Button(new GUIContent(GetTextureFromSprite(s1)), GUILayout.Width(secondLength), GUILayout.Height(secondLength)))
                {
                    isBuilding = true;
                    currentBuilding = so;
                }
            }

            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isBuilding)
        {
            Instantiate(currentBuilding.prefab, GetCurrentWorldPoint(), Quaternion.identity);

            currentBuilding = null;
            GameMouse.Instance.MousePriteReset();
            
            return;
        }


        //移动中
        if (currentBuilding != null && isBuilding)
        {
            GameMouse.Instance.mouseSprite = currentBuilding.prefab.GetComponent<SpriteRenderer>().sprite;
        }
    }

    private Vector3 GetCurrentWorldPoint()
    {
        Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        point.z = 0;
        return point;
    }

    private Texture GetTextureFromSprite(Sprite sprite)
    {
        Texture2D croppedTexture = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
        Color[] pixels = sprite.texture.GetPixels((int)sprite.textureRect.x,
                                                (int)sprite.textureRect.y,
                                                (int)sprite.textureRect.width,
                                                (int)sprite.textureRect.height);
        croppedTexture.SetPixels(0, 0, (int)sprite.textureRect.width, (int)sprite.textureRect.height, pixels, 0);
        croppedTexture.Apply();
        return croppedTexture;
    }
}
