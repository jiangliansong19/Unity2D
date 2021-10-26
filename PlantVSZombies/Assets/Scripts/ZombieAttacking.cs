using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttacking : MonoBehaviour
{

    float last = 0.0f;

    //在碰撞后，会每一帧执行一次
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "plant")
        {
            this.GetComponent<Animator>().SetTrigger("isAttacking");

            if (Time.time - last >= 1)
            {
                collision.gameObject.GetComponent<Health>().doDamage(2);
                last = Time.time;
            }
        }

    }

    //在碰撞瞬间，只会被执行一次
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }


    //在碰撞离开瞬间，只会被执行一次
    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }
}
