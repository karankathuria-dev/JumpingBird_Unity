using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 5; // Points to add on collection
    public ScoreManager scoreManager;
    public float spinSpeed = 200f;
    public float speed = 2f;
    public GameObject collectParticlesPrefab;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Check if the player collected it
        {
            scoreManager.AddPoint(coinValue); // Add points
                                              // Instantiate particle effect at coin's position
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
