using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTime : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(destroyTime());
    }
    IEnumerator destroyTime()
    {
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }
}
