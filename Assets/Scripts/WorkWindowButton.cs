using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkWindowButton : MonoBehaviour
{
    public GameObject WorkWindowPrefab;
    public Animator anim;
    
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

}
