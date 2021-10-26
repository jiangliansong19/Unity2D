using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    //bullet,Zombie都是刚体且都有碰撞器，会发生碰撞效果：Zombie会被bullet击退(飞)
    //bullet的碰撞器circleCollision里设置isTrigger=YES后，碰撞后，bullet直接穿过Zombie。

    //检测碰撞
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Zombie")
        {
            collision.GetComponent<Health>().doDamage(1);

            //如果不销毁bullet，会显示bullet穿过Zombie的现象
            Destroy(gameObject);
        }
    }

    //不执行
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("OnCollisionEnter2D");
    }

    //如果既要检测到物体的接触又不想让碰撞检测影响物体移动
    //或要检测一个物件是否经过空间中的某个区域
    //这时就可以用到触发器
}
