
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 5; // Points to add on collection
    private int noOfCoins = 0;
    public ScoreManager scoreManager;
    public float spinSpeed = 200f;
    public float speed = 2f;
    public GameObject collectParticlesPrefab;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Check if the player collected it
        {
            noOfCoins++;
            scoreManager.AddPoint(coinValue); // Add points
            scoreManager.AddCoins(noOfCoins);
            Instantiate(collectParticlesPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject); // Remove the coin
        }
    }
    private void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x < -10f)
        {
            Destroy(gameObject);
        }
        transform.Rotate(0f, 0f, spinSpeed * Time.deltaTime);
    }
}
