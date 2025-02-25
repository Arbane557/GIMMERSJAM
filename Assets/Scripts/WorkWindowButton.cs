using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WorkWindowButton : MonoBehaviour
{
    public GameObject WorkWindowPrefab;
    public GameObject WorkFinishedPrefab;
    public Animator anim;
    public Animator animFinish;
    public TextMeshProUGUI notification;
    private int notifCount;
    public Slider workBar;
    public GameObject workBarObj;
    public float workBarCurr;
    public float workBarMax;
    public bool protectBuff;
    public GameObject protectSprite;
    public Color normalColor, protectColor;
    public float reducing, gain, penalty;
    bool on;
    public AudioManager Sound;
    public PlayerMinigame player;
    private Coroutine work, loss;
    public GameObject windowSpawner;
    public EmailHandler emailHandler;
    public GameObject miniGameWindow;
    public GameObject gameoverScreen;
    public GameObject gameEssence;
    public GameObject win, lose;
    bool done;
    private void Start()
    {
        workBarCurr = 0.4f * workBarMax;
        notifCount = 0;
        work = StartCoroutine(generateTask());
        Sound = GameObject.FindGameObjectWithTag("Sound").GetComponent<AudioManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMinigame>();
        emailHandler = GameObject.FindGameObjectWithTag("Email").GetComponent<EmailHandler>();
    }
    private void Update()
    {
        workBarCurr -=  (protectBuff ? 0 : reducing) * workBarMax * Time.deltaTime;
        protectSprite.SetActive(protectBuff);
        workBar.value = workBarCurr/workBarMax;
        if (workBarCurr < 0) workBarCurr = 0;
        on = !(notifCount == 0);
        notification.transform.parent.gameObject.SetActive(on);
        if (workBarCurr <= 0 && loss == null)
        {
            loss = StartCoroutine(lossWork());
        }
        if (workBarCurr >= workBarMax && loss == null)
        {
            loss = StartCoroutine(winWork());
        }
    }
    public void openWorkWindow()
    {
        if (notifCount > 0 && WorkWindowPrefab.activeSelf == false)
        {
            notifCount--;
            if (WorkFinishedPrefab.activeSelf == true)
            {
                WorkFinishedPrefab.SetActive(false);
                animFinish.SetBool("close", true);
            }
            if (WorkWindowPrefab.activeSelf == false)
            {
                WorkWindowPrefab.SetActive(true);
            }
        }
        else if (WorkWindowPrefab.activeSelf == false)
        {
            WorkFinishedPrefab.SetActive(true);
        }
    }

    IEnumerator generateTask()
    {
        while (true)
        {          
            yield return new WaitForSeconds(10);
            Sound.PlaySFX(3);
            notifCount++;
            notification.text = notifCount.ToString();
        }
    }
    public void addWorkBar()
    {
        workBarCurr += gain * workBarMax;
    }
    public void subtractWorkBar()
    {
        workBarCurr -= penalty * workBarMax;
    }

    public IEnumerator protectBuffOn()
    {
        protectBuff = true;
        workBar.fillRect.GetComponent<RawImage>().color = protectColor;
        StartCoroutine(countdown());
        yield return null;
    }

    public IEnumerator countdown()
    {
        yield return new WaitForSeconds(5);
        workBar.fillRect.GetComponent<RawImage>().color = normalColor;
        protectBuff = false;
    }

    public IEnumerator lossWork()
    {
        Sound.PlaySFX(10);
        windowSpawner.SetActive(false);
        miniGameWindow.SetActive(false);
        StopCoroutine(work);
        emailHandler.showMails(); 
        yield return new WaitForSeconds(4);
        emailHandler.createMails("BOSS", "LOOKS LIKE YOU HAVE NEGLECTED YOUR WORK");
        yield return new WaitForSeconds(4);
        emailHandler.createMails("BOSS", "I'M TAKING OFF YOUR ACCESS FROM THIS COMPUTER!!");
        yield return new WaitForSeconds(5);
        gameEssence.SetActive(false);
        gameoverScreen.SetActive(true);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public IEnumerator winGameFired()
    {
        if (!done)
        {
            done = true;
            Sound.PlaySFX(10);
            emailHandler.showMails();
            windowSpawner.SetActive(false);
            StopCoroutine(work);
            yield return new WaitForSeconds(6);
            yield return new WaitForSeconds(4);
            emailHandler.createMails("BOSS", "FINISHED A GAME AT WORK??");
            yield return new WaitForSeconds(4);
            emailHandler.createMails("BOSS", "ARE YOU JOKING??");
            yield return new WaitForSeconds(4);
            emailHandler.createMails("BOSS", "YOU ARE FIRED!");
            yield return new WaitForSeconds(4);
            win.SetActive(true);
            yield return new WaitForSeconds(4);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }

    public IEnumerator winWork()
    {
        if (!done)
        {
            done = true;
            Sound.PlaySFX(10);
            emailHandler.showMails();
            windowSpawner.SetActive(false);
            miniGameWindow.SetActive(false);
            StopCoroutine(work);
            yield return new WaitForSeconds(4);
            emailHandler.createMails("BOSS", "WOW YOU COMPLETED ALL YOUR WORK");
            yield return new WaitForSeconds(4);
            emailHandler.createMails("BOSS", "AS EXPECTED FROM YOU!");
            yield return new WaitForSeconds(4);
            emailHandler.createMails("BOSS", "YOU WILL BE PROMOTED, CONGRATULATIONS!!");
            yield return new WaitForSeconds(4);
            lose.SetActive(true);
            yield return new WaitForSeconds(5);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
}
