using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WorkWindowButton : MonoBehaviour
{
    public GameObject WorkWindowPrefab;
    public Animator anim;
    public TextMeshProUGUI notification;
    private int notifCount;
    bool on;
    private void Start()
    {
        notifCount = 0;
        StartCoroutine(generateTask());
    }
    private void Update()
    {
        on = !(notifCount == 0);
        notification.transform.parent.gameObject.SetActive(on);
    }
    public void openWorkWindow()
    {
        if (WorkWindowPrefab.activeSelf == false)
        {
            WorkWindowPrefab.SetActive(true);
            GetComponent<Collider2D>().enabled = true;
        }
        else if (WorkWindowPrefab.activeSelf == true)
        {
            anim.SetBool("close", true);
            GetComponent<Collider2D>().enabled = false;
        }
    }

    IEnumerator generateTask()
    {
        while (true)
        {
            notifCount++;
            notification.text = notifCount.ToString();
            yield return new WaitForSeconds(20);
        }
    }
}
