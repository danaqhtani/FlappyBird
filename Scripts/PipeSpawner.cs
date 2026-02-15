using System.Collections;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public GameObject m_PipePrefab;
    public GameObject m_CoinPrefab;

    public float heightOffset = 10;

    private Bird bird;

    private IEnumerator Start()
    {
        // Find and assign a reference to the Bird instance
        bird = FindAnyObjectByType<Bird>();

        while (true)
        {
            // Spawn a pipe only if the player is ready and the bird is alive
            if (bird.IsReadyAndAlive)
                SpawnPipe();

            yield return new WaitForSeconds(GetSpawnRate);
        }
    }

    private void SpawnPipe()
    {
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;

        var pipeObject = Instantiate(m_PipePrefab, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0), transform.rotation);

        // 50% we span coin
        if (Random.value > .5f)
        {
            // Find the midpoint transform inside the pipe
            Transform getMidPoint = pipeObject.transform.Find("Mid Point");
            // Spawn a coin at the midpoint and set it as a child of the pipe
            var coin = Instantiate(m_CoinPrefab, getMidPoint.position, Quaternion.identity, pipeObject.transform);
            // Set name
            coin.name = "Coin";
        }

    }

    /// <summary>
    /// Returns the spawn rate (seconds between obstacle spawns) based on the selected game mode.
    /// </summary>
    public float GetSpawnRate
    {
        get
        {
            float spawnRate = 0;
            switch (GameManager.Instance.m_GameMode)
            {
                case GameMode.Easy:
                    spawnRate = Random.Range(4, 6);
                    break;
                case GameMode.Hard:
                    spawnRate = Random.Range(1, 2);
                    break;
                case GameMode.Mission:
                    spawnRate = Random.Range(2, 4);
                    break;
            }

            return spawnRate;
        }
    }
}
