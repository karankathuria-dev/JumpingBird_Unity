using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    public ScoreManager scoreManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Bird should have the "Player" tag
        {
           // Debug.Log("I am here");
            scoreManager.AddPoint(1);
            Destroy(gameObject); // Remove the trigger after scoring once
        }
    }
}
