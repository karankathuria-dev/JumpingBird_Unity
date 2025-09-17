using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public ScoreManager scoreManager;
    public TMP_Text scoreText;
    public GameObject gameOverPanel;
    public GameObject pausePanel;

    private bool isGameOver = false;
    private bool isPaused = false;

    void Start()
    {
        isGameOver = false;
        isPaused = false;
        gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);
        scoreManager.currentScore = 0;
        scoreManager.UpdateScoreUI();
    }

    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f; // Stop all movements
        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        isGameOver = false;
        isPaused = false;
        gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);
        scoreManager.currentScore = 0;
        scoreManager.UpdateScoreUI();
       UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void PauseGame()
    {
        if (isPaused || isGameOver)
            return;

        isPaused = true;
        Time.timeScale = 0f; // Freeze game
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        if (!isPaused)
            return;

        isPaused = false;
        Time.timeScale = 1f; // Resume game
        pausePanel.SetActive(false);
    }
    public void HomeScreen()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneData._Home);
    }
}
