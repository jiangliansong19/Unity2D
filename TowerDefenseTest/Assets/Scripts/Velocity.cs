using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Velocity : MonoBehaviour
{
    public Vector2 v2;

    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = v2;
    }
}
