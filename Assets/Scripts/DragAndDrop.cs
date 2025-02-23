using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using TMPro;

public class DragAndDrop : MonoBehaviour
{
    private Vector2 offset;
    public Vector2 minBounds;
    public Vector2 maxBounds;
    private Animator anim;
    public List<GameObject> windows;
    private PlayerMinigame PM;
    private WorkWindowButton WB;
    public GameObject Corn;
    public GameObject CornUI;
    public GameObject Cow;
    public GameObject CowUI;
    public GameObject Scare;
    public GameObject ScareUI;
    public AudioManager Sound;

    private void Start()
    {
        Sound = GameObject.FindGameObjectWithTag("Sound").GetComponent<AudioManager>();

        if (gameObject.CompareTag("Corn"))
        {
            Corn = GameObject.FindGameObjectWithTag("Cornish");
            StartCoroutine(CornHub()); 
        }
        if (gameObject.CompareTag("Cow"))
        {
            Cow = GameObject.FindGameObjectWithTag("Cowish");
            StartCoroutine(CowHub());
        }
        if (gameObject.CompareTag("Scare"))
        {
            Scare = GameObject.FindGameObjectWithTag("Scarish");
            StartCoroutine(ScareHub());
        }
        anim = GetComponent<Animator>();
        if (GameObject.FindGameObjectWithTag("Player")!= null) 
            PM = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMinigame>();
        if (GameObject.FindGameObjectWithTag("Work")!= null)
            WB = GameObject.FindGameObjectWithTag("Work").GetComponent<WorkWindowButton>();
    }
    void HandleDragging()
    {
        Vector2 newPosition = mousepos() + offset;       
        transform.position = clampBorder(newPosition);
    }
    void OnMouseDown()
    {
        offset = (Vector2)transform.position - mousepos();
    }
    private void OnMouseDrag()
    {
        HandleDragging();
    } 
    Vector2 mousepos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    Vector2 clampBorder(Vector2 pos)
    {
        pos.x = Mathf.Clamp(pos.x, minBounds.x, maxBounds.x);
        pos.y = Mathf.Clamp(pos.y, minBounds.y, maxBounds.y);
        return pos;
    }
    public void closeButton()
    {
        if (windows.Count < 1)
        {
            anim.SetBool("close", true);
            Destroy(this.gameObject, 1f);
        }
        else
        {
            var win = windows[windows.Count - 1];
            win.SetActive(true);
            win.GetComponent<Animator>().SetBool("close", true);
            windows.RemoveAt(windows.Count - 1);
        }
    }
    public void Heal()
    {     
        if (PM.currHP < PM.maxHP) PM.currHP += 1;
        Sound.PlaySFX(6);
        Destroy(this.gameObject);
    }
    public void Protect()
    {
        StartCoroutine(WB.protectBuffOn());
        Sound.PlaySFX(8);
        Destroy(this.gameObject);
    }
    
    public IEnumerator CornHub()
    {
        yield return new WaitForSeconds(5);
        Sound.PlaySFX(1);
        CornUI.SetActive(false);
        Corn.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        Corn.transform.GetChild(0).gameObject.SetActive(false);
        Destroy(this.gameObject);
    }
    public IEnumerator CowHub()
    {
        yield return new WaitForSeconds(5);
        Sound.PlaySFX(2);
        CowUI.SetActive(false);
        Cow.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(7);
        Cow.transform.GetChild(0).gameObject.SetActive(false);
        Destroy(this.gameObject);
    }
    public IEnumerator ScareHub()
    {
        yield return new WaitForSeconds(5);
        Sound.PlaySFX(9);
        ScareUI.SetActive(false);
        Scare.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        Scare.transform.GetChild(0).gameObject.SetActive(false);
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        if (gameObject.CompareTag("Corn")) Corn.transform.GetChild(0).gameObject.SetActive(false);
        if (gameObject.CompareTag("Cow")) Cow.transform.GetChild(0).gameObject.SetActive(false);
        if (gameObject.CompareTag("Scare")) Scare.transform.GetChild(0).gameObject.SetActive(false);
    }
}
