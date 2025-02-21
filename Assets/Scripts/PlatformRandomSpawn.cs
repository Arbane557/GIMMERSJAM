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
    private List<GameObject> platforms;
    public GameObject player;
    private void Start()
    {
        currentyValue = player.transform.position.y;
        spawnPlatform();
        spawnPlatform();
    }
    private void Update()
    {
        if (player.transform.position.y > currentyValue + 10)
        {
            spawnPlatform();
        }
    }
   
    void spawnPlatform()
    {
        currentyValue = player.transform.position.y;
        for (int i = 0; i < 10; i++)
        {          
            var platform = Instantiate(platformPrefab);
            platform.transform.position = transform.position;
            platform.transform.parent = this.transform;
            Vector2 pos = new Vector2(transform.position.x + Random.Range(-3,3), yValue);
            yValue += Random.Range(2, 4);
            platform.transform.position = pos;
        }
    }
}
