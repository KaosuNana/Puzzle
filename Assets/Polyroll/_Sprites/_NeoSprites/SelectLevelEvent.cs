using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectLevelEvent : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public UIMenu instance;
    public int sign;

    public void OnSelect(BaseEventData eventData)
    {
        if (eventData.selectedObject.GetComponent<Selectable>() == null)
        {
            return;
        }

        instance.CurLevel = sign;
        ShowFingerEvent(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        if (eventData.selectedObject.GetComponent<Selectable>() == null)
        {
            return;
        }

        ShowFingerEvent(false);
    }

    private void ShowFingerEvent(bool isShow)
    {
        this.transform.Find("Finger").gameObject.SetActive(isShow);
    }
}
