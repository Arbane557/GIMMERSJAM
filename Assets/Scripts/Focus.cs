using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using TMPro;
public class Focus : MonoBehaviour
{
    public TMP_InputField input;
    void Start()
    {
        FocusOnInput();
    }

    public void FocusOnInput()
    {
        input.ActivateInputField();
        input.Select();
    }
}
