using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MapAttachableObject
{
    public float maxHealth;
    private float health;
    public float damage;

    public BaseUI UI;

    public virtual void Start()
    {
        health = maxHealth;
    }

    public virtual void TakeDamage(float damage)
    {
        health -= damage;

        if(UI)
            UI.SetHealth(health, maxHealth);

        if (health <= 0)
            Destroy(gameObject);
    }

    public void Attack(Character target)
    {
        if(target)
            target.TakeDamage(damage);
    }
}
