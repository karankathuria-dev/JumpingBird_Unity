using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public ScoreManager scoreManager;
    public TMP_Text scoreText;
    public TMP_Text highscoreText;

    public GameObject gameOverPanel;
    public GameObject pausePanel;

    private bool isGameOver = false;
    private bool isPaused = false;

    // Inside your GameManager class
    public GameObject continueWithAdButton;

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
        HighScoreSave();
        Time.timeScale = 0f; // Stop all movements
        ShowInterstitialOnGameOver();
        AdsManager.Instance.rewardedAds.LoadAd();
        gameOverPanel.SetActive(true);
        // Disable the button when the game starts
        if (continueWithAdButton != null)
        {
            continueWithAdButton.SetActive(false);
        }

        // Subscribe to the event from the AdsManager
        RewardedAdsManager.OnAdReady += EnableContinueButton;
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
    private void HighScoreSave()
    {
        // Check and save the high score
        int highScore = PlayerPrefs.GetInt("HighScore", 0); // Get existing high score, or 0 if none
        if (scoreManager.currentScore > highScore)
        {
            highScore = scoreManager.currentScore;
            PlayerPrefs.SetInt("HighScore", highScore); // Save the new high score
        }
        // Add the coins from this game to the total
       int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0) + scoreManager.no_OfCoins;
        PlayerPrefs.SetInt("TotalCoins", totalCoins);
        ShowScore(highScore);
    }
    private void ShowScore(int highScore)
    {
        if (scoreText != null)
            scoreText.text = "SCORE : " + scoreManager.currentScore;
        if (highscoreText != null)
            highscoreText.text = "HIGH SCORE :" + highScore;
    }
    public void ShowInterstitialOnGameOver()
    {
        // The AdsManager.Instance syntax gives you direct access.
        AdsManager.Instance.interstitialAds.ShowAd();
    }
    public void ContinueWithAds()
    {
        // This method is called by the "Continue" button on the Game Over screen.
        AdsManager.Instance.rewardedAds.ShowAd();
    }
    public void SetGameOverScreenOff()
    {
        isGameOver = false;
        gameOverPanel.SetActive(false);
    }
    // Inside your GameManager class

    private void EnableContinueButton()
    {
        // Enable the button
        if (continueWithAdButton != null)
        {
            continueWithAdButton.SetActive(true);
        }
    }
    private void OnDestroy()
    {
        RewardedAdsManager.OnAdReady -= EnableContinueButton;
    }
}
