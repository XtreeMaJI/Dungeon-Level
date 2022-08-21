using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerInputHandler))]
public class Player : Character
{
    private LevelManager levelManager;

    public override void Start()
    {
        base.Start();
        levelManager = FindObjectOfType<LevelManager>();
        (UI as PlayerUI).SetDamage(damage);
    }

    public override void OnZeroHealth()
    {
        levelManager?.GameOver();
    }

    public void IncreaseDamage(float incDamage)
    {
        damage += incDamage;
        if(UI)
            (UI as PlayerUI).SetDamage(damage);
    }
}
