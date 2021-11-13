using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敌人：寻找最近的建筑，攻击建筑。
/// </summary>
public class Enermy : MonoBehaviour
{
    [SerializeField] private Transform HQTransform;

    public int health;

    private float searchTimer;
    private float searchTimeMax = 0.5f;

    private float enermySearchRadius = 20f;
    private float moveSpeed = 6f;
    public int enermyDamageAmount = 20;

    private Transform targetTransform;
    private Rigidbody2D rigidBody2D;


    // Start is called before the first frame update
    void Start()
    {
        targetTransform = HQTransform;
        rigidBody2D = GetComponent<Rigidbody2D>();

        GetComponent<HealthSystem>().OnDieHandler += (object sender, System.EventArgs e) =>
        {
            Destroy(gameObject);
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (targetTransform != null)
        {
            Vector3 moveDir = (targetTransform.position - transform.position).normalized;
            rigidBody2D.velocity = moveSpeed * moveDir;
        }
        else
        {
            rigidBody2D.velocity = Vector2.zero;
        }


        searchTimer -= Time.deltaTime;
        if (searchTimer <= 0)
        {
            searchTimer += searchTimeMax;
            SearchNearestBuilding();
        }
    }

    private void SearchNearestBuilding()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, enermySearchRadius);

        bool hasBuildingAround = false;
        foreach (Collider2D item in colliders)
        {
            if (item.GetComponent<BuildingTypeHolder>() != null)
            {
                hasBuildingAround = true;
                if (targetTransform == null)
                {
                    targetTransform = item.transform;
                }
                else
                {
                    if (Vector3.Distance(transform.position, targetTransform.position) > Vector3.Distance(transform.position, item.transform.position)) 
                    {
                        targetTransform = item.transform;
                    }
                }
            }
        }

        if (hasBuildingAround == false)
        {
            targetTransform = HQTransform;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BuildingTypeHolder holder = collision.gameObject.GetComponent<BuildingTypeHolder>();
        if (holder != null)
        {
            HealthSystem healthSystem = holder.GetComponent<HealthSystem>();
            healthSystem.DoDamage(this.enermyDamageAmount);

            Destroy(gameObject);
        }
    }
}
