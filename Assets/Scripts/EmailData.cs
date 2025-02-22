using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EmailData : MonoBehaviour
{
    public TextMeshProUGUI fromText;
    public TextMeshProUGUI toText;

    public void writeEmail(string from, string mail)
    {
        fromText.text = from;
        toText.text = mail;
    }
}
