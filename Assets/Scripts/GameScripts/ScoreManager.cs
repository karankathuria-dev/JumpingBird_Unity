using UnityEngine;
using TMPro; // Import TextMeshPro namespace

public class ScoreManager : MonoBehaviour
{
    public int currentScore = 0;
    public TMP_Text scoreText; // Reference to the UI Text
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

   public void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = currentScore.ToString();
    }
}
