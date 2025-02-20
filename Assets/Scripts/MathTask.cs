using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MathTask : MonoBehaviour
{
    private int a, b;
    private string sum;
    private int sumNum;
    public TextMeshProUGUI fieldInput;
    public TextMeshProUGUI questionText;

    private void OnEnable()
    {
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
        if (sum == sumNum.ToString()) {
            Debug.Log("success");
        } else Debug.Log("failed");

    }
    public void removeNumber()
    {
        sum = sum.Remove(sum.Length - 1, 1);
    }
}
