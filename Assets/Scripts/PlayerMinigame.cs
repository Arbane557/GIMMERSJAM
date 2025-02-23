using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
    public GameObject[] hearts;
    public int currHP;
    public int maxHP;
    public GameObject gameOverText;
    public GameObject gameEssence;
    public GameObject gameWindow;
    public GameObject gameoverScreen;
    bool gameover;
    public float duration = 2.0f;
    public AudioManager Sound;

    private void Start()
    {
        currHP = maxHP; 
        maxHeight = 0;
        rb = GetComponent<Rigidbody2D>();
        Sound = GameObject.FindGameObjectWithTag("Sound").GetComponent<AudioManager>();

    }

    private void Update()
    {
        if (!gameover)
        {
            if (transform.position.y > maxHeight) maxHeight = transform.position.y;
            score.text = "Score : " + Mathf.RoundToInt(maxHeight);
            float movex = Input.GetAxisRaw("Horizontal");
            transform.rotation = (movex >= 0 ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0));
            rb.velocity = new Vector2(movex * 3, rb.velocity.y);

            if (currHP < 1)
            {
                mainCamera.GetComponent<CameraFollow>().enabled = false;
                mainCamera.transform.position = new Vector3(0, 0, 0);
                gameOverText.SetActive(true);
                GetComponent<SpriteRenderer>().enabled = false;
                gameover = true;
            }

            float loseYThreshold = mainCamera.transform.position.y - 8f;
            if (transform.position.y < loseYThreshold)
            {
                RevivePlayer();
            }
            for (int i = 0; i < hearts.Length; i++)
            {
                hearts[i].SetActive(i < currHP);
            }
        }
        else
        {
            StartCoroutine(lose());
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform") && rb.velocity.y <= 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, bounceForce);
            currentPlatform = collision.gameObject;
            if (collision.gameObject == currentPlatform)
            {
                currentPlatform.transform.localScale = new Vector2(currentPlatform.transform.localScale.x - 0.15f, currentPlatform.transform.localScale.y);
                if (currentPlatform.transform.localScale.x <= 0)
                {
                    currentPlatform.GetComponent<Collider2D>().enabled = false;
                    currentPlatform.GetComponent<SpriteRenderer>().enabled = false;
                }
            }
        }
    }

    void RevivePlayer()
    {
        currHP--;
        if (currentPlatform == null)
        {
            return;
        }
        currentPlatform.transform.localScale = new Vector2(0.75f, currentPlatform.transform.localScale.y);
        currentPlatform.GetComponent<Collider2D>().enabled = true;
        currentPlatform.GetComponent<SpriteRenderer>().enabled = true;
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

    IEnumerator lose()
    {
        gameEssence.SetActive(false);
        Sound.gameObject.SetActive(false);
        yield return new WaitForSeconds(2);

        Vector3 startPosition = gameWindow.transform.position;
        Vector3 targetPosition = Vector3.zero;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            gameWindow.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        gameWindow.transform.position = targetPosition;
        yield return new WaitForSeconds(2);
        gameWindow.SetActive(false);
        gameoverScreen.SetActive(true);
        yield return new WaitForSeconds(7);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

    }
}
