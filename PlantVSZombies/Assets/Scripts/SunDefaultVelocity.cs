using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunDefaultVelocity : MonoBehaviour
{
    public Vector2 vector;

    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = vector;
    }
}
