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
    private void Start()
    {
        if (gameObject.CompareTag("Corn"))
        {
            Corn = GameObject.FindGameObjectWithTag("Cornish");
            StartCoroutine(CornHub()); 
        }
        anim = GetComponent<Animator>();
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
        PM = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMinigame>();
        if (PM.currHP < PM.maxHP) PM.currHP += 1;
    }

    public void Protect()
    {
        WB = GameObject.FindGameObjectWithTag("Work").GetComponent<WorkWindowButton>();
        StartCoroutine(WB.protectBuffOn());
    }

    public IEnumerator CornHub()
    {
        yield return new WaitForSeconds(5);
        CornUI.SetActive(false);
        Corn.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        Corn.transform.GetChild(0).gameObject.SetActive(false);
        Destroy(this.gameObject);
    }
}
