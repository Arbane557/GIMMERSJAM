using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingTask : MonoBehaviour
{
    public GameObject[] fileSprites;
    public void CheckOrder()
    {
        System.Array.Sort(fileSprites, (a, b) =>
            a.transform.position.x.CompareTo(b.transform.position.x)
        );

        for (int i = 0; i < fileSprites.Length; i++)
        {
            int fileNum = int.Parse(fileSprites[i].gameObject.name);
            int expectedNumber = i + 1;

            if (fileNum != expectedNumber)
            {
                Debug.Log("Failed");
                return;
            }
        }

        Debug.Log("Success");
    }
}
