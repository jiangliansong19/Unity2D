using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 箭抛射体。构造体，攻击目标，攻击后消失。
/// </summary>
public class ArrowProjectile : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;

    public int damageAmount = 10;



    private float moveSpeed = 10f;



    private Transform targetEnermy;


    public static Transform Create(Vector3 position)
    {
        Transform arrowTransform = Resources.Load<Transform>("pfArrow");
        Transform result = Instantiate(arrowTransform, position, Quaternion.identity);
        return result;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (targetEnermy != null)
        {
            Vector3 moveDir = targetEnermy.position - transform.position;

            Vector2 normalized = moveDir.normalized;
            float tan = Mathf.Atan2(normalized.y, normalized.x);
            float deg = tan * Mathf.Rad2Deg;

            transform.position += moveDir * Time.deltaTime * moveSpeed;
            transform.eulerAngles = new Vector3(0, 0, deg);
        }
    }

    public void SetTargetEnermy(Transform trans)
    {
        this.targetEnermy = trans;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enermy enermy = collision.GetComponent<Enermy>();
        if (enermy != null)
        {
            HealthSystem healthSystem = collision.GetComponent<HealthSystem>();
            healthSystem.DoDamage(this.damageAmount);

            Destroy(gameObject);
        } 
    }
}
