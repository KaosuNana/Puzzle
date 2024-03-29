using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : Singleton<SpriteManager>
{
    private Sprite _curBG;

    public Sprite CurBG
    {
        get => _curBG;
    }
    public void ChangeBG(Sprite sprite)
    {
        _curBG = sprite;
    }
}
