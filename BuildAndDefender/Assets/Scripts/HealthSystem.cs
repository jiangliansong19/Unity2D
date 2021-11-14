using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 记录对象的生命值, 管理生命条。通知对象：收到伤害，死亡。
/// </summary>
public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnDieHandler;
    public event EventHandler<EventHandlerOnDamaneArgs> OnDamageHandler;
    public event EventHandler OnHealHandler;
    public event EventHandler OnHealFullHandler;
 
    public class EventHandlerOnDamaneArgs: EventArgs
    {
        public int damageAmount;
    }

    private int health;
    private int healthMax;

    private HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = transform.Find("HealthBar").GetComponent<HealthBar>();

        health = GetOrginalHealthAmount();
        healthMax = health;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.gameObject.SetActive(health != healthMax);
    }

    public void DoDamage(int damageAmount)
    {
        health -= damageAmount;

        OnDamageHandler?.Invoke(this, new EventHandlerOnDamaneArgs { damageAmount = damageAmount });
        if (health <= 0)
        {
            OnDieHandler?.Invoke(this, EventArgs.Empty);
        }

        if (healthBar != null)
        {
            healthBar.SetHealthPercent((float)health / (float)healthMax);
        }
    }

    private int GetOrginalHealthAmount()
    {
        int health = 0;
        BuildingTypeHolder holder = GetComponent<BuildingTypeHolder>();
        if (holder != null)
        {
            health = holder.type.health;
        }

        Enermy enermy = GetComponent<Enermy>();
        if (enermy != null)
        {
            health = enermy.health;
        }
        return health;
    }

    public int GetHealthAmountMax()
    {
        return healthMax;
    }

    public int GetHealthAmount()
    {
        return health;
    }

    public void HealFull()
    {
        health = healthMax;
        OnHealFullHandler?.Invoke(this, EventArgs.Empty);
    }


}
