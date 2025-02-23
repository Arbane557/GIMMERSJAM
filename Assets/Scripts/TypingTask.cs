using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

[System.Serializable]

public struct Command
{
    public string instruction;
    public bool confirmation;
}
public class TypingTask : MonoBehaviour
{   
    public List<Command> instructions = new List<Command>();
    public TextMeshProUGUI instructionDisplay;
    public TextMeshProUGUI timeText;
    public TMP_InputField instructionText;
    public GameObject workParent;
    private bool confirm;
    public Color red, normal;
    public float blinkDuration = 2f;
    public float blinkInterval = 0.2f;
    bool done;
    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(null);
        chooseRandomCommand();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !done)
        {
            done = true;
            Debug.Log("enter");
            confirmButton();
        }
    }

    void chooseRandomCommand()
    {
        instructionText.text = "";
        var com = instructions[Random.Range(0, instructions.Count)];
        instructionDisplay.text = com.instruction;
        confirm = com.confirmation;
        timeText.text = "-" + Random.Range(10, 25) + ":" + Random.Range(10, 61);
    }
    public void confirmButton() {

        string text = confirm ? "confirm" : "reject";
        if (string.Equals(instructionText.text, text, System.StringComparison.OrdinalIgnoreCase))
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
