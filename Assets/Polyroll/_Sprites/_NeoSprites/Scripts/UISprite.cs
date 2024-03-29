using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISprite : MonoBehaviour
{
    public CanvasGroup NotEnough;
    public TextMeshProUGUI Coin;
    private int index = 1;

    public int Index
    {
        get => index;
        set
        {
            if (value >= index)
            {
                index = value;
                Turn(value);
            }

            if (value <= index)
            {
                index = value;
                Turn(value);
            }
        }
    }

    public int SelectedItem
    {
        set { this.gameObject.GetComponent<Image>().sprite = SpriteManager.Instance.CurBG; }
    }

    public GameObject[] Groups;

    public int NOW;
    public void SetActive()
    {
        GameController.instance.MaskImg.gameObject.SetActive(true);
        Coin.text = PlayerPrefs.GetInt("Coin").ToString();
        this.gameObject.GetComponent<Image>().sprite = SpriteManager.Instance.CurBG;
        index = 1;
        Page = 1;
        for (int i = 0; i < Groups.Length; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Groups[i].transform.GetChild(j).gameObject.GetComponent<SelectBackEvent>().CheckCoin();
            }
        }

        StartCoroutine(SetFocus(Groups[0].transform.GetChild(0).GetComponent<Button>()));
    }

    public void Turn(int index)
    {
        if (index <= 3)
        {
            Groups[0].transform.localPosition = new Vector3(0, 0, 0);
            Groups[1].transform.localPosition = new Vector3(-1200, 0, 0);
            Groups[2].transform.localPosition = new Vector3(1200, 0, 0);
        }

        if (index >= 4 && index <= 6)
        {
            Groups[1].transform.localPosition = new Vector3(0, 0, 0);
            Groups[0].transform.localPosition = new Vector3(-1200, 0, 0);
            Groups[2].transform.localPosition = new Vector3(1200, 0, 0);
        }    
        
        if (index >= 7 && index <= 9)
        {
            Groups[2].transform.localPosition = new Vector3(0, 0, 0);
            Groups[1].transform.localPosition = new Vector3(-1200, 0, 0);
            Groups[0].transform.localPosition = new Vector3(1200, 0, 0);
        }

        Groups[0].GetComponent<CanvasGroup>().alpha = index <= 3 ? 1 : 0;
        if (index >= 4 && index <= 6)
            Groups[1].GetComponent<CanvasGroup>().alpha = 1;
        else
            Groups[1].GetComponent<CanvasGroup>().alpha = 0;
        if (index >= 7 && index <= 9)
            Groups[2].GetComponent<CanvasGroup>().alpha = 1;
        else
            Groups[2].GetComponent<CanvasGroup>().alpha = 0;
        if (Groups[0].GetComponent<CanvasGroup>().alpha == 1)
        {
            _page = 1;
            Left.gameObject.SetActive(false);
            Right.gameObject.SetActive(true);
        }

        if (Groups[1].GetComponent<CanvasGroup>().alpha == 1)
        {
            _page = 2;
            Left.gameObject.SetActive(true);
            Right.gameObject.SetActive(true);
        }

        if (Groups[2].GetComponent<CanvasGroup>().alpha == 1)
        {
            _page = 3;
            Left.gameObject.SetActive(true);
            Right.gameObject.SetActive(false);
        }
    }

    private int _page = 1;

    public int Page
    {
        get => _page;
        set
        {
            if (value == 0)
                return;
            if (value == 4)
                return;
            if (value == 1)
            {
                Groups[0].transform.localPosition = new Vector3(0, 0, 0);
                Groups[1].transform.localPosition = new Vector3(-1200, 0, 0);
                Groups[2].transform.localPosition = new Vector3(1200, 0, 0);
            }

            if (value == 2)
            {
                Groups[1].transform.localPosition = new Vector3(0, 0, 0);
                Groups[0].transform.localPosition = new Vector3(-1200, 0, 0);
                Groups[2].transform.localPosition = new Vector3(1200, 0, 0);
            }    
        
            if (value == 3)
            {
                Groups[2].transform.localPosition = new Vector3(0, 0, 0);
                Groups[1].transform.localPosition = new Vector3(-1200, 0, 0);
                Groups[0].transform.localPosition = new Vector3(1200, 0, 0);
            }
            if (value == 1)
            {
                Groups[0].GetComponent<CanvasGroup>().alpha = 1;
                Groups[1].GetComponent<CanvasGroup>().alpha = 0;
                Groups[2].GetComponent<CanvasGroup>().alpha = 0;
                Left.gameObject.SetActive(false);
                Right.gameObject.SetActive(true);
            }

            if (value == 2)
            {
                Groups[1].GetComponent<CanvasGroup>().alpha = 1;
                Groups[0].GetComponent<CanvasGroup>().alpha = 0;
                Groups[2].GetComponent<CanvasGroup>().alpha = 0;
                Left.gameObject.SetActive(true);
                Right.gameObject.SetActive(true);
            }

            if (value == 3)
            {
                Groups[2].GetComponent<CanvasGroup>().alpha = 1;
                Groups[1].GetComponent<CanvasGroup>().alpha = 0;
                Groups[0].GetComponent<CanvasGroup>().alpha = 0;
                Left.gameObject.SetActive(true);
                Right.gameObject.SetActive(false);
            }

            _page = value;
        }
    }

    IEnumerator SetFocus(Button btn)
    {
        yield return new WaitForSeconds(0.5f);
        GameController.instance._mode = GameController.GameMode.Back;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(btn.gameObject);
        GameController.instance.MaskImg.gameObject.SetActive(false);
    }
    
    // void Update()
    // {
    //     if (GameController.instance._mode == GameController.GameMode.Back)
    //     {
    //         if (Page == 3)
    //         {
    //             Left.gameObject.SetActive(true);
    //             Right.gameObject.SetActive(false);
    //         }
    //
    //         if (Page == 1)
    //         {
    //             Left.gameObject.SetActive(false);
    //             Right.gameObject.SetActive(true);
    //         }
    //
    //         if (Page == 2)
    //         {
    //             Left.gameObject.SetActive(true);
    //             Right.gameObject.SetActive(true);
    //         }
    //     }
    // }

    public Button Left;
    public Button Right;

    public void TurnLeft()
    {
        Page--;
        NOW = Page;
    }

    public void TurnRight()
    {
        Page++;
        NOW = Page;
    }
}