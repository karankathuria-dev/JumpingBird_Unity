using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class HomeController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI coinsText;
   

    // UI elements for your leaderboard screen
    public GameObject leaderboardPanel;
    public Transform leaderboardContent; // The parent object for your score entries
    public GameObject leaderboardEntryPrefab; // A prefab for a single score entry

    public GameObject settingsPanel;


    private int totalCoins;
   [SerializeField] private LeaderboardManager leaderboardManager;

    private void Start()
    {
        // Load the saved High Score and display it on the UI
        int loadedHighScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = loadedHighScore.ToString();
        int currentScore = PlayerPrefs.GetInt("CurrentScore");
        if (currentScore > 0)
        {
            leaderboardManager.SubmitScoreAsync(currentScore);
        }
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
    public async void OnLeaderboardButtonClicked()
    {
        // First, show the leaderboard panel
        leaderboardPanel.SetActive(true);

        // Then, get the scores from the service
        var scores = await leaderboardManager.GetScoresAsync();

        if (scores != null)
        {
            // Clear any old entries from a previous view
            foreach (Transform child in leaderboardContent)
            {
                Destroy(child.gameObject);
            }

            // Loop through the returned scores and populate the UI
            foreach (var score in scores)
            {
                // Instantiate a new entry from your prefab
                GameObject newEntry = Instantiate(leaderboardEntryPrefab, leaderboardContent);

                // Get the TextMeshPro components from your prefab
                // You'll need to name these in your prefab to match
                
                 var rankText = newEntry.transform.Find("RankText").GetComponent<TMPro.TextMeshProUGUI>();
                 var scoreText = newEntry.transform.Find("ScoreText").GetComponent<TMPro.TextMeshProUGUI>();
                // You can add a TextMeshPro for player name if you want to display a placeholder
                 var nameText = newEntry.transform.Find("NameText").GetComponent<TMPro.TextMeshProUGUI>();


                // Set the text values
                 rankText.text = score.Rank.ToString();
                 scoreText.text = score.Score.ToString();
                // For anonymous players, you can use a generic name
                 nameText.text = "Player";
            }
        }
    }

    public void closeLeaderboardPanel()
    {
        leaderboardPanel.SetActive(false);
    }
    public void OnCloseSettingsButtonClicked()
    {
        // Hide the settings panel
        settingsPanel.SetActive(false);

    }
    

    public void OnOpenSettingsButtonClicked()
    {
        // Activate the settings panel
        settingsPanel.SetActive(true);
    }
}
