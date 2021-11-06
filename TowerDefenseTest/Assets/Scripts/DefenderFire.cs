using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 防御塔开火。
/// 防御塔添加触碰器，敌人从触碰开始，可以被攻击。
/// 防御塔始终优先攻击进入攻击范围内的第一个敌人。
/// </summary>
public class DefenderFire : MonoBehaviour
{
    public GameObject bullet1;
    public GameObject bullet2;
    public GameObject bullet3;

    private float maxTime = 2;
    private float time;

    private bool canFire;

    private List<GameObject> enermyList;


    // Start is called before the first frame update
    void Start()
    {
        maxTime = 1;
        enermyList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enermyList.Count > 0)
        {
            time -= Time.deltaTime;
            if (time < 0)
            {
                time = maxTime;
                GameObject obj = Instantiate(bullet1, transform.position, Quaternion.identity);
                obj.GetComponent<Velocity>().v2 = enermyList[0].transform.position - obj.transform.position;
            }
        }
        else
        {
            time = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.StartsWith("enermy"))
        {
            enermyList.Add(collision.gameObject);
        }
        
        print("invoke OnTriggerEnter2D " + collision.gameObject.tag);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.StartsWith("enermy"))
        {
            enermyList.Remove(collision.gameObject);
        }

        print("invoke OnTriggerExit2D " + collision.gameObject.tag);
    }
}
