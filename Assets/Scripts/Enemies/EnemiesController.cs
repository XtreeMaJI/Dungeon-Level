using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    private TurnsManager turnsManager;
    private List<Enemy> enemies;

    void Start()
    {
        turnsManager = FindObjectOfType<TurnsManager>();
        enemies = new List<Enemy>(FindObjectsOfType<Enemy>());
    }

    private void Update()
    {
        if (turnsManager.currentTurn != TurnsManager.Turn.Enemy)
            return;

        enemies.RemoveAll(x => x == null);
        foreach (var enemy in enemies)
        {
            if(enemy)
            {
                enemy.MakeTurn();
            }
        }

        turnsManager.SwitchTurn();
    }

    public bool IsEnemiesRemained()
    {
        foreach(var enemy in enemies)
        {
            if (enemy != null)
            {
                return true;
            }
        }

        return false;
    }

}
