using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformRandomSpawn : MonoBehaviour
{
    public Vector2 minBounds;
    public Vector2 maxBounds;
    public GameObject platformPrefab;
    public int yValue;
    private float currentyValue;
    private List<GameObject> platforms = new List<GameObject>();
    public GameObject player;

    private void Start()
    {
        
        currentyValue = player.transform.position.y;

        SpawnPlatform();
    }

    private void Update()
    {
        if (player.transform.position.y > currentyValue + 10)
        {
            SpawnPlatform();
        }

        for (int i = platforms.Count - 1; i >= 0; i--)
        {
            if (platforms[i].transform.position.y < player.transform.position.y - 6.5f)
            {
                Destroy(platforms[i]);
                platforms.RemoveAt(i);
            }
        }
    }

    void SpawnPlatform()
    {
        currentyValue = player.transform.position.y;

        for (int i = 0; i < 5; i++)
        {
            var platform = Instantiate(platformPrefab);
            platform.transform.parent = transform;

            Vector2 pos = new Vector2(
                transform.position.x + Random.Range(-3, 3),
                yValue
            );
            yValue += Random.Range(2, 4);

            platform.transform.position = pos;

            platforms.Add(platform);
        }
    }
}
