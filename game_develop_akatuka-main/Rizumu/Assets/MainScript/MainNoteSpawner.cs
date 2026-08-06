using System.Collections;
using Platformer;
using UnityEngine;

public class MainNoteSpawner : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;

    [Header("Note")]
    [SerializeField] private GameObject[] notePrefabs;
    [SerializeField] private Transform spawnPoint;

    [Header("Analyze")]
    [SerializeField] private int sampleSize = 256;

    [SerializeField] private float volumeThreshold = 0.02f;
    [SerializeField] private float spawnInterval = 0.2f;
    private MainPlayerController player;
     private PlayerStatus players;
     private Enemy enemy;

    private float[] samples;
    private float timer;
    
    public bool GO = false;

    // 曲終了判定用
    public bool Gameclear = false;
    
    public static bool musicFinished = false;
    
    private bool isPause = false;
    private bool isResume = false;

    private int A = 0;

    void Start()
    {
        musicFinished = false;
        
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<MainPlayerController>();
        players = GameObject.FindGameObjectWithTag("PlayerStatus").GetComponent<PlayerStatus>();
        enemy = GameObject.Find("Enemy").GetComponent<Enemy>();
        
        samples = new float[sampleSize];
        audioSource.Play();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (A == 0)
            {
                isPause = true;
                Time.timeScale = 0f;
                audioSource.Pause();
                A = 1;
            }
            else if (A == 1)
            {
                StartCoroutine(ResumeCoroutine());
                A = 0;
            }
        }
        
        if (isPause)
        {
            return;
        }
        
        // 曲が終わったか判定
        if (!musicFinished && !audioSource.isPlaying)
        {
            musicFinished = true;
            MusicEnd();
            return;
        }

        if (GO == true)
        {
            musicFinished = true;
            MusicEnd();
        }

        /*if (Input.GetKeyDown(KeyCode.E))
        {
            enemy.Enemylife = 0;
            musicFinished = true;
            MusicEnd();
        }*/

        // 曲終了後は解析しない
        if (musicFinished)
            return;

        timer += Time.deltaTime;

        audioSource.GetOutputData(samples, 0);

        float volume = 0f;

        for (int i = 0; i < samples.Length; i++)
        {
            volume += Mathf.Abs(samples[i]);
        }

        volume /= samples.Length;
        
        //Debug.Log(volume);

        if (volume > volumeThreshold && timer >= spawnInterval)
        {
            SpawnNote();
            timer = 0f;
        }
    }

    public void SpawnNote()
    {
        int randomIndex = Random.Range(0, notePrefabs.Length);
        
        if (enemy.Enemylife == 0 || enemy.Enemylife <= 0)
        {
            int randomIndex1 = Random.Range(1, notePrefabs.Length);
            
            Instantiate(
                notePrefabs[randomIndex1],
                spawnPoint.position,
                Quaternion.identity
            );
            
            return;
        }
        
        // ランダムに選ばれたPrefabを生成
        Instantiate(
            notePrefabs[randomIndex],
            spawnPoint.position,
            Quaternion.identity
        );
    }
    
    IEnumerator ResumeCoroutine()
    {
        yield return new WaitForSecondsRealtime(3f);

        Time.timeScale = 1f;

        audioSource.UnPause();

        isPause = false;
    }

    void MusicEnd()
    {
        if (PlayerStatus.life >= 1 && enemy.Enemylife == 0 || enemy.Enemylife <= 0)
        {
            Gameclear = true;
            
            //Debug.Log("クリア");
        } 
        else if (PlayerStatus.life >= 1 && enemy.Enemylife >= 0)
        {
            player.gameover = true;
            
            //Debug.Log("クリア");
        }
        
        audioSource.Stop();

        // ここにリザルト表示などを書く
        // SceneManager.LoadScene("Result");
    }
}

//Debug.Log(volume);
