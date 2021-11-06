using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftObject : MonoBehaviour
{
    private float x;

    // Start is called before the first frame update
    void Start()
    {
        x = -10;
    }

    // Update is called once per frame
    void Update()
    {
        x = x + 1f * Time.deltaTime;
        transform.position = new Vector3(x, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("left-OnTriggerEnter " + collision.gameObject.tag);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        print("left-OnTriggerExit2D " + collision.gameObject.tag);
    }

}
