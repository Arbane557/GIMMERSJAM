using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using TMPro;

public class PlayerMinigame : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject currentPlatform;
    private float maxHeight;
    public TextMeshProUGUI score;
    private void Start()
    {
        maxHeight = 0;
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if(transform.position.y > maxHeight) maxHeight = transform.position.y;
        score.text = "Score : " + Mathf.RoundToInt(maxHeight);
        if (GetCurrentPlatform() != null) currentPlatform = GetCurrentPlatform().gameObject;
        float movex = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(movex * 3, rb.velocity.y);
        if (transform.position.y < transform.position.y - 10) transform.position = currentPlatform.transform.position + new Vector3(0,1,0);
    }

    Collider2D GetCurrentPlatform()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1f);

        foreach (Collider2D col in colliders)
        {
            if (col.gameObject != this.gameObject && col.CompareTag("Platform"))
            {
                return col;
            }
        }
        return null;
    }
}
