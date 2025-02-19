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
    public GameObject notification;
    private TextMeshProUGUI counter;
    private void Start()
    {
        counter = notification.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (windows.Count > 0) 
        { 
            notification.SetActive(true);
            counter.text = "" + windows.Count;
        }
        else notification.SetActive(false);

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
    private void OnMouseUp()
    {
        //Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1f);

        //foreach (Collider2D col in colliders)
        //{
        //    if (col.gameObject != this.gameObject && col.CompareTag("Window") && windows.Count == 0)
        //    {
        //        col.gameObject.GetComponent<DragAndDrop>().windows.Add(this.gameObject);
        //        gameObject.SetActive(false);
        //        break;
        //    }
        //}
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
}
