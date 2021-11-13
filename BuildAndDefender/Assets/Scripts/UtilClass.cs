using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilClass
{
    public static Vector3 GetMouseWorldPoint()
    {
        Vector3 v3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        v3.z = 0;
        return v3;
    }

}
