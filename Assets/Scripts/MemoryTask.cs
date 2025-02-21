using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryTask : MonoBehaviour
{

    public Color highlight, covered;
    int count = 0;
    int confirmed = 0;
    private void OnEnable()
    {
        foreach(Transform t in transform)
        {
            bool random = Random.Range(1, 3) == 1;
            t.GetComponent<RawImage>().color = random ? highlight : covered;
            if (random) { count++; t.gameObject.name = "target"; }
        }
        StartCoroutine(turnOff());
    }

    IEnumerator turnOff()
    {
        yield return new WaitForSeconds(2);
        foreach (Transform t in transform)
        {
            t.GetComponent<RawImage>().color = covered;
        }
    }

    public void confirm(GameObject button)
    {
        if (button.name == "target")
        {
            Debug.Log("success");
            button.name = "button";
            confirmed++;
            if (confirmed == count) Debug.Log("successssssssssss");
        }
        else Debug.Log("failed");

    }
}
