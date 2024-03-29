using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectBackEvent : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public UISprite instance;
    public int sign;
    public int coin;

    public void OnSelect(BaseEventData eventData)
    {
        if (eventData.selectedObject.GetComponent<Selectable>() == null)
        {
            return;
        }

        instance.Index = sign;
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

    public void Change()
    {
        if (PlayerPrefs.GetInt("BGState" + sign, 0) == 1)
        {
            SpriteManager.Instance.ChangeBG(GameController.instance.Backs[sign - 1]);
            PlayerPrefs.SetInt("CurBack", sign - 1);
            instance.SelectedItem = sign;
        }

        if (PlayerPrefs.GetInt("BGState" + sign, 0) == 0)
        {
            if (PlayerPrefs.GetInt("Coin") >= coin)
            {
                PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") - this.coin);
                SpriteManager.Instance.ChangeBG(GameController.instance.Backs[sign - 1]);
                PlayerPrefs.SetInt("CurBack", sign - 1);
                instance.SelectedItem = sign;
                instance.Coin.text = PlayerPrefs.GetInt("Coin").ToString();
                //购买状态
                PlayerPrefs.SetInt("BGState" + sign, 1);
                //购买信息隐藏
                CheckCoin();
            }
            else
            {
                instance.NotEnough.alpha = 1;
                Invoke(nameof(ShowTip), 0.5f);
            }
        }
        
    }

    void ShowTip()
    {
        instance.NotEnough.alpha = 0;
    }

    public void CheckCoin()
    {
        if (sign == 1)
            return;
        else
        {
            if (PlayerPrefs.GetInt("BGState" + sign, 0) == 1)
                transform.Find("Coin").gameObject.SetActive(false);
            else 
                transform.Find("Coin").gameObject.SetActive(true);
        }
    }
    
}