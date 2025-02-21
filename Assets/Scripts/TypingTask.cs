using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
   
    private void OnEnable()
    {
        chooseRandomCommand();
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
        }
        else 
        { 
            Debug.Log("failed"); 
            workParent.GetComponent<WorkWindowButton>().subtractWorkBar(); 
        }
        transform.parent.GetComponent<Animator>().SetBool("close", true);
        gameObject.SetActive(false);
        
    }

}
