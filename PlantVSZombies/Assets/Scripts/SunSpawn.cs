using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunSpawn : MonoBehaviour
{

    //在inpector里显示的公开属性
    public GameObject prefab;


    // Start is called before the first frame update
    void Start()
    {
        //重复执行某方法(方法名，第一次执行的时间，每多少秒执行一次)
        InvokeRepeating("Spawn", 5, 10);
    }

    // Update is called once per frame
    void Spawn()
    {
        Instantiate(prefab, transform.position, Quaternion.identity);
    }
}
