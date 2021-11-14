using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuidingBeAttack : MonoBehaviour
{
    private Transform repaireButton;
    private Transform demolishButton;

    // Start is called before the first frame update
    void Start()
    {
        repaireButton = transform.Find("RepairButton");
        repaireButton.gameObject.SetActive(false);

        demolishButton = transform.Find("DemolishButton");
        if (demolishButton != null)
        {
            demolishButton.gameObject.SetActive(false);
        }

        HealthSystem healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDamageHandler += HealthSystem_OnDamageHandler;
        healthSystem.OnDieHandler += HealthSystem_OnDieHandler;
        healthSystem.OnHealHandler += HealthSystem_OnHealHandler;
        healthSystem.OnHealFullHandler += HealthSystem_OnHealFullHandler;

        MouseEnterExits enterExits = transform.GetComponent<MouseEnterExits>();
        enterExits.OnMouseEnter += EnterExits_OnMouseEnter;
        enterExits.OnMouseExit += EnterExits_OnMouseExit;
    }

    private void HealthSystem_OnHealFullHandler(object sender, System.EventArgs e)
    {
        repaireButton.gameObject.SetActive(false);
    }

    private void HealthSystem_OnHealHandler(object sender, System.EventArgs e)
    {
        repaireButton.gameObject.SetActive(true);
    }

    private void HealthSystem_OnDieHandler(object sender, System.EventArgs e)
    {
        Destroy(gameObject);
        SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingDestroyed);
    }

    private void HealthSystem_OnDamageHandler(object sender, HealthSystem.EventHandlerOnDamaneArgs e)
    {
        repaireButton.gameObject.SetActive(true);
    }

    private void EnterExits_OnMouseExit(object sender, System.EventArgs e)
    {
        if (demolishButton != null)
        {
            demolishButton.gameObject.SetActive(false);
        }
    }

    private void EnterExits_OnMouseEnter(object sender, System.EventArgs e)
    {
        if (demolishButton != null)
        {
            demolishButton.gameObject.SetActive(true);
        }
    }
}
