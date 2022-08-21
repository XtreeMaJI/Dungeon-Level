using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MapAttachableObject
{
    public bool isTrappedChest = false;
    public float damageFromTrap = 10;
    public float bonusDamage = 20;

    public void OpenChest(Player opener)
    {
        if (isTrappedChest)
        {
            opener.TakeDamage(damageFromTrap);
        }
        else
        {
            opener.IncreaseDamage(bonusDamage);
        }

        Destroy(gameObject);
    }
}
