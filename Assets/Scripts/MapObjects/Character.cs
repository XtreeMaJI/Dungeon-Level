using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MapAttachableObject
{
    public float maxHealth;
    private float health;
    public float damage;

    public BaseUI UI;

    private Animator animator;

    public virtual void Start()
    {
        health = maxHealth;
        UI.SetHealth(health, maxHealth);
        animator = GetComponent<Animator>();
    }

    public virtual void TakeDamage(float damage)
    {
        health -= damage;

        if(UI)
            UI.SetHealth(health, maxHealth);

        if (health <= 0)
            OnZeroHealth();
    }

    public void Attack(Character target)
    {
        if(target)
        {
            target.TakeDamage(damage);
            transform.LookAt(target.transform);

            if (animator)
                animator.SetBool("IsAttack", true);
        }
    }

    public void OnAttackEnd()
    {
        if(animator)
            animator.SetBool("IsAttack", false);
    }

    public abstract void OnZeroHealth(); 

}
