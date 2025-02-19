using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MathTask : MonoBehaviour
{
    private int a, b;
    private string sum;
    public TextMeshProUGUI fieldInput;
    
    public void addNumber(string num)
    {
        sum += num;
    }
}
