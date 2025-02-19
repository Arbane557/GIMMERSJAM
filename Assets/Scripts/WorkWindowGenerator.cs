using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkWindowGenerator : MonoBehaviour
{
    private void OnEnable()
    {
        transform.GetChild(Random.Range(1,1)).gameObject.SetActive(true);
    }
}
