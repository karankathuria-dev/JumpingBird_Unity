using UnityEngine;

public class LifePickup : MonoBehaviour
{
    private LifeManager lifeManager;
    public float speed = 2f; // Move speed if needed
    public float destroyX = -10f; // X position where heart is destroyed
    public float scaleSpeed = 1f; // Speed of scaling animation
    public float scaleAmount = 0.2f; // How much it scales up/down
    public GameObject heartCollectiblePrefab;

    private Vector3 initialScale;

    private void Start()
    {
        initialScale = transform.localScale;
        lifeManager = FindFirstObjectByType<LifeManager>();
    }
    void Update()
    {
        // Move the heart to the left
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        // Animate scale up and down
        float scale = Mathf.PingPong(Time.time * scaleSpeed, scaleAmount) + 1f;
        transform.localScale = initialScale * scale;

        


        // Destroy if it goes off screen
        if (transform.position.x < destroyX)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && lifeManager !=null)
        {
            lifeManager.GainLife();
            // Instantiate particle effect at coin's position
            Instantiate(heartCollectiblePrefab, transform.position, Quaternion.identity);
            Destroy(gameObject); // Remove the pickup after collecting
        }
    }
}
