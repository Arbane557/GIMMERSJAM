using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class WindowSpawn : MonoBehaviour
{
    public GameObject[] window;
    public Vector2 minBounds;
    public Vector2 maxBounds;

    public float startInterval = 5f; 
    public float minInterval = 2f; 
    public float decreaseRate = 0.01f;

    private void Start()
    {
        StartCoroutine(SpawnWindow());
    }

    IEnumerator SpawnWindow()
{
    float elapsedTime = 0f;
    float spawnInterval = startInterval;

    while (true)
    {
        GameObject prefab = GetRandomPrefab();
        GameObject win = Instantiate(prefab);
        Vector2 pos = new Vector3(Random.Range(minBounds.x, maxBounds.x), Random.Range(minBounds.y, maxBounds.y), -0.1f);
        win.transform.position = ClampBorder(pos);

        yield return new WaitForSeconds(spawnInterval);

        elapsedTime += spawnInterval;
        spawnInterval = Mathf.Max(minInterval, startInterval - (elapsedTime * decreaseRate));
    }
}

    GameObject GetRandomPrefab()
    {
        int rand = Random.Range(0, 8);
        if (rand <= 4) return window[0];
        else if (rand <= 6) return window[Random.Range(1, 4)];
        else return window[Random.Range(4, 6)];
    }

    Vector2 ClampBorder(Vector2 pos)
    {
        pos.x = Mathf.Clamp(pos.x, minBounds.x, maxBounds.x);
        pos.y = Mathf.Clamp(pos.y, minBounds.y, maxBounds.y);
        return pos;
    }
}
