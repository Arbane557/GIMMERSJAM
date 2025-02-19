using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkWindowHandler : MonoBehaviour
{
    private Vector2 offset;
    public Vector2 minBounds;
    public Vector2 maxBounds;
    void OnMouseDown()
    {
        offset = (Vector2)transform.position - mousepos();
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
