using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MathTask : MonoBehaviour
{
    private int a, b;
    private string sum;
    private int sumNum;
    public TextMeshProUGUI fieldInput;
    public TextMeshProUGUI questionText;
    public GameObject workParent;
    public Color red, normal;
    public float blinkDuration = 2f;
    public float blinkInterval = 0.2f;
    bool done;
    private void OnEnable()
    {
        sum = "";
        a = Random.Range(10, 100);
        b = Random.Range(10, 100);
        questionText.text = "" + a + " + " + b;
        sumNum = a + b;
    }
    private void Update()
    {
        fieldInput.text = sum;
    }
    public void addNumber(string num)
    {
        sum += num; 
    }
    public void enter()
    {
        if (!done)
        {
            done = true;
            if (sum == sumNum.ToString())
            {
                Debug.Log("success");
                workParent.GetComponent<WorkWindowButton>().addWorkBar();
                transform.parent.GetComponent<Animator>().SetBool("close", true);
                done = false;
                gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("failed");
                workParent.GetComponent<WorkWindowButton>().subtractWorkBar();
                StartCoroutine(wrong());
            }
        }
    }
    public void removeNumber()
    {
        if (sum != "") sum = sum.Remove(sum.Length - 1, 1);
    }
    IEnumerator wrong()
    {
        float elapsed = 0f;
        while (elapsed < blinkDuration)
        {
            yield return new WaitForSeconds(blinkInterval);
            transform.GetChild(0).transform.GetComponent<RawImage>().color = red;
            yield return new WaitForSeconds(blinkInterval);
            transform.GetChild(0).transform.GetComponent<RawImage>().color = normal;
            elapsed += blinkInterval;
        }
        transform.parent.GetComponent<Animator>().SetBool("close", true);
        done = false;
        gameObject.SetActive(false);
    }
}
