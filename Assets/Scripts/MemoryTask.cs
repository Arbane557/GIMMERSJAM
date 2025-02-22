using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryTask : MonoBehaviour
{

    public Color highlight, covered, wrong, chosen;
    int count = 0;
    int confirmed = 0;
    public GameObject workParent;
    public Transform tf;
    private void OnEnable()
    {     

        foreach (Transform t in tf)
        {
            t.GetComponent<Button>().enabled = false;
            bool random = Random.Range(1, 4) == 1;
            t.GetComponent<RawImage>().color = random ? highlight : covered;
            if (random) { count++; t.name = "target"; }
        }
        StartCoroutine(turnOff());
    }

    IEnumerator turnOff()
    {
        yield return new WaitForSeconds(2);
        foreach (Transform t in tf)
        {
            t.GetComponent<RawImage>().color = covered;
            t.GetComponent<Button>().enabled = true;
        }
    }
    public void confirm(GameObject button)
    {
        if (button.name == "target")
        {
            Debug.Log("success");
            button.name = "button";
            button.GetComponent<RawImage>().color = chosen;
            confirmed++;
            if (confirmed == count)
            {
                count = 0;
                confirmed = 0;
                workParent.GetComponent<WorkWindowButton>().addWorkBar();
                transform.parent.GetComponent<Animator>().SetBool("close", true);
                gameObject.SetActive(false);
            }
        }
        else
        {
            count = 0;
            confirmed = 0;
            StartCoroutine(lose());
        }
        

    }

    IEnumerator lose()
    {
        foreach (Transform t in tf) 
        {
            t.GetComponent<RawImage>().color = wrong;
        }
        yield return new WaitForSeconds(1);
        workParent.GetComponent<WorkWindowButton>().subtractWorkBar();
        transform.parent.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
