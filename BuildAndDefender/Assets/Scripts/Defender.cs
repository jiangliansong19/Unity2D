using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ñ°ÕÒµÐÈË£¬¹¥»÷µÐÈË¡£
/// ±»¹¥»÷£¬±»´Ý»Ù¡£
/// </summary>
public class Defender : MonoBehaviour
{

    private float searchTimer;
    private float searchTimeMax = 1f;

    private float fireTimer;
    private float fireTimerMax = 0.5f;

    private GameObject targetEnermy;
    private BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        searchTimer -= Time.deltaTime;
        if (searchTimer <= 0)
        {
            searchTimer += searchTimeMax;
            SearchEnermy();
        }

        if (targetEnermy != null)
        {
            fireTimer -= Time.deltaTime;
            if (fireTimer <= 0)
            {
                fireTimer += fireTimerMax;
                ShootEnermy();
            }
        }

    }

    private void SearchEnermy()
    {
        if (targetEnermy != null) return;

        BuildingTypeSO typeSO = GetComponent<BuildingTypeHolder>().type;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, typeSO.defenderData.defenderRadius);

        foreach (Collider2D item in colliders)
        {
            if (item.GetComponent<Enermy>() != null)
            {
                targetEnermy = item.gameObject;
                return;
            }
        }
    }

    private void ShootEnermy()
    {
        Transform arrowTransform = ArrowProjectile.Create(transform.position + new Vector3(0, boxCollider.size.y, 0));
        arrowTransform.GetComponent<ArrowProjectile>().SetTargetEnermy(targetEnermy.transform);
    }
}
