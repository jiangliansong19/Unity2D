using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firing : MonoBehaviour
{
    public GameObject bulletPrefab;

    public float interval = 0.5f;

    private void Start()
    {
        //重复调用(某方法，第一次调用的时间，多少秒调用一次)
        InvokeRepeating("Shoot", 0, interval);
    }

    //检测前方物体
    bool isZombieInFront()
    {
        Vector2 origin = new Vector2(9.5f, this.transform.position.y);

        //被投射的射线检测到的物体。从最右侧，往左侧发出射线。第一个被检测到的就是Zombie
        RaycastHit2D[] hits = Physics2D.RaycastAll(origin, Vector2.left);

        foreach (RaycastHit2D hit in hits)
        {

            //如果碰撞体的gameobject.tag是Zombie
            if (hit.collider != null && hit.collider.gameObject.tag == "Zombie")
            {
                return true;
            }
        }

        return false;
    }

    void Shoot()
    {
        //前方有Zombie，实例化子弹。
        if (isZombieInFront())
        {
            //后去与脚本绑定的object的动画Animator，修改Animator的状态机参数。
            this.GetComponent<Animator>().SetTrigger("isFiring");

            //实例化子弹，而子弹有绑定了一个Velocity，开始向右侧运动
            Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        }
    }

}
