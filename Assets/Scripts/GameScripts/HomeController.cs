using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class HomeController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private GameObject shopPanel;
    private int totalCoins;

    private void Start()
    {
        // Load the saved High Score and display it on the UI
        int loadedHighScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = loadedHighScore.ToString();
        UpdateCoinDisplay();
       
    }
    public void GotoGame()
    {
        AdsManager.Instance.interstitialAds.ShowAd();
        SceneManager.LoadScene(SceneData._GameView);
    }
   
    public void UpdateCoinDisplay()
    {
        // Load the total coins and display them on the UI
        totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        coinsText.text = totalCoins.ToString();
    }
}
