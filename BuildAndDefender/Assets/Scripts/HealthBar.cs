using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Transform barTransform;

    // Start is called before the first frame update
    void Start()
    {
        barTransform = transform.Find("Bar");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHealthPercent(float percent)
    {
        barTransform.localScale = new Vector3(percent, 1, 1);
    }
}
