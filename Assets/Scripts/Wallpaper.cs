using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallpaper : MonoBehaviour
{
    public Sprite[] wallpapers;
    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = wallpapers[Random.Range(0, wallpapers.Length)];
    }
}
