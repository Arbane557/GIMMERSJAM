using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkWindowGenerator : MonoBehaviour
{
    private void OnEnable()
    {
        transform.GetChild(Random.Range(0,4)).gameObject.SetActive(true);
    }
}
