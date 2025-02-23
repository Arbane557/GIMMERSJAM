using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
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
                StartCoroutine(StartGame());
            }
            else if (string.Equals(input.text, "quit", System.StringComparison.OrdinalIgnoreCase))
            {
                End.SetActive(true);
                animator.SetBool("end", true);
                StartCoroutine(QuitGame());
            }
            else
            {
                input.text = "";
            }
        }
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    IEnumerator QuitGame()
    {
        yield return new WaitForSeconds(3);
        Application.Quit();
    }
   
}
