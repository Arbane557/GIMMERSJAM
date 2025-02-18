using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WindowSpawn : MonoBehaviour
{
    public GameObject[] window;
    public Vector2 minBounds;
    public Vector2 maxBounds;

    private Animator anim;
    private void Start()
    {
        StartCoroutine(spawnWindow());
    }
    IEnumerator spawnWindow()
    {
        while (true)
        {
            GameObject win = Instantiate(window[0]);
            Vector2 pos = new Vector2(Random.Range(minBounds.x, maxBounds.x), Random.Range(minBounds.y, maxBounds.y));
            win.transform.position = clampBorder(pos);
            yield return new WaitForSeconds(1);
        }
    }
    Vector2 clampBorder(Vector2 pos)
    {
        pos.x = Mathf.Clamp(pos.x, minBounds.x, maxBounds.x);
        pos.y = Mathf.Clamp(pos.y, minBounds.y, maxBounds.y);
        return pos;
    }

  
}
