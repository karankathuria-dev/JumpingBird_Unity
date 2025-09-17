using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public GameObject pipePrefab;
    public GameObject scoreTriggerPrefab;
    public ScoreManager scoreManager;

    public float spawnRate = 2f;
    public float minY = -2f;
    public float maxY = 2f;

    public float initialGapSize = 2.5f;
    public float minGapSize = 1.2f;
    public float gapShrinkPerPoint = 0.05f;

    public float initialPipeSpeed = 2f;
    public float maxPipeSpeed = 5f;
    public float speedIncreasePerPoint = 0.1f;

    private float currentGapSize;
    private float currentPipeSpeed;
    private float timer = 0f;

    void Start()
    {
        currentGapSize = initialGapSize;
        currentPipeSpeed = initialPipeSpeed;
    }

    void Update()
    {
        UpdateDifficulty();

        timer += Time.deltaTime;
        if (timer >= spawnRate)
        {
            SpawnPairedPipes();
            timer = 0f;
        }
    }

    void UpdateDifficulty()
    {
        int score = scoreManager.currentScore;

        currentGapSize = initialGapSize - score * gapShrinkPerPoint;
        if (currentGapSize < minGapSize)
            currentGapSize = minGapSize;

        currentPipeSpeed = initialPipeSpeed + score * speedIncreasePerPoint;
        if (currentPipeSpeed > maxPipeSpeed)
            currentPipeSpeed = maxPipeSpeed;
    }

    void SpawnPairedPipes()
    {
        float pipeHeight = pipePrefab.GetComponent<SpriteRenderer>().bounds.size.y;

        float centerY = Random.Range(minY + currentGapSize / 2f, maxY - currentGapSize / 2f);

        // Top pipe
        Vector3 topPos = new Vector3(transform.position.x, centerY + currentGapSize / 2f + pipeHeight / 2f, 0);
        GameObject topPipe = Instantiate(pipePrefab, topPos, Quaternion.Euler(0, 0, 180f));
        SpriteRenderer sr = topPipe.GetComponent<SpriteRenderer>();
        sr.color = Random.value > 0.5f ? Color.green : Color.red;
        topPipe.AddComponent<PipeMovement>().speed = currentPipeSpeed;


        // Bottom pipe
        Vector3 bottomPos = new Vector3(transform.position.x, centerY - currentGapSize / 2f - pipeHeight / 2f, 0);
        GameObject bottomPipe = Instantiate(pipePrefab, bottomPos, Quaternion.identity);
        SpriteRenderer sr_1 = bottomPipe.GetComponent<SpriteRenderer>();
        sr_1.color = sr.color;
        bottomPipe.AddComponent<PipeMovement>().speed = currentPipeSpeed;

        // Score Trigger
        Vector3 triggerPos = new Vector3(transform.position.x, centerY, 0);
        GameObject trigger = Instantiate(scoreTriggerPrefab, triggerPos, Quaternion.identity);
        trigger.AddComponent<PipeMovement>().speed = currentPipeSpeed;
        trigger.GetComponent<ScoreTrigger>().scoreManager = scoreManager;
    }
}
