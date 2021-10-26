using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    int cur = 5;

    public void doDamage(int n)
    {
        cur -= n;
        if (cur < 0)
        {
            //销毁物体
            Destroy(gameObject);
        }
    }
}
