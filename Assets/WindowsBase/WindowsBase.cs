using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[RequireComponent(typeof(CanvasGroup))]
public class WindowsBase : MonoBehaviour, IPointerClickHandler
{
    public WindowsDefolt windowsDefolt = new WindowsDefolt();
    public WindowsSetting windowsSetting = new WindowsSetting();
    public WindowsEvents windowsEvents = new WindowsEvents();

    private void Start()
    {
        if (windowsSetting.HideOnStart)
        {
            HideWindows();
        }
    }
    public void SetupWindows()
    {
        Image solidImage = GetComponent<Image>();
        if (solidImage)
        {
            solidImage.color = windowsDefolt.SolidColor;
        }
        windowsDefolt.canvasGroup = GetComponent<CanvasGroup>();
        if (!windowsDefolt.canvasGroup)
        {
            windowsDefolt.canvasGroup = GetComponent<CanvasGroup>();
        }
        RectTransform rt = transform as RectTransform;

        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;        
        rt.sizeDelta = Vector2.zero;
        rt.pivot = new Vector2(0.5f,0.5f);

    }
    public void ShowWindows()
    {
        if (!windowsSetting.smooth)
        {
            windowsDefolt.canvasGroup.alpha = 1f;
            windowsDefolt.canvasGroup.interactable = true;
            windowsDefolt.canvasGroup.blocksRaycasts = true;
            windowsEvents.OnShow.Invoke();
        }
        else
        {
            StartCoroutine(ShowSmooth());
        }

    }
    public void ShowWindows(bool IsSmooth)
    {
        if (!IsSmooth)
        {
            windowsDefolt.canvasGroup.alpha = 1f;
            windowsDefolt.canvasGroup.interactable = true;
            windowsDefolt.canvasGroup.blocksRaycasts = true;
            windowsEvents.OnShow.Invoke();
        }
        else
        {
            StartCoroutine(ShowSmooth());
        }
    }
    public void HideWindows()
    {
        if (!windowsSetting.smooth)
        {
            windowsDefolt.canvasGroup.alpha = 0;
            windowsDefolt.canvasGroup.interactable = false;
            windowsDefolt.canvasGroup.blocksRaycasts = false;
            windowsEvents.OnHide.Invoke();
        }
        else
        {
            StartCoroutine(HideSmooth());
        }

    }
    public void HideWindows(bool isSmooth)
    {
        if (!isSmooth)
        {
            windowsDefolt.canvasGroup.alpha = 0;
            windowsDefolt.canvasGroup.interactable = false;
            windowsDefolt.canvasGroup.blocksRaycasts = false;
            windowsEvents.OnHide.Invoke();
        }
        else
        {
            StartCoroutine(HideSmooth());
        }

    }

    IEnumerator ShowSmooth()
    {
        while (windowsDefolt.canvasGroup.alpha < 1)
        {
            windowsDefolt.canvasGroup.alpha += 1f * Time.deltaTime / windowsSetting.smoothTime;
            yield return name;
        }
        windowsDefolt.canvasGroup.alpha = 1f;
        windowsDefolt.canvasGroup.interactable = true;
        windowsDefolt.canvasGroup.blocksRaycasts = true;
        windowsEvents.OnShow.Invoke();
    }
    IEnumerator HideSmooth()
    {
        while (windowsDefolt.canvasGroup.alpha > 0)
        {
            windowsDefolt.canvasGroup.alpha -= 1f * Time.deltaTime / windowsSetting.smoothTime;
            yield return null;
        }
        windowsDefolt.canvasGroup.alpha = 0;
        windowsDefolt.canvasGroup.interactable = false;
        windowsDefolt.canvasGroup.blocksRaycasts = false;
        windowsEvents.OnHide.Invoke();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Image solidImage = GetComponent<Image>();
        if (solidImage && windowsSetting.HideToClick)
        {
            HideWindows();
        }
    }
}
[System.Serializable]
public class WindowsDefolt
{
    public Color SolidColor = new Color(0, 0, 0, 0.6f);
    public string windowsName = "Windows Name";
    public CanvasGroup canvasGroup;
}

[System.Serializable]
public class WindowsSetting
{
    public bool HideOnStart;
    public bool smooth;
    [Range(0.1f, 2f)]
    public float smoothTime = 0.5f;
    public bool HideToClick;
}


[System.Serializable]
public class WindowsEvents
{
    public UnityEvent OnShow;
    public UnityEvent OnHide;
}