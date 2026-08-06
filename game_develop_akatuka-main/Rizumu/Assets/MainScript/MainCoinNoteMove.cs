using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System;
using Platformer;

public class MainCoinNoteMove : MonoBehaviour
{
    [Header("移動速度")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("削除位置(X座標)")]
    [SerializeField] private float destroyX = -10f;
 
    private PlayerStatus players;
    private MainScoreManager scoreManager;
    
    void Start()
    {
        players = GameObject.Find("PlayerStatus").GetComponent<PlayerStatus>();
        scoreManager = GameObject.Find("ScoreManager").GetComponent<MainScoreManager>();

        if (Cursor.S == 1)
        {
            moveSpeed = 10f;
        }
        else if (Cursor.S == 2)
        {
            moveSpeed = 12f;
        }
        else if (Cursor.S == 3)
        {
            moveSpeed = 15f;
        }
    }
    
    void Update()
    {
        // 左へ移動
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
       
        // 指定位置まで来たら削除
        if (transform.position.x <= destroyX)
        {
            Destroy(gameObject);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("shild"))
        {
            scoreManager.Miss();
            
            Destroy(gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            
            scoreManager.AddCoin(1);
            
            players.getcoin += scoreManager.coin;
        }
    }
}
