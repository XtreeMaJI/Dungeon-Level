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
    }

    public override void OnZeroHealth()
    {
        levelManager?.GameOver();
    }
}
