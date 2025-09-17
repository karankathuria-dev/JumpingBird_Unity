using System.Collections;
using UnityEngine;

public class HeartSpawner : MonoBehaviour
{
    public GameObject heartPrefab; // Assign your heart prefab here
    //public float spawnInterval = 10f; // Time between spawns
    public Vector2 spawnAreaMin; // Minimum X, Y
    public Vector2 spawnAreaMax; // Maximum X, Y

    void Start()
    {
        StartCoroutine(SpawnHearts());
    }

    IEnumerator SpawnHearts()
    {
        while (true)
        {
            float randomInterval = Random.Range(5f, 15f);
            yield return new WaitForSeconds(randomInterval);

            // Generate random position within bounds
            Vector3 spawnPosition = new Vector3(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y),
                0f);

           // Collider2D hit = Physics2D.OverlapCircle(spawnPosition, 0.5f);
           // if (hit == null)
           // {
                Instantiate(heartPrefab, spawnPosition, Quaternion.identity);
           // }
        }
    }
}
