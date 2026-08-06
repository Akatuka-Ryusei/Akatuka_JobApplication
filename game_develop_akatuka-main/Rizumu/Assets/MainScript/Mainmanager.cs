using Platformer;
using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Mainmanager : MonoBehaviour
{
    private MainNoteSpawner nosp;
    private MainPlayerController player;
    private PlayerStatus players;
    private MainScoreManager score;
    
    [SerializeField] private GameObject Gameclearui; 
    [SerializeField] private GameObject Gameoverui;
    [SerializeField] private GameObject scoreui; 
    [SerializeField] private GameObject goldui;
    
    [SerializeField] private Button[] buttonsC;
    private int currentIndexC = 0;
    
    [SerializeField] private Button[] buttonsG;
    private int currentIndexG = 0;
    
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI goldText;

    private int getcoin2;
    
    private bool GETCOIN = false;

    private void Start()
    {
        nosp = GameObject.Find("NoteManager").GetComponent<MainNoteSpawner>();
        player = GameObject.Find("Player").GetComponent<MainPlayerController>();
        players = GameObject.Find("PlayerStatus").GetComponent<PlayerStatus>();
        score = GameObject.Find("ScoreManager").GetComponent<MainScoreManager>();
        
        Gameoverui.SetActive(false);
        Gameclearui.SetActive(false);
        scoreui.SetActive(false);
        goldui.SetActive(false);
        
        CSelectButton();
        GSelectButton();
    }
    
    private void Update()
    {
        if (player.gameover == true)
        {
            MainNoteSpawner.musicFinished = true;
            
            if (!GETCOIN)
            {
                PlayerStatus.havecoin += getcoin2;
                GETCOIN = true;
            }

            nosp.GO = true;
            
            Gameoverui.SetActive(true);
            
            //下
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                currentIndexG++;

                if (currentIndexG >= buttonsG.Length)
                    currentIndexG = 0;

                GSelectButton();
            }

            // 上
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                currentIndexG--;

                if (currentIndexG < 0)
                    currentIndexG = buttonsG.Length - 1;

                GSelectButton();
            }

            // 決定
            if (Input.GetKeyDown(KeyCode.Return))
            {
                buttonsG[currentIndexG].onClick.Invoke();

                //リスタート
                if (currentIndexG == 0)
                {
                    //三秒後にシーンを読み込む処理を呼び出す
                    Invoke("ReloadLevel", 1);
                    PlayerStatus.attackpower = player.defaultattackpower;
                    PlayerStatus.life = player.Maxlife;
                }
                // 戻る
                else if (currentIndexG == 1)
                {
                    PlayerStatus.attackpower = player.defaultattackpower;
                    PlayerStatus.life = player.Maxlife;
                    SceneManager.LoadScene("mati");
                    Cursor.S = 0;
                }
            }
        }

        if (nosp.Gameclear == true)
        {
            MainNoteSpawner.musicFinished = true;
            
            Gameclearui.SetActive(true);

            StartCoroutine(ShowRoutine());
            
            if (!GETCOIN)
            {
                PlayerStatus.havecoin += getcoin2;
                GETCOIN = true;
            }
            //下
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                currentIndexC++;

                if (currentIndexC >= buttonsC.Length)
                    currentIndexC = 0;

                CSelectButton();
            }

            // 上
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                currentIndexC--;

                if (currentIndexC < 0)
                    currentIndexC = buttonsC.Length - 1;

                CSelectButton();
            }

            // 決定
            if (Input.GetKeyDown(KeyCode.Return))
            {
                buttonsC[currentIndexC].onClick.Invoke();

                //リスタート
                if (currentIndexC == 0)
                {
                    //三秒後にシーンを読み込む処理を呼び出す
                    Invoke("ReloadLevel", 1);
                    PlayerStatus.attackpower = player.defaultattackpower;
                    PlayerStatus.life = player.Maxlife;
                }
                // 戻る
                else if (currentIndexC == 1)
                {
                    SceneManager.LoadScene("mati");
                    Cursor.S = 0;
                }
            }
        }
    }
    
    IEnumerator ShowRoutine()
    {
        if (Cursor.S == 1)
        {
            getcoin2 = players.getcoin * (score.score / 1000);
        }
        else if (Cursor.S == 2)
        {
            getcoin2 = 2 * (players.getcoin * (score.score / 1000));
        }
        else if (Cursor.S == 3)
        {
            getcoin2 = 3 * (players.getcoin * (score.score / 1000));
        }

        scoreui.SetActive(true);
        scoreText.text = "スコア：" + score.score.ToString();
        yield return new WaitForSeconds(1);

        goldui.SetActive(true);
        goldText.text = "獲得金：" + getcoin2.ToString();
        yield return new WaitForSeconds(1);
    }
    
    void CSelectButton()
    {
        EventSystem.current.SetSelectedGameObject(buttonsC[currentIndexC].gameObject);
    }
    
    void GSelectButton()
    {
        EventSystem.current.SetSelectedGameObject(buttonsG[currentIndexG].gameObject);
    }
    
    //またシーンを読み込む
    private void ReloadLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
