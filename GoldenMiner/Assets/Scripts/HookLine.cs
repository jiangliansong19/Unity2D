using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookLine : MonoBehaviour
{
    public Transform starts;
    LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = this.GetComponent<LineRenderer>();
        lineRenderer.startWidth = 1.1f;
    }

    // Update is called once per frame
    void Update()
    {
        updateLine();
    }

    void updateLine()
    {
        lineRenderer.SetPosition(0, starts.position);
        lineRenderer.SetPosition(1, transform.position);
    }
}
