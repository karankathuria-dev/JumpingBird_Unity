using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float initialFlapForce = 5f;    // Starting flap force
    public float maxFlapForce = 10f;       // Maximum flap force limit
    public float increaseRate = 0.1f;      // How much force increases per second
   // public GameManager gameManager;
    public LifeManager lifeManager;

    private float flapForce;
    private Rigidbody2D rb;
    private bool hasStarted = false; // Tracks if the game has started

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        flapForce = initialFlapForce;
        rb.gravityScale = 0f; // Start without gravity
    }

    void Update()
    {
        // Check for touch input
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (!hasStarted)
            {
                StartGame();
            }
            Flap();
        }

        // If game has started, increase flap force over time
        if (hasStarted)
        {
            IncreaseFlapForce();
        }
    }

    private void StartGame()
    {
        hasStarted = true;
        rb.gravityScale = 1f; // Enable gravity
    }

    private void Flap()
    {
        rb.linearVelocity = Vector2.up * flapForce;
    }

    private void IncreaseFlapForce()
    {
        if (flapForce < maxFlapForce)
        {
            flapForce += increaseRate * Time.deltaTime;
            if (flapForce > maxFlapForce)
                flapForce = maxFlapForce;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!lifeManager) return;

        // When bird hits pipes or ground
       
        lifeManager.LooseLife();
        

        // Optionally, stop bird movement
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
    }
}
