using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FileDrag : MonoBehaviour
{
    private Vector2 offset;
    public Vector2 minBounds;
    public Vector2 maxBounds;
    public TextMeshProUGUI textName;

    private void OnEnable()
    {
    }
    void OnMouseDown()
    {
        Debug.Log("Dragged");
        offset = (Vector2)transform.position - mousepos();
    }
    private void Update()
    {
        
        Vector3 parentPos = transform.parent.position;
        minBounds = new Vector2(parentPos.x - 2.5f, parentPos.y);
        maxBounds = new Vector2(parentPos.x + 2.5f, parentPos.y);
    }
    private void OnMouseDrag()
    {
        HandleDragging();
    }
    void HandleDragging()
    {
        Vector2 newPosition = mousepos() + offset;
        transform.position = clampBorder(newPosition);
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
}
