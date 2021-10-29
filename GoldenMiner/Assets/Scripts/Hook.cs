using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RotationDir
{
    left,
    right,
}


public class Hook : MonoBehaviour
{
    public Transform starts;//绳卷位置

    LineRenderer lineRenderer; //线条渲染

    Vector3 hookOrigin;//hook的初始位置

    public RotationDir rotateDir;//hook的摆动方向

    public float rotateSpeed = 0.15f;//hook的摆动速度

    public float moveSpeed = 30.0f;//hook的前进速度

    bool isFiring = false; //hook是否已经发出

    bool isBacking = false; //hook是否正在返回

    PropsScript beHookedProps;//被勾住的道具


    // Start is called before the first frame update
    void Start()
    {
        hookOrigin = transform.position;

        lineRenderer = this.GetComponent<LineRenderer>();
        lineRenderer.startWidth = 1.1f;
    }

    // Update is called once per frame
    void Update()
    {
        updateLine();

        HookRotate();

        if (!isFiring && !isBacking) {
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                isFiring = true;
                isBacking = false;
            }
        } else {
            if (isFiring && !isBacking)
            {
                HookMoveForward();
            }
            else if (isFiring && isBacking)
            {
                HookMoveBack();
            }
        }

        if (HookCollisionBoundary() == true)
        {
            isFiring = true;
            isBacking = true;
        }

        if (HookReturnToOrigin() == true && isBacking && isFiring)
        {
            isFiring = false;
            isBacking = false;
            moveSpeed = 0;
        }
    }

    /// <summary>
    /// Hook碰到道具时
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        PropsScript script = other.gameObject.GetComponent<PropsScript>();
        if (script != null) {
            float tmpDistance = Vector3.Distance(transform.position, script.transform.position);
            script.transform.position = transform.position + transform.up * -1 * tmpDistance;
            script.transform.SetParent(transform);
            moveSpeed = moveSpeed - moveSpeed * 0.15f * script.scaleLevel;

            beHookedProps = script;

            isBacking = true;
            isFiring = true;
        }
    }

    /// <summary>
    /// 更新绳索的位置
    /// </summary>
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
        if (moveSpeed == 0)
        {
            moveSpeed = 30.0f;
        }
        transform.position += transform.up * -1 * moveSpeed * Time.deltaTime;
    }

    /// <summary>
    /// hook向后移动
    /// </summary>
    public void HookMoveBack() {

        if (beHookedProps != null)
        {
            moveSpeed /= beHookedProps.scaleLevel;
        }
        transform.position += transform.up * moveSpeed * Time.deltaTime;
    }

    /// <summary>
    /// Hook是否撞击到了边界
    /// </summary>
    /// <returns></returns>
    bool HookCollisionBoundary()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        if (x > GameMode.Instance.minX && x < GameMode.Instance.maxX && y > GameMode.Instance.minY)
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// Hook是否回到了原点。是，修改moveSpeed = 0，销毁道具
    /// </summary>
    /// <returns></returns>
    bool HookReturnToOrigin()
    {
        float originalDistance = Vector3.Distance(hookOrigin, starts.position);
        float distance = Vector3.Distance(transform.position, starts.position);

        if (distance < originalDistance)   
        {
            if (beHookedProps != null)
            {
                Destroy(beHookedProps.gameObject);
            }
            return true;
        }
        return false;
    }
}
