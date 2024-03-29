using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIMenu : MonoBehaviour
{
    public GameController GC;

    public Transform content;
    public List<Button> Levels;
    private int curLevel;
    public int LeastLevel;
    public int CurLevel
    {
        get => curLevel;
        set
        {
            curLevel = value;
            LeastLevel = value;
            UpdatePosition(curLevel);
        }
    }

    private int _page = 1;

    public int Page
    {
        get => _page;
        set
        {
            _page = value;
            if(_page == 1)
                content.DOLocalMoveX(760, 0.5f);         
            if(_page == 2)
                content.DOLocalMoveX(-740, 0.5f);           
            if(_page == 3)
                content.DOLocalMoveX(-2240, 0.5f);         
            if(_page == 4)
                content.DOLocalMoveX(-3740, 0.5f);         
            if(_page == 5)
                content.DOLocalMoveX(-5240, 0.5f);     
            if(_page == 6)
                content.DOLocalMoveX(-6740, 0.5f);           
            if(_page == 7)
                content.DOLocalMoveX(-8240, 0.5f);     
            if(_page == 8)
                content.DOLocalMoveX(-9740, 0.5f);
            
        }
    }
    // Update is called once per frame
    // void Update()
    // {
    //     if (GC._mode == GameController.GameMode.Menu)
    //     {
    //         content.DOLocalMoveX()
    //     }
    // }

    void UpdatePosition(int level)
    {
        switch (level)
        {
            case 0 :
                if (_page != 1) _page = 1;
                content.DOLocalMoveX(760, 0.5f);
                break;
            case 48 : 
                if (_page != 1) _page = 1;
                content.DOLocalMoveX(760, 0.5f);
                break;  
            case 5 : 
                if (_page != 1) _page = 1;
                content.DOLocalMoveX(760, 0.5f);
                break;
            case 53 : 
                if (_page != 1) _page = 1;
                content.DOLocalMoveX(760, 0.5f);
                break;     
            case 6 : 
                if (_page != 2) _page = 2;
                content.DOLocalMoveX(-740, 0.5f);
                break;
            case 54 : 
                if (_page != 2) _page = 2;
                content.DOLocalMoveX(-740, 0.5f);
                break; 
            case 11 : 
                if (_page != 2) _page = 2;
                content.DOLocalMoveX(-740, 0.5f);
                break;
            case 59 : 
                if (_page != 2) _page = 2;
                content.DOLocalMoveX(-740, 0.5f);
                break; 
            case 12 : 
                if (_page != 3) _page = 3;
                content.DOLocalMoveX(-2240, 0.5f);
                break;     
            case 60 : 
                if (_page != 3) _page = 3;
                content.DOLocalMoveX(-2240, 0.5f);
                break;    
            case 17 : 
                if (_page != 3) _page = 3;
                content.DOLocalMoveX(-2240, 0.5f);
                break;     
            case 65 : 
                if (_page != 3) _page = 3;
                content.DOLocalMoveX(-2240, 0.5f);
                break;           
            case 18 : 
                if (_page != 4) _page = 4;
                content.DOLocalMoveX(-3740, 0.5f);
                break;      
            case 66 : 
                if (_page != 4) _page = 4;
                content.DOLocalMoveX(-3740, 0.5f);
                break; 
            case 23 : 
                if (_page != 4) _page = 4;
                content.DOLocalMoveX(-3740, 0.5f);
                break;      
            case 71 : 
                if (_page != 4) _page = 4;
                content.DOLocalMoveX(-3740, 0.5f);
                break;         
            case 24 : 
                if (_page != 5) _page = 5;
                content.DOLocalMoveX(-5240, 0.5f);
                break;     
            case 72 : 
                if (_page != 5) _page = 5;
                content.DOLocalMoveX(-5240, 0.5f);
                break;       
            case 29 : 
                if (_page != 5) _page = 5;
                content.DOLocalMoveX(-5240, 0.5f);
                break;     
            case 77 : 
                if (_page != 5) _page = 5;
                content.DOLocalMoveX(-5240, 0.5f);
                break;          
            case 30 : 
                if (_page != 6) _page = 6;
                content.DOLocalMoveX(-6740, 0.5f);
                break;   
            case 78 : 
                if (_page != 6) _page = 6;
                content.DOLocalMoveX(-6740, 0.5f);
                break;      
            case 35 : 
                if (_page != 6) _page = 6;
                content.DOLocalMoveX(-6740, 0.5f);
                break;   
            case 83 : 
                if (_page != 6) _page = 6;
                content.DOLocalMoveX(-6740, 0.5f);
                break;          
            case 36 : 
                if (_page != 7) _page = 7;
                content.DOLocalMoveX(-8240, 0.5f);
                break;  
            case 84 : 
                if (_page != 7) _page = 7;
                content.DOLocalMoveX(-8240, 0.5f);
                break;   
            case 41 : 
                if (_page != 7) _page = 7;
                content.DOLocalMoveX(-8240, 0.5f);
                break;  
            case 89 : 
                if (_page != 7) _page = 7;
                content.DOLocalMoveX(-8240, 0.5f);
                break;
            case 42:
                if (_page != 8) _page = 8;
                content.DOLocalMoveX(-9740, 0.5f);
                break;    
            case 90:
                if (_page != 8) _page = 8;
                content.DOLocalMoveX(-9740, 0.5f);
                break;
        }
    }

    public void InitLevel()
    {
        //Debug.Log(EventSystem.current.currentSelectedGameObject);
        GameController.instance.MaskImg.gameObject.SetActive(true);
        if (Levels.Count > 0)
        {
            if(LeastLevel >= 90 || LeastLevel >= 42)
                UpdatePosition(42);         
            if((LeastLevel >= 84 && LeastLevel <= 89) || (LeastLevel >= 36 && LeastLevel <= 41))
                UpdatePosition(36);               
            if((LeastLevel >= 30 && LeastLevel <= 35) || (LeastLevel >= 78 && LeastLevel <= 83))
                UpdatePosition(30);              
            if((LeastLevel >= 72 && LeastLevel <= 77) || (LeastLevel >= 24 && LeastLevel <= 29))
                UpdatePosition(24);            
            if((LeastLevel >= 66 && LeastLevel <= 71) || (LeastLevel >= 18 && LeastLevel <= 23))
                UpdatePosition(18);               
            if((LeastLevel >= 60 && LeastLevel <= 65) || (LeastLevel >= 12 && LeastLevel <= 17))
                UpdatePosition(12);              
            if((LeastLevel >= 54 && LeastLevel <= 59) || (LeastLevel >= 6 && LeastLevel <= 11))
                UpdatePosition(6);            
            if((LeastLevel >= 0 && LeastLevel <= 5) || (LeastLevel >= 48 && LeastLevel <= 53))
                UpdatePosition(0);           
 
            StartCoroutine(SetFocus(content.GetChild(LeastLevel).GetComponent<Button>()));
            return;
        }
        for (int i = 0; i < 95; i++)
        {
            Levels.Add(content.GetChild(i).GetComponent<Button>());
            content.GetChild(i).gameObject.AddComponent<SelectLevelEvent>();
            content.GetChild(i).GetComponent<SelectLevelEvent>().instance = this;
            content.GetChild(i).GetComponent<SelectLevelEvent>().sign = i;
        }

        for (int i = 0; i < Levels.Count; i++)
        {
            Levels[i].navigation = new Navigation()
            {
                mode = Navigation.Mode.Explicit,
                selectOnUp = i < 48 ? null : Levels[i - 48],
                selectOnDown = i > 47 ? null : i == 47 ? null : Levels[i + 48],
                selectOnLeft = i == 0 || i == 48 ? Levels[i] : Levels[i - 1],
                selectOnRight = i == 47 || i == 94 ? Levels[i] : Levels[i + 1]
            };
        }
        Left.onClick.AddListener(()=>
        {
            if(_page == 1)
                return;
            Page--;
        });      
        Right.onClick.AddListener(()=>
        {
            if(_page == 8)
                return;
            Page++;
        });
        StartCoroutine(SetFocus(content.GetChild(0).GetComponent<Button>()));
    }

    public Button Left;
    public Button Right;
    IEnumerator SetFocus(Button btn)
    {
        yield return new WaitForSeconds(0.5f);
        GameController.instance._mode = GameController.instance.MaskImg2.gameObject.activeSelf ? GameController.GameMode.None : GameController.GameMode.Menu;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(btn.gameObject);
        GameController.instance.MaskImg.gameObject.SetActive(false);
    }
}

