using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLevel : MonoBehaviour
{
    //缩放等级
    public float[] leval;

    private int index = 0;

    private Camera cameraControl;

    public float FOV;

    bool changeStatus = false;

    float now, last;



    // Start is called before the first frame update
    void Start()
    {
        cameraControl = this.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !changeStatus)
        {
            index++;
            changeStatus = !changeStatus;
            now = leval[index % leval.Length];
            last = leval[(index - 1) % leval.Length];
            cameraControl.fieldOfView = last;
        }

        if (changeStatus)
        {
            cameraControl.fieldOfView = Mathf.Lerp(cameraControl.fieldOfView, now = leval[index % leval.Length], 0.1f);
            FOV = cameraControl.fieldOfView;
            if (Mathf.Abs(FOV - now) <= 1)
            {
                changeStatus = !changeStatus;
            }
        }
    }
}
