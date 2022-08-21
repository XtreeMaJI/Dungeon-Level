using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public PlayerUI playerUI;
    EnemiesController enemiesController;
    public float maxTimeForTurn = 120f;
    private float timeForTurn = 120f;
    private TurnsManager turnsManager;

    private void Start()
    {
        enemiesController = FindObjectOfType<EnemiesController>();
        turnsManager = FindObjectOfType<TurnsManager>();
        RestartTimer();
    }

    public bool IsWin()
    {
        return !enemiesController.IsEnemiesRemained();
    }

    public void GameWin()
    {
        playerUI?.ShowWinPanel();
    }

    public void GameOver()
    {
        playerUI?.ShowLosePanel();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("MainScene");
    }

    private void ForceEndTurn()
    {
        turnsManager.SwitchTurn();
    }

    public void RestartTimer()
    {
        timeForTurn = maxTimeForTurn;
    }

    private void Update()
    {
        timeForTurn -= Time.deltaTime;
        playerUI.SetRemainTime((int)timeForTurn);
        if(timeForTurn <= 0f)
        {
            timeForTurn = maxTimeForTurn;
            ForceEndTurn();
        }
    }

}
