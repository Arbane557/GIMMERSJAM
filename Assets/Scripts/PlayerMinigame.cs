using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMinigame : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject currentPlatform;
    private float maxHeight;
    public TextMeshProUGUI score;
    public float bounceForce;
    public Camera mainCamera;
    public float blinkDuration = 2f;
    public float blinkInterval = 0.2f;

    private void Start()
    {
        maxHeight = 0;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (transform.position.y > maxHeight) maxHeight = transform.position.y;
        score.text = "Score : " + Mathf.RoundToInt(maxHeight);
        float movex = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(movex * 3, rb.velocity.y);

        float loseYThreshold = mainCamera.transform.position.y - 8f;
        if (transform.position.y < loseYThreshold)
        {
            RevivePlayer();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform") && rb.velocity.y <= 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, bounceForce);
            currentPlatform = collision.gameObject;
        }
    }

    void RevivePlayer()
    {
        if (currentPlatform == null)
        {
            return;
        }
        transform.position = currentPlatform.transform.position + new Vector3(0, 1, 0);
        rb.velocity = Vector2.zero;
        StartCoroutine(BlinkEffect());
    }

    IEnumerator BlinkEffect()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr == null) yield break;
        float elapsed = 0f;
        while (elapsed < blinkDuration)
        {
            sr.enabled = !sr.enabled;
            yield return new WaitForSeconds(blinkInterval);
            elapsed += blinkInterval;
        }
        sr.enabled = true;
    }
}
