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
    public float decreaseRate = 0.05f;

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
            GameObject win = Instantiate(window[0]);
            Vector2 pos = new Vector2(Random.Range(minBounds.x, maxBounds.x), Random.Range(minBounds.y, maxBounds.y));
            win.transform.position = ClampBorder(pos);

            yield return new WaitForSeconds(spawnInterval);

            elapsedTime += spawnInterval;

            spawnInterval = Mathf.Max(minInterval, startInterval - (elapsedTime * decreaseRate));
        }
    }

    Vector2 ClampBorder(Vector2 pos)
    {
        pos.x = Mathf.Clamp(pos.x, minBounds.x, maxBounds.x);
        pos.y = Mathf.Clamp(pos.y, minBounds.y, maxBounds.y);
        return pos;
    }
}
