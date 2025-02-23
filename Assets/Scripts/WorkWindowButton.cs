using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WorkWindowButton : MonoBehaviour
{
    public GameObject WorkWindowPrefab;
    public GameObject WorkFinishedPrefab;
    public Animator anim;
    public Animator animFinish;
    public TextMeshProUGUI notification;
    private int notifCount;
    public Slider workBar;
    public float workBarCurr;
    public float workBarMax;
    public bool protectBuff;
    public GameObject protectSprite;
    public Color normalColor, protectColor;
    public float reducing, gain, penalty;
    bool on;
    public AudioManager Sound;
    private void Start()
    {
        workBarCurr = 0.4f * workBarMax;
        notifCount = 0;
        StartCoroutine(generateTask());
        Sound = GameObject.FindGameObjectWithTag("Sound").GetComponent<AudioManager>();
    }
    private void Update()
    {
        workBarCurr -=  (protectBuff ? 0 : reducing) * workBarMax * Time.deltaTime;
        protectSprite.SetActive(protectBuff);
        workBar.value = workBarCurr/workBarMax;
        if (workBarCurr < 0) workBarCurr = 0;
        on = !(notifCount == 0);
        notification.transform.parent.gameObject.SetActive(on);
    }
    public void openWorkWindow()
    {
        if (notifCount > 0 && WorkWindowPrefab.activeSelf == false)
        {
            if (WorkFinishedPrefab.activeSelf == true)
            {
                WorkFinishedPrefab.SetActive(false);
                animFinish.SetBool("close", true);
            }

            if (WorkWindowPrefab.activeSelf == false)
            {
                WorkWindowPrefab.SetActive(true);
            }
            notifCount--;
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
}
