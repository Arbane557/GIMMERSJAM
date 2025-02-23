using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GachaSystem : MonoBehaviour
{
    public GameObject[] planets;
    public Button spinButton;
    public float spinDuration = 2f;
    public float spinInterval = 0.1f;
    private PlayerMinigame PM;
    public GameObject bigWin;
    public GameObject bigLoss;
    public AudioManager Sound;

    void Start()
    {
        Sound = GameObject.FindGameObjectWithTag("Sound").GetComponent<AudioManager>();
        if (GameObject.FindGameObjectWithTag("Player") != null)
            PM = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMinigame>();
        spinButton.onClick.AddListener(() => StartCoroutine(Spin()));
        HideAllPlanets();
    }

    IEnumerator Spin()
    {
        spinButton.interactable = false;
        float elapsed = 0f;
        int index = 0;
        HideAllPlanets();
        Sound.PlaySFX(5);
        while (elapsed < spinDuration)
        {
            HideAllPlanets();
            planets[index].SetActive(true);
            index = (index + 1) % planets.Length;
            yield return new WaitForSeconds(spinInterval);
            elapsed += spinInterval;
        }

        HideAllPlanets();

        float roll = Random.value; 
        if (roll < 0.5f)
        {
            planets[1].SetActive(true);
            if (PM.currHP < PM.maxHP) PM.currHP += 1;
            StartCoroutine(win());
        }
        else
        {
            if (Random.value < 0.5f)
            {
                planets[0].SetActive(true);
            }
            else
            {
                planets[2].SetActive(true);
            }
            PM.currHP -= 1;
            StartCoroutine(loss());
        }
       
        spinButton.interactable = true;
    }

    void HideAllPlanets()
    {
        foreach (var planet in planets)
        {
            planet.SetActive(false);
        }
    }

    IEnumerator win()
    {
        Instantiate(bigWin);
        yield return new WaitForSeconds(2);
        Destroy(transform.parent.gameObject);       
    }
    IEnumerator loss()
    {
        Instantiate(bigLoss);
        yield return new WaitForSeconds(2);
        Destroy(transform.parent.gameObject);
    }
}
