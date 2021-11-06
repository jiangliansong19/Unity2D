using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //规则
    //1.被触碰的物体，主动触碰的物体，都会调用以下方法。
    //2.参数collision都是相同的，都是标记为isTrigger的那个物体。
    //3.isTrigger相当于在物体上开启了一个触碰检测器。被检测的对象必须是刚体。
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("right-OnTriggerEnter " + collision.gameObject.tag);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        print("right-OnTriggerExit2D " + collision.gameObject.tag);
    }
}
