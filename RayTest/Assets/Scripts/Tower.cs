using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tower : MonoBehaviour
{
    public GameObject BulletPrefab;

    private List<GameObject> bulletObjects;
    private List<GameObject> enermyObjects;

    //public event EventHandler OnTowrFire;

    private float timer;
    private float maxTime;

    private float bulletSpeed;

    // Start is called before the first frame update
    void Start()
    {
        bulletSpeed = 3;
        maxTime = 1;
        bulletObjects = new List<GameObject>();
        enermyObjects = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject obj in bulletObjects)
        {
            obj.transform.position += new Vector3(-3 * Time.deltaTime, -3 * Time.deltaTime - 10, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.StartsWith("person"))
        {
            enermyObjects.Add(collision.gameObject);

            Debug.Log("person leaev fire range");
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.StartsWith("person"))
        {
            enermyObjects.Remove(collision.gameObject);

            Debug.Log("person enter fire range");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer += maxTime;
            GameObject bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
            bulletObjects.Add(bullet);
        }
    }


}
