using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cinemachineCamera;

    private float moveSpeed = 30f;
    private float zoomSpeed = 3f;

    private float minOrthographicSize = 10f;
    private float maxOrthographicSize = 30f;

    private float currentOrthograpicSize;

    // Start is called before the first frame update
    void Start()
    {
        currentOrthograpicSize = cinemachineCamera.m_Lens.OrthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        HandlerMoveMent();
        HandleZoom();
    }

    private void HandlerMoveMent()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = new Vector3(x, y).normalized;

        transform.position += Time.deltaTime * moveSpeed * moveDir;
    }

    private void HandleZoom()
    {

        cinemachineCamera.m_Lens.OrthographicSize += -Input.mouseScrollDelta.y * zoomSpeed;

        currentOrthograpicSize += -Input.mouseScrollDelta.y * zoomSpeed;
        cinemachineCamera.m_Lens.OrthographicSize = Mathf.Clamp(currentOrthograpicSize, minOrthographicSize, maxOrthographicSize);

    }
}
