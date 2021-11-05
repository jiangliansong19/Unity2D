using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilsClass
{
    public static Vector3 GetCurrentWorldPoint()
    {
        Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        point.z = 0;
        return point;
    }

    public static Texture GetTextureFromSprite(Sprite sprite)
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

    public static bool isTouchPointInScreenRect(Rect rect)
    {
        Vector3 point = Input.mousePosition;
        return point.x > rect.xMin && point.x < rect.xMax && point.y > rect.yMin && point.y < rect.yMax;
    }

    public static GameObject GetObjectByRay(Vector3 atPosition)
    {
        RaycastHit2D[] objs = Physics2D.RaycastAll(new Vector2(atPosition.x, atPosition.y), Vector2.zero);
        if (objs != null && objs.Length > 0)
        {
            return objs[0].collider.gameObject;
        }
        return null;
    }

    //public static Vector2 GetUGUIFromWorlPoint(Vector3 v)
    //{
    //    Vector2 screenPoint = Camera.main.WorldToScreenPoint(v);
    //    Vector2 screenSize = new Vector2(Screen.width, Screen.height);
    //    screenPoint -= screenSize / 2;
    //    Vector2 anchorPosition = screenPoint / screenSize * 
    //}
}