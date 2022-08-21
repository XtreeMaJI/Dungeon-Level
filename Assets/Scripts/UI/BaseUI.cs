using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BaseUI : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public Image healthBar;

    public void SetHealth(float health, float maxHealth)
    {
        if(healthText)
            healthText.SetText("Health: " + health.ToString());

        if (healthBar)
            healthBar.fillAmount = health/maxHealth;
    }

}
