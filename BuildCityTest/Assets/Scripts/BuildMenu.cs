using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class BuildMenu : MonoBehaviour
{
    public Buildings[] buildInfos;

    private Buildings currentBuilding;

    private bool isBuilding = false;

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(Screen.width/2 - 350, Screen.height - 100, 680, 100));
        GUILayout.BeginHorizontal("box");



        foreach (Buildings info in buildInfos)
        {
            Sprite s = info.transform.GetComponent<SpriteRenderer>().sprite;
            if (GUILayout.Button(new GUIContent(GetTextureFromSprite(s)), GUILayout.Width(90), GUILayout.Height(90)))
            {
                isBuilding = true;
                currentBuilding = info;
            }
        }

        GUILayout.EndHorizontal();
        GUILayout.EndArea();
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
            Instantiate(currentBuilding.transform, getCurrentWorldPoint(), Quaternion.identity);

            currentBuilding = null;
            isBuilding = false;
            GameMouse.Instance.MousePriteReset();
            
            return;
        }


        //移动中
        if (currentBuilding != null && isBuilding)
        {
            GameMouse.Instance.mouseSprite = currentBuilding.transform.GetComponent<SpriteRenderer>().sprite;
        }
    }

    private Vector3 getCurrentWorldPoint()
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
