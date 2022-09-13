using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public event EventHandler OnHealthChanged;

    [SerializeField] private int maxHP = 10;
    [SerializeField] private int HP = 10;
    [SerializeField] private float pushRecoverySpeed = 0.2f;

    public Transform pfHealthBar;

    protected float immuneTime = 1f;
    protected float lastImmune;

    protected Vector3 pushDir;

    private void Awake()
    {
        HP = maxHP;
        InstantiateHPbar();
    }

    private void Start()
    {
        InstantiateHPbar();
    }

    private void InstantiateHPbar()
    {
        Transform healthBarTransform = Instantiate(pfHealthBar, new Vector3(transform.position.x, 2.3f, transform.position.z), Quaternion.identity);
        healthBarTransform.SetParent(this.gameObject.transform);
        HealthBar healthBar = healthBarTransform.GetComponent<HealthBar>();
        healthBar.Setup(this);
    }

    public float GetHealthPercent()
    {
        return (float)HP / maxHP;
    }

    protected virtual void ReceiveDamage(Damage dmg)
    {
        if(Time.time - lastImmune > immuneTime)
        {
            lastImmune = Time.time;
            HP -= dmg.damageAmount;
            pushDir = (transform.position - dmg.origin).normalized * dmg.pushForce;

            GameManager.instance.ShowText(dmg.damageAmount.ToString(), 30, Color.red, transform.position * 1.01f, transform.up*100f, 1f);

            if(HP <= 0)
            {
                HP = 0;
                Death();
            }

            if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
        }
    }

    protected virtual void Death()
    {
        gameObject.SetActive(false);
        HP = maxHP;

        if (gameObject.transform.CompareTag("Enemy")) GameManager.instance.SetMoney(10);
    }
}
