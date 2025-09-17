using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public ScoreManager scoreManager;
    public float spawnInterval = 3f;
  //  public float xRange = 8f;
    public float yRange = 2f;

    private void Start()
    {
        InvokeRepeating("SpawnCoin", 1f, spawnInterval);
    }

    void SpawnCoin()
    {
        float x = transform.position.x;
        float y = Random.Range(-yRange, yRange);
        Vector2 spawnPosition = new Vector2(x, y);

       GameObject coin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
        coin.GetComponent<Coin>().scoreManager = scoreManager;
    }
}
