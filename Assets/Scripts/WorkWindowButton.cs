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
    bool on;
    private void Start()
    {
        workBarCurr = 0.1f * workBarMax;
        notifCount = 0;
        StartCoroutine(generateTask());
    }
    private void Update()
    {
        workBar.value = workBarCurr/workBarMax;
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
        else
        {
            WorkFinishedPrefab.SetActive(true);
        }
    }

    IEnumerator generateTask()
    {
        while (true)
        {          
            yield return new WaitForSeconds(10);
            notifCount++;
            notification.text = notifCount.ToString();
        }
    }
    public void addWorkBar()
    {
        workBarCurr += 0.05f * workBarMax;
    }
    public void subtractWorkBar()
    {
        workBarCurr -= 0.1f * workBarMax;
    }
}
