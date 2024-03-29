using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header(" Debug ")] public bool deletePrefs;

    [Header(" Canvas Groups ")] public CanvasGroup MAIN;
    public CanvasGroup BACK;
    public CanvasGroup MENU;
    public CanvasGroup GAME;
    public CanvasGroup SETTINGS;
    public CanvasGroup QUIT;
    public Image fadeImage;
    public float fadeSpeed;
    CanvasGroup[] canvases;

    [Header(" Game Buttons ")] public GameObject backButton;
    public GameObject nextButton;
    public GameObject GameClear;

    [Header(" Grid Container ")] public Transform gridContainer;
    public static int activeImage;

    [Header(" Sprites ")] public Sprite[] sprites;

    [Header(" Managers ")] public RotationCenter rotationCenter;
    public AdMobManager adMobManager;
    public UnityAdsManager unityAdsManager;

    [Header(" Effects ")] public RectTransform uiParticlesParent;

    [Header(" Sounds ")] public AudioSource openImageSound;
    public AudioSource imageFinishedGame;
    public AudioSource imageFinishedMenu;

    [Header(" Ads ")] public GameObject noAdsButton;

    [Header(" Btns ")] public Button startGame;
    public Button tipBtn;
    public Button backSprite;

    public enum GameMode
    {
        Main = 1,
        Back = 2,
        Menu = 3,
        Game = 4,
        Quit = 5,
        None = 6,
    }

    public Sprite[] Backs;

//dp[i]=max(dp[j])+1
    public GameMode _mode;

    public static GameController instance;

    // Use this for initialization
    private void Awake()
    {
        instance = this;
        if (PlayerPrefs.GetInt("CurBack") == 0)
        {
            SpriteManager.Instance.ChangeBG(Backs[0]);
        }
        else
        {
            SpriteManager.Instance.ChangeBG(Backs[PlayerPrefs.GetInt("CurBack")]);
        }

        MAIN.gameObject.GetComponent<Image>().sprite = SpriteManager.Instance.CurBG;
    }

    void Start()
    {
        startGame.onClick.AddListener(() =>
        {
            // StartCoroutine(ShowCanvasGroupCoroutine(GameMode.Menu, MENU));
            ShowCanvasGroupCoroutines(GameMode.Menu, MENU);
            // _mode = GameMode.Menu;
            MENU.GetComponent<Image>().sprite = SpriteManager.Instance.CurBG;
            MENU.GetComponent<UIMenu>().InitLevel();
        });
        backSprite.onClick.AddListener(() =>
        {
            ShowCanvasGroupCoroutines(GameMode.Back, BACK);
            // _mode = GameMode.Back;
            BACK.GetComponent<Image>().sprite = SpriteManager.Instance.CurBG;
            BACK.GetComponent<UISprite>().SetActive();
        });
        int noAds = PlayerPrefs.GetInt("NOADS");

        if (noAds == 1)
        {
            noAdsButton.SetActive(false);
        }

        // Enable the fade panel
        fadeImage.gameObject.SetActive(true);

        // Store the canvases into an array
        canvases = new CanvasGroup[] { MENU, GAME, SETTINGS, MAIN, BACK, QUIT };
        SetGrid();

        // Add the listeners to the grid buttons
        AddListeners();

        tipBtn.onClick.AddListener(() =>
        {
            tipBtn.gameObject.SetActive(false);
            PlayerPrefs.SetInt("Tip", PlayerPrefs.GetInt("Tip", 0) + 1);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(startGame.gameObject);
        });

        if (Screen.width == 1920 && Screen.height == 1200)
        {
            tipBtn.gameObject.SetActive(false);
            StartCoroutine(SetFocus(startGame));
        }
        else
        {
            if (PlayerPrefs.GetInt("Tip", 0) < 3)
            {
                tipBtn.gameObject.SetActive(true);
                StartCoroutine(SetFocus(tipBtn));
            }
            else
            {
                tipBtn.gameObject.SetActive(false);
                StartCoroutine(SetFocus(startGame));
            }
        }

        _mode = GameMode.Main;
    }

    private Vector3 _startPosition;
    private Vector3 _nowPosition;
    private float xMoveDis;
    private int backvalue = 0;
    private Vector3 _MousestartPosition;
    private const float SwipeThreshold = 200f;

    public GameObject GOEXIT;

    private bool isExisting;

    // Update is called once per frame
    void Update()
    {
        if (deletePrefs)
            PlayerPrefs.DeleteAll();
        if (_mode == GameMode.Back)
        {
            if (Input.GetButtonDown("Cancel"))
            {
                StartCoroutine(ShowCanvasGroupCoroutine(GameMode.Main, MAIN));
                MAIN.GetComponent<Image>().sprite = SpriteManager.Instance.CurBG;
                StartCoroutine(SetFocus(startGame));
            }
        }

        if (_mode == GameMode.Menu)
        {
            if (Input.GetButtonDown("Cancel"))
            {
                StartCoroutine(ShowCanvasGroupCoroutine(GameMode.Main, MAIN));
                MAIN.GetComponent<Image>().sprite = SpriteManager.Instance.CurBG;
                StartCoroutine(SetFocus(startGame));
            }
        }

        if (_mode == GameMode.Game)
        {
            if (Input.GetButtonDown("Cancel") && !GameClear.activeSelf)
            {
                backButton.GetComponent<Button>().onClick.Invoke();
            }

            if (Input.GetButtonDown("Cancel") && GameClear.activeSelf)
            {
                MaskImg2.gameObject.SetActive(true);
                StartCoroutine(CancelMain());

                SetGrid(true);
            }
        }

        if (_mode == GameMode.Main)
        {
            if (Input.GetButtonDown("Cancel"))
            {
                // StartCoroutine(ShowCanvasGroupCoroutine(GameMode.Quit, QUIT));
                // StartCoroutine(SetFocus(QuitGame));
                if (!isExisting)
                {
                    isExisting = true;
                    GOEXIT.SetActive(true);
                    Invoke("ShowTip", 3f);
                }
                else
                {
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#else
 			Application.Quit();
#endif
                }
            }

            // if (Input.touches.Length > 0)
            // {
            //     // Touch touch = Input.GetTouch(0);
            //     if (Input.touches[0].phase == TouchPhase.Began)
            //         _startPosition = Input.touches[0].position;
            //     else if(Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            //     {
            //         Vector2 deltaPosition = Input.touches[0].position - _startPosition;
            //         if (deltaPosition.magnitude > SwipeThreshold)
            //         {
            //             if(deltaPosition.x > 0)
            //             {
            //                 StartCoroutine(ShowCanvasGroupCoroutine(GameMode.Quit, QUIT));
            //                 StartCoroutine(SetFocus(QuitGame));
            //             }
            //         }
            //     }
            // }
        }

        if (MAIN.alpha == 1)
        {
//             if (Input.GetMouseButtonDown(0))
//             {
//                 _MousestartPosition = UnityEngine.Input.mousePosition;
//             }
//             else if (Input.GetMouseButtonUp(0))
//             {
//                 Vector3 deltaPosition = Input.mousePosition - _MousestartPosition;
//                 Debug.Log("Input.touchCount:" + deltaPosition.magnitude);
//                 if (deltaPosition.magnitude > SwipeThreshold)
//                 {
//                     if(deltaPosition.x > 0)
//                     {
//                         if(!isExisting)
//                         {
//                             isExisting = true;
//                             GOEXIT.SetActive(true);
//                             Invoke("ShowTip", 3f);
//                         }
//                         else
//                         {
// #if UNITY_EDITOR
//                             UnityEditor.EditorApplication.isPlaying = false;
// #else
//  			Application.Quit();
// #endif
//                         }
//
//                     }
//                 }
//             }
            if (Input.touchCount <= 0)
            {
                backvalue = 0;
                return;
            }

            if (Input.touchCount > 0)
            {
                // Touch touch = Input.GetTouch(0);
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                    _startPosition = Input.GetTouch(0).position;

                _nowPosition = Input.GetTouch(0).position;

                if ((Input.GetTouch(0).phase == TouchPhase.Ended) || (Input.GetTouch(0).phase == TouchPhase.Canceled))
                {
                    // Vector2 deltaPosition = Input.touches[0].position - _startPosition;
                    _startPosition = _nowPosition;
                    return;
//                     if (deltaPosition.magnitude > SwipeThreshold)
//                     {
//                         if(deltaPosition.x > 0)
//                         {
//                             if(!isExisting)
//                             {
//                                 isExisting = true;
//                                 GOEXIT.SetActive(true);
//                                 Invoke("ShowTip", 3f);
//                             }
//                             else
//                             {
// #if UNITY_EDITOR
//                                 UnityEditor.EditorApplication.isPlaying = false;
// #else
//  			Application.Quit();
// #endif
//                             }
//
//                         }
//                     }
                }

                xMoveDis = Mathf.Abs(_nowPosition.x - _startPosition.x);
                if (xMoveDis != 0)
                {
                    if (!isExisting)
                    {
                        isExisting = true;
                        GOEXIT.SetActive(true);
                        Invoke("ShowTip", 3f);
                    }
                    else
                    {
#if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
#else
 			Application.Quit();
#endif
                    }
                }
            }
        }
    }

    public Button CancelBtn;

    public void CancelBTN()
    {
        if (!isExisting)
        {
            isExisting = true;
            GOEXIT.SetActive(true);
            Invoke("ShowTip", 3f);
        }
        else
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
 			Application.Quit();
#endif
        }
    }
    void ShowTip()
    {
        GOEXIT.SetActive(false);
        isExisting = false;
    }

    IEnumerator SetFocus(Button btn)
    {
        yield return new WaitForSeconds(2f);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(btn.gameObject);
    }

    IEnumerator CancelMain()
    {
        yield return new WaitForSeconds(2f);
        MyUtils.HideAllCGs(canvases);
        MyUtils.EnableCG(MAIN);
        yield return new WaitForSeconds(1f);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(startGame.gameObject);
        MaskImg2.gameObject.SetActive(false);
    }

    public Button QuitGame;
    public Image MaskImg;
    public Image MaskImg2;

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
    }

    public void CancelGame()
    {
        StartCoroutine(ShowCanvasGroupCoroutine(GameMode.Main, MAIN));
        MAIN.GetComponent<Image>().sprite = SpriteManager.Instance.CurBG;
        StartCoroutine(SetFocus(startGame));
    }

    public void SetGrid()
    {
        // Hide all the canvas groups except the grid
        //MyUtils.HideAllCGs(canvases);
        //MyUtils.EnableCG(MENU);
        //StartCoroutine(ShowCanvasGroupCoroutine(MENU));
        StartCoroutine(ShowCanvasGroupCoroutine(GameMode.Main, MAIN));
        StartCoroutine(SetFocus(startGame));
    }

    public void SetGrid(bool finishedImage)
    {
        // Hide all the canvas groups except the grid
        //MyUtils.HideAllCGs(canvases);
        //MyUtils.EnableCG(MENU);
        ShowCanvasGroupCoroutines(GameMode.Menu, MENU, finishedImage);
        MENU.GetComponent<Image>().sprite = SpriteManager.Instance.CurBG;
        MENU.GetComponent<UIMenu>().InitLevel();
    }

    public void SetSettings()
    {
        // Show the settings panel
        // StartCoroutine(ShowCanvasGroupCoroutine(SETTINGS));
    }

    void ShowCanvasGroupCoroutines(GameMode mode, CanvasGroup destinationCG, bool finished = false)
    {
        MyUtils.HideAllCGs(canvases);
        MyUtils.EnableCG(destinationCG);
        if (finished)
        {
            //uiParticlesParent.GetComponent<UIParticles>().PlayParticles(gridContainer.GetChild(activeImage));
            gridContainer.GetChild(activeImage).GetComponent<GridButton>().Appear();
            imageFinishedMenu.Play();

            // Show an interstitial
            UnityAdsManager.ShowVideoStatic();
            if (MaskImg.gameObject.activeSelf) CancelGame();
        }

        _mode = GameMode.None;
    }

    IEnumerator ShowCanvasGroupCoroutine(GameMode mode, CanvasGroup destinationCG, bool finished = false)
    {
        _mode = GameMode.None;
        fadeImage.raycastTarget = true;

        while (fadeImage.color.a < 1)
        {
            Color c = fadeImage.color;
            c.a += Time.deltaTime * fadeSpeed;
            fadeImage.color = c;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        // At this point, the screen is completely white
        // Manage the canvases
        MyUtils.HideAllCGs(canvases);
        MyUtils.EnableCG(destinationCG);

        yield return new WaitForSeconds(0.25f);

        while (fadeImage.color.a > 0)
        {
            Color c = fadeImage.color;
            c.a -= Time.deltaTime * fadeSpeed;
            fadeImage.color = c;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        fadeImage.raycastTarget = false;

        // Show the banner if the destination is the game, else hide it
        if (destinationCG == GAME)
            adMobManager.ShowBanner();
        else
            adMobManager.HideBanner();

        // If we have finised the image, play the particles
        if (finished)
        {
            //uiParticlesParent.GetComponent<UIParticles>().PlayParticles(gridContainer.GetChild(activeImage));
            gridContainer.GetChild(activeImage).GetComponent<GridButton>().Appear();
            imageFinishedMenu.Play();

            // Show an interstitial
            UnityAdsManager.ShowVideoStatic();
            if (MaskImg.gameObject.activeSelf) CancelGame();
        }

        _mode = mode;

        yield return null;
        // isCancel = false;
    }

    void AddListeners()
    {
        int k = 0;
        int jishu = 1;
        int oushu = 0;
        foreach (Button b in gridContainer.GetComponentsInChildren<Button>())
        {
            int _k = k;
            int _jishu = jishu;
            int _oushu = oushu;
            if (_k < sprites.Length)
            {
                // b.onClick.AddListener(delegate { SetGame(_k, b.GetComponent<GridButton>().indexText.text); });

                if (_k >= 0 && _k <= 47)
                {
                    b.GetComponent<GridButton>().indexText.text = (_jishu).ToString();
                    b.GetComponent<GridButton>().indexText2 = (_jishu).ToString();
                }

                if (_k > 47)
                {
                    b.GetComponent<GridButton>().indexText.text = (_oushu).ToString();
                    b.GetComponent<GridButton>().indexText2 = (_oushu).ToString();
                }

                b.GetComponent<GridButton>().indexText.color = Color.white;
                b.GetComponent<GridButton>().image.sprite = sprites[_k];
                b.onClick.AddListener(delegate { SetGame(_k, b.GetComponent<GridButton>().indexText2); });
                // Check if this image has been unlocked or not to show it or not
                int imageState = PlayerPrefs.GetInt("Image" + _k);
                if (imageState == 1)
                {
                    b.GetComponent<GridButton>().indexText.text = "";
                }
                else
                {
                    b.GetComponent<GridButton>().image.gameObject.SetActive(false);
                }
            }
            else
            {
                b.gameObject.SetActive(false);
            }

            k++;
            if (k < 48) jishu += 2;
            if (k > 47) oushu += 2;
        }
    }

    public UIParticles ui;

    public void ImageFinished()
    {
        ui.PlayParticles(GameClear.transform);
        PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + 200);
        Coin.text = PlayerPrefs.GetInt("Coin").ToString();
        // Hide the back button
        backButton.SetActive(false);
        GameClear.SetActive(true);
        // Show the next button
        nextButton.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(nextButton.transform.GetChild(1).GetComponent<Button>().gameObject);
        GAME.gameObject.transform.GetChild(2).gameObject.SetActive(false);
        GAME.gameObject.transform.GetChild(3).gameObject.SetActive(false);
        PlayerPrefs.SetInt("Image" + activeImage, 1);

        // Setup button as finished
        gridContainer.GetChild(activeImage).GetComponent<GridButton>().image.color = Color.white;
        gridContainer.GetChild(activeImage).GetComponent<GridButton>().indexText.text = "";

        // Play a finished sound
        imageFinishedGame.Play();
    }

    public TextMeshProUGUI Coin;
    public TextMeshProUGUI Level;

    public void SetGame(int spriteIndex, string level)
    {
        string thislv = level;
        Level.text = "关卡" + thislv;
        // PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + 200);
        Coin.text = PlayerPrefs.GetInt("Coin").ToString();
        // Hide the grid panel and show the game one
        // Or just fade in the game panel maybe
        // Set the active index
        activeImage = spriteIndex;

        // Animate the button
        gridContainer.GetChild(spriteIndex).GetComponent<GridButton>().Roll();

        // Hide the back button
        backButton.SetActive(true);

        // Show the next button
        nextButton.SetActive(false);
        GameClear.SetActive(false);
        GAME.gameObject.transform.GetChild(2).gameObject.SetActive(false);
        GAME.gameObject.transform.GetChild(3).gameObject.SetActive(true);
        StartCoroutine(ShowCanvasGroupCoroutine(GameMode.Game, GAME));

        // Set the sprite to play with
        rotationCenter.imageSR.sprite = sprites[spriteIndex];

        // Then split the image
        rotationCenter.SplitImage();

        // Play an opening sound
        openImageSound.Play();
        //_mode = GameMode.Game;
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void PurchaseNoAds()
    {
        // Save the preference
        PlayerPrefs.SetInt("NOADS", 1);

        // Disable Unity ads & Admob
        adMobManager.enabled = false;
        unityAdsManager.enabled = false;
        noAdsButton.SetActive(false);
    }
}