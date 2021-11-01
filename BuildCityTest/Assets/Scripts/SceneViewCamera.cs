using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SceneViewCamera : MonoBehaviour
{
    [SerializeField, Range(0.1f, 100f)]
    private float wheelSpeed = 1.0f;

    [SerializeField, Range(0.1f, 100f)]
    private float moveSpped = 0.3f;

    [SerializeField, Range(0.1f, 1.0f)]
    private float rotateSpeed = 0.3f;

    private Vector3 preMousePosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MouseUpdate();
    }

    private void MouseUpdate()
    {
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheel != 0.0f)
        {
            MouseWheel(scrollWheel);
        }

        if (Input.GetMouseButtonDown(0) ||
            Input.GetMouseButtonDown(1) ||
            Input.GetMouseButtonDown(2))
        {
            preMousePosition = Input.mousePosition;
        }
        MouseDrag(Input.mousePosition);
        
    }

    private void MouseWheel(float delta)
    {
        transform.position += transform.forward * delta * wheelSpeed;
    }

    public void CameraRotate(Vector2 angle)
    {
        transform.RotateAround(transform.position, transform.right, angle.x);
        transform.RotateAround(transform.position, Vector3.up, angle.y);
    }
    private void MouseDrag(Vector3 mousePos) 
    {
        Vector3 diff = mousePos - preMousePosition;
        if (diff.magnitude < Vector3.kEpsilon)
        {
            return;
        }

        if (Input.GetMouseButton(2))
        {
            transform.Translate(-diff * Time.deltaTime * moveSpped);
        }
        else if (Input.GetMouseButton(1))
        {
            CameraRotate(new Vector2(-diff.y, diff.x) * rotateSpeed);
        }
        preMousePosition = mousePos;
    }
}
