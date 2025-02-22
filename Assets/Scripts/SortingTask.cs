using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SortingTask : MonoBehaviour
{
    public GameObject[] fileSprites;
    public GameObject workParent;
    public GameObject childMain;
    public Color red, normal;
    public float blinkDuration = 2f;
    public float blinkInterval = 0.2f;
    private void OnEnable()
    {
        List<int> numbers = new List<int> { 1, 2, 3, 4 };
        for (int i = 0; i < numbers.Count; i++)
        {
            int randomIndex = Random.Range(i, numbers.Count);
            int temp = numbers[i];
            numbers[i] = numbers[randomIndex];
            numbers[randomIndex] = temp;
        }
        
        for (int i = 0; i < fileSprites.Length; i++)
        {
            fileSprites[i].name = numbers[i].ToString();
            fileSprites[i].GetComponent<FileDrag>().textName.text = fileSprites[i].name;
        }
    }

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
                workParent.GetComponent<WorkWindowButton>().subtractWorkBar();
                StartCoroutine(wrong());
                return;
            }
        }
        Debug.Log("Success");
        workParent.GetComponent<WorkWindowButton>().addWorkBar();
        transform.parent.GetComponent<Animator>().SetBool("close", true);
        gameObject.SetActive(false);

    }

    IEnumerator wrong()
    {
        float elapsed = 0f;
        while (elapsed < blinkDuration)
        {
            yield return new WaitForSeconds(blinkInterval);
            childMain.transform.GetChild(0).transform.GetComponent<RawImage>().color = red;
            yield return new WaitForSeconds(blinkInterval);
            childMain.transform.GetChild(0).transform.GetComponent<RawImage>().color = normal;
            elapsed += blinkInterval;
        }
        transform.parent.GetComponent<Animator>().SetBool("close", true);
        gameObject.SetActive(false);
    }
}
