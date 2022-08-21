using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUI : BaseUI
{
    private LevelManager levelManager;

    public GameObject gamePanel;
    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject leavePanel;

    public TextMeshProUGUI remainTimeText;

    public void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        ShowGamePanel();
    }

    private void HideAllPanels()
    {
        gamePanel?.SetActive(false);
        winPanel?.SetActive(false);
        losePanel?.SetActive(false);
        leavePanel?.SetActive(false);
    }

    public void ShowGamePanel()
    {
        HideAllPanels();
        gamePanel?.SetActive(true);
    }

    public void ShowWinPanel()
    {
        HideAllPanels();
        winPanel?.SetActive(true);
    }

    public void ShowLosePanel()
    {
        HideAllPanels();
        losePanel?.SetActive(true);
    }

    public void ShowLeavePanel()
    {
        HideAllPanels();
        leavePanel?.SetActive(true);
    }

    public void OnButtonRestart()
    {
        levelManager.RestartLevel();
    }

    public void OnCloseButton()
    {
        ShowGamePanel();
    }

    public void SetDamage(float damage)
    {
        if (damageText)
            damageText.SetText("Damage: " + damage.ToString());
    }

    public void SetRemainTime(int remainTime)
    {
        remainTimeText.SetText(remainTime.ToString());
    }

}
