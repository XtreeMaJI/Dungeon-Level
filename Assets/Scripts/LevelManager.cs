using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public PlayerUI playerUI;
    EnemiesController enemiesController;

    private void Start()
    {
        enemiesController = FindObjectOfType<EnemiesController>();
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

}
