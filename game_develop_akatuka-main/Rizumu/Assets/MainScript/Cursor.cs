using Platformer;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Cursor : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    [SerializeField] private Button[] stagebuttons;
    [SerializeField] private Button[] shopbuttons;
    
    private PlayerStatus players;
    private shop _shop;
    [SerializeField] private GameObject SHOP;
    [SerializeField] private GameObject stage;
    
    private int currentIndex = 0;
    private int stagecurrentIndex = 0;
    private int shopcurrentIndex = 0;
    private bool cursorstatusbattle = false;
    private bool cursorstatusshop = false;
    public bool buyhp = false;
    public bool buyup = false;
    public bool buyupup = false;

    private int a = 0;
    private int b = 0;
    private int c = 0;
    public static int S = 0;
    
    void Start()
    {
        SelectButton();
        _shop = GameObject.Find("SHOP").GetComponent<shop>();
        SHOP.SetActive(false);
        stage.SetActive(false);
        players = GameObject.FindGameObjectWithTag("PlayerStatus").GetComponent<PlayerStatus>();
        c = PlayerPrefs.GetInt("c");
    }
    
    void Update()
    {
        if (c == 0)
        {
            PlayerPrefs.SetInt("Coin", 0);
            PlayerPrefs.Save();

            c = PlayerPrefs.GetInt("c", 1);
        }
        
        //普通のカーソル処理
        if (cursorstatusbattle == false && cursorstatusshop == false)
        {
            // 右
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                currentIndex++;

                if (currentIndex >= buttons.Length)
                    currentIndex = 0;

                SelectButton();
            }

            // 左
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                currentIndex--;

                if (currentIndex < 0)
                    currentIndex = buttons.Length - 1;

                SelectButton();
            }

            // 決定
            if (Input.GetKeyDown(KeyCode.Return))
            {
                buttons[currentIndex].onClick.Invoke();

                //戻り
                if (currentIndex == 0)
                {
                    SceneManager.LoadScene("Title");
                }
                // ショップ
                else if (currentIndex == 1)
                {
                    a++;
                }
                //バトル
                else if (currentIndex == 2)
                {
                    b++;
                }

                //Debug.Log(currentIndex);
            }
        }
        
        //ショップ
        if (cursorstatusshop == true)
        {
            SHOP.SetActive(true);
            
            // 右
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                shopcurrentIndex++;

                if (shopcurrentIndex >= shopbuttons.Length)
                    shopcurrentIndex = 0;

                shopSelectButton();
            }

            // 左
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                shopcurrentIndex--;

                if (shopcurrentIndex < 0)
                    shopcurrentIndex = shopbuttons.Length - 1;

                shopSelectButton();
            }

            // 決定
            if (Input.GetKeyDown(KeyCode.Return))
            {
                shopbuttons[shopcurrentIndex].onClick.Invoke();

                //戻る
                if (shopcurrentIndex == 0)
                {
                    a = 0;
                }
                //HPUP
                else if (shopcurrentIndex == 1)
                {
                    if (PlayerStatus.havecoin >= shop.Hpupprice)
                    {
                        buyhp = true;
                    }
                }
                //UP
                else if (shopcurrentIndex == 2)
                {
                    if (PlayerStatus.havecoin >= shop.upprice)
                    {
                        buyup = true;
                    }
                }
                //UPUP
                else if (shopcurrentIndex == 3)
                {
                    if (PlayerStatus.havecoin >= shop.upupprice)
                    {
                        buyupup = true;
                    }
                }
            }
        }

        //バトル
        if (cursorstatusbattle == true)
        {
            stage.SetActive(true);
            
            // 右
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                stagecurrentIndex++;

                if (stagecurrentIndex >= stagebuttons.Length)
                    stagecurrentIndex = 0;

                stageSelectButton();
            }

            // 左
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                stagecurrentIndex--;

                if (stagecurrentIndex < 0)
                    stagecurrentIndex = stagebuttons.Length - 1;

                stageSelectButton();
            }

            /* 下
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                stagecurrentIndex++;

                if (stagecurrentIndex >= stagebuttons.Length)
                    stagecurrentIndex = 0;

                stageSelectButton();
            }

            // 上
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                stagecurrentIndex--;

                if (stagecurrentIndex < 0)
                    stagecurrentIndex = stagebuttons.Length - 1;

                stageSelectButton();
            }*/
            
            // 決定
            if (Input.GetKeyDown(KeyCode.Return))
            {
                stagebuttons[stagecurrentIndex].onClick.Invoke();
                
                //戻る
                if (stagecurrentIndex == 0)
                {
                    b = 0;
                }
                //ステージ１
                else if (stagecurrentIndex == 1)
                {
                    SceneManager.LoadScene("main");
                    b = 0;
                    S = 1;
                }
                else if (stagecurrentIndex == 2)
                {
                    SceneManager.LoadScene("main 1");
                    b = 0;
                    S = 2;
                }
                else if (stagecurrentIndex == 3)
                {
                    SceneManager.LoadScene("main 2");
                    b = 0;
                    S = 3;
                }
            }
        }

        if (a >= 1)
        {
            cursorstatusshop = true;
            //SHOP.SetActive(true);
        }
        else if (a == 0)
        {
            SHOP.SetActive(false);
            cursorstatusshop = false;
        }

        if (b >= 1)
        {
            cursorstatusbattle = true;
        }
        else if (b == 0)
        {
            stage.SetActive(false);
            cursorstatusbattle = false;
        }
        
        //Debug.Log("activeSelf : " + SHOP.activeSelf);
        //Debug.Log("activeInHierarchy : " + SHOP.activeInHierarchy);
    }
    
    void SelectButton()
    {
        EventSystem.current.SetSelectedGameObject(buttons[currentIndex].gameObject);
    }
    
    void shopSelectButton()
    {
        EventSystem.current.SetSelectedGameObject(shopbuttons[shopcurrentIndex].gameObject);
    }
    
    void stageSelectButton()
    {
        EventSystem.current.SetSelectedGameObject(stagebuttons[stagecurrentIndex].gameObject);
    }
}
