using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class MainMenuCo : MonoBehaviour
{
    public TMP_InputField input;
    public GameObject StartObj;
    public GameObject End;
    public Animator animator;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (string.Equals(input.text, "enter", System.StringComparison.OrdinalIgnoreCase))
            {
                StartObj.SetActive(true);
                animator.SetBool("start", true);
            }
            else if (string.Equals(input.text, "quit", System.StringComparison.OrdinalIgnoreCase))
            {
                End.SetActive(true);
                animator.SetBool("end", true);
            }
            else
            {
                input.text = "";
            }
        }
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

}
