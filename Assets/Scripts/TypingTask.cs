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
    public WorkWindowButton wb;
    public TextMeshProUGUI instructionDisplay;
    public TMP_InputField instructionText;
    private bool confirm;
   
    private void OnEnable()
    {
        chooseRandomCommand();
    }
    void chooseRandomCommand()
    {
        var com = instructions[Random.Range(0, instructions.Count)];
        instructionDisplay.text = com.instruction;
        confirm = com.confirmation;
    }
    public void confirmButton() {
        string text = confirm ? "confirm" : "deny";
        if (string.Equals(instructionText.text, text, System.StringComparison.OrdinalIgnoreCase)){
            Debug.Log("success");
        }
        else Debug.Log("failed");
        //wb.openWorkWindow();
    }

}
