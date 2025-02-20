using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Vector2 pos = new Vector2(Random.Range(minBounds.x, maxBounds.x), Random.Range(minBounds.y, maxBounds.y));
        win.transform.position = ClampBorder(pos);

        if (prefab == window[1] || prefab == window[2])
        {
            Destroy(win, 5f);
        }

        yield return new WaitForSeconds(spawnInterval);

        elapsedTime += spawnInterval;
        spawnInterval = Mathf.Max(minInterval, startInterval - (elapsedTime * decreaseRate));
    }
}

    GameObject GetRandomPrefab()
    {
        float rand = Random.value;

        if (rand < 0.4f)  // 
            return window[0]; 
        else if (rand < 0.7f)
            return window[1]; 
        else  // 
            return window[2]; 
    }

    Vector2 ClampBorder(Vector2 pos)
    {
        pos.x = Mathf.Clamp(pos.x, minBounds.x, maxBounds.x);
        pos.y = Mathf.Clamp(pos.y, minBounds.y, maxBounds.y);
        return pos;
    }
}
