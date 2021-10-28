using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RotationDir
{
    left,
    right,
}


public class HookLine : MonoBehaviour
{
    public Transform starts;
    LineRenderer lineRenderer;

    Vector3 hookOrigin;

    public RotationDir rotateDir;
    public float rotateSpeed = 0.15f;

    public float moveSpeed = 2.5f;

    bool isFiring = false; //hook是否已经发出

    bool isBacking = false; //hook是否正在返回

    private void Awake() {
        hookOrigin = transform.position;
    }

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

        HookRotate();

        if (!isFiring && !isBacking) {
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
                if (!isFiring && !isBacking) {
                    isFiring = true;
                    isBacking = false;
                }
            }
        } else {
            if (isFiring && !isBacking) {
                HookMoveForward();
            } else if (isFiring && isBacking) {
                HookBackMove();
            }
        }

        if (HookCollisionBoundary() == true) {
            isFiring = true;
            isBacking = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        PropsScript script = other.gameObject.GetComponent<PropsScript>();
        if (script != null) {
            float tmpDistance = Vector3.Distance(transform.position, script.transform.position);
            script.transform.position = transform.position + transform.up * -1 * tmpDistance;
            script.transform.SetParent(transform);
            moveSpeed = moveSpeed - moveSpeed * 0.15f * script.scaleLevel;
            
            isBacking = true;
            isFiring = true;
        }
    }


    void updateLine()
    {
        lineRenderer.SetPosition(0, starts.position);
        lineRenderer.SetPosition(1, transform.position);
    }

    /// <summary>
    /// Hook开始左右摆动
    /// </summary>
    public void HookRotate() {

        if (isFiring) {
            return;
        }

        float rightAngle = Vector3.Angle(transform.up * -1, Vector3.right);

        if (rotateDir == RotationDir.left) {
            if (rightAngle < 170) {
                transform.RotateAround(starts.position, Vector3.forward, rotateSpeed * Time.deltaTime);
            } else {
                rotateDir = RotationDir.right;
            }
        } else {
            if (rightAngle > 10) {
                transform.RotateAround(starts.position, Vector3.forward, -rotateSpeed * Time.deltaTime);
            } else {
                rotateDir = RotationDir.left;
            }
        }

    }

    /// <summary>
    /// hook向前移动
    /// </summary>
    public void HookMoveForward() {
        transform.position += transform.up * -1 * moveSpeed * Time.deltaTime;
    }

    /// <summary>
    /// hook向后移动
    /// </summary>
    public void HookBackMove() {
        transform.position += transform.up * moveSpeed * Time.deltaTime;
    }

    
    bool HookCollisionBoundary() {
        float x = transform.position.x;
        float y = transform.position.y;
        if (x <= GameMode.Instance.minX || x >= GameMode.Instance.maxX 
            || y <= GameMode.Instance.minY) {
                return true;
            }

        return false;
    }
}
