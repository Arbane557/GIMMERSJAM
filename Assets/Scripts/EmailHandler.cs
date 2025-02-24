using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EmailHandler : MonoBehaviour
{
    public GameObject Email;
    public GameObject mailHolder;
    public Transform mailContainer;
    public TextMeshProUGUI notification;
    private int notifCount;
    bool haveMails;
    bool on;
    // Start is called before the first frame update
    void Start()
    {
        on = false;
    }
    private void Update()
    {
        haveMails = !(notifCount == 0);
        notification.transform.parent.gameObject.SetActive(haveMails);
    }
    public void showMails()
    {
        notifCount = 0;
        on = !on;
        if(on) Email.SetActive(on);
        else Email.GetComponent<Animator>().SetBool("close",true);
    }

    public void createMails(string from, string text)
    {
        var mail = Instantiate(mailHolder);
        mail.transform.parent = mailContainer;
        mail.GetComponent<EmailData>().writeEmail(from, text);
        notifCount++;
        notification.text = notifCount.ToString();
    }
}
