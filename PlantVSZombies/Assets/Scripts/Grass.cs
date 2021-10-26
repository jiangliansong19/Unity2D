using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    //点击后松开
    private void OnMouseUpAsButton()
    {
        //点击menu中的flower之后，静态变量BuildMenu.cur有值
        if (BuildMenu.cur != null)
        {
            Instantiate(BuildMenu.cur.gameObject, transform.position, Quaternion.identity);
            SunCollection.score -= BuildMenu.cur.price;
            BuildMenu.cur = null;
        }
    }

    //只调用一次，在鼠标进入的瞬间
    private void OnMouseEnter()
    {
        print("OnMouseEnter");
    }

    //调用多次，在鼠标在物体上时，每一帧调用一次
    private void OnMouseOver()
    {
        print("OnMouseOver");
    }

    //鼠标左键按住后拽动。每一帧调用一次。松开左键结束，结束时鼠标可能已经在物体外。
    private void OnMouseDrag()
    {
        print("OnMouseDrag");
    }

    //只调用一次，在鼠标离开瞬间。
    private void OnMouseExit()
    {
        print("OnMouseExit");
    }
}
