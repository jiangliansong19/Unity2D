using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunCollection : MonoBehaviour
{
    //公开的静态变量
    public static int score = 500;
    // Start is called before the first frame update


    //执行一次，按下鼠标的瞬间。
    private void OnMouseDown()
    {
        score += 20;

        Destroy(gameObject);
    }

}
