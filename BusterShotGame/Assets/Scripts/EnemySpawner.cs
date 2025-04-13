using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [Tooltip("Drag all possible enemy prefabs here.")]
    [SerializeField] private GameObject[] enemyPrefabs; // an array of possible enemies
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private int spawnAmountPerInterval = 1;
    [SerializeField] private float spawnDistanceFromScreen = 1f;
    [SerializeField] private float randomOffset = 1f;

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            for (int i = 0; i < spawnAmountPerInterval; i++)
            {
                SpawnEnemyAtEdge();
            }
            timer = 0f;
        }
    }

    private void SpawnEnemyAtEdge()
    {
        // (Same logic to pick a random edge)
        Camera cam = Camera.main;
        float screenHalfHeight = cam.orthographicSize;
        float screenHalfWidth = screenHalfHeight * cam.aspect;

        int edge = Random.Range(0, 4);
        Vector2 spawnPos = Vector2.zero;

        switch (edge)
        {
            case 0: // Top
                spawnPos.x = Random.Range(-screenHalfWidth, screenHalfWidth);
                spawnPos.y = screenHalfHeight + spawnDistanceFromScreen;
                break;
            case 1: // Bottom
                spawnPos.x = Random.Range(-screenHalfWidth, screenHalfWidth);
                spawnPos.y = -(screenHalfHeight + spawnDistanceFromScreen);
                break;
            case 2: // Left
                spawnPos.x = -(screenHalfWidth + spawnDistanceFromScreen);
                spawnPos.y = Random.Range(-screenHalfHeight, screenHalfHeight);
                break;
            case 3: // Right
                spawnPos.x = (screenHalfWidth + spawnDistanceFromScreen);
                spawnPos.y = Random.Range(-screenHalfHeight, screenHalfHeight);
                break;
        }

        spawnPos += Random.insideUnitCircle * randomOffset;
        Vector3 spawnWorldPos = new Vector3(spawnPos.x, spawnPos.y, 0);

        // NEW: Randomly pick from array
        if (enemyPrefabs.Length > 0)
        {
            int index = Random.Range(0, enemyPrefabs.Length);
            GameObject chosenEnemyPrefab = enemyPrefabs[index];

            Instantiate(chosenEnemyPrefab, spawnWorldPos, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("No enemy prefabs assigned to EnemySpawner!");
        }
    }
}
