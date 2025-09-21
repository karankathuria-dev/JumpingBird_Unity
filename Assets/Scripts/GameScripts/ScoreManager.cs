using UnityEngine;
using TMPro; // Import TextMeshPro namespace

public class ScoreManager : MonoBehaviour
{
    public int currentScore = 0;
    public int no_OfCoins = 0;
    public TMP_Text scoreText; // Reference to the UI Text
    public TMP_Text coinText;
    public AudioSource pointSound;

    void Start()
    {
        UpdateScoreUI();
    }

    public void AddPoint(int amount)
    {
        // Play the point sound
        if (pointSound != null)
        {
           // Debug.Log("I am here");
            pointSound.Play();
        }
        currentScore+=amount;
       
        UpdateScoreUI();
    }
    public void AddCoins(int noOfCoins)
    {
        // Play the point sound
        if (pointSound != null)
        {
            // Debug.Log("I am here");
            pointSound.Play();
        }
       no_OfCoins += noOfCoins;
        UpdateScoreUI();
    }

   public void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = currentScore.ToString();
        if (coinText != null)
            coinText.text = ": " + no_OfCoins;
        
    }
}
