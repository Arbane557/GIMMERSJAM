using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoScroll : MonoBehaviour
{
    [Tooltip("The ScrollRect containing the content to scroll.")]
    public ScrollRect scrollRect;

    [Tooltip("Scroll speed in pixels per second.")]
    public float scrollSpeed = 50f;

    private RectTransform contentRect;
    private float contentHeight;
    private float viewportHeight;

    void Start()
    {
        if (scrollRect == null)
        {
            Debug.LogError("ScrollRect is not assigned.");
            return;
        }

        contentRect = scrollRect.content;
        // Make sure your Content's pivot is (0.5, 1) for top alignment.
        contentHeight = contentRect.rect.height;
        viewportHeight = scrollRect.viewport.rect.height;
    }

    void Update()
    {
        // Only scroll if the content is taller than the viewport.
        if (contentHeight <= viewportHeight)
            return;

        // Move the content upward by adjusting its anchoredPosition.
        float newY = contentRect.anchoredPosition.y + scrollSpeed * Time.deltaTime;

        // If we've scrolled past the end, reset to the beginning.
        if (newY >= contentHeight - viewportHeight)
        {
            newY = 0f;
        }

        contentRect.anchoredPosition = new Vector2(contentRect.anchoredPosition.x, newY);
    }
}
