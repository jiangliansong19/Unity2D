using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Velocity : MonoBehaviour
{
    public int xSpeed;
    public int ySpeed;

    private void FixedUpdate()
    {
        Rigidbody2D obj = this.GetComponent<Rigidbody2D>();
        obj.velocity = new Vector2(xSpeed, ySpeed);
    }
}
