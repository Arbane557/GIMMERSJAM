using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCloseDeactivate : MonoBehaviour
{
    public void OnCloseAnimationComplete()
    {
        gameObject.SetActive(false);
    }
}
