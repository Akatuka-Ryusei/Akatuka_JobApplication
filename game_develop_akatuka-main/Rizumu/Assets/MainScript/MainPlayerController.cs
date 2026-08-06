using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;

namespace Platformer
{
    public class MainPlayerController : MonoBehaviour
    {
        private MainScoreManager scoreManager;
        private Enemy enemy;
        private MainNoteSpawner NS;
        
        public float jumpForce;

        [Header("Shield")] [SerializeField] private GameObject shieldPrefab;

        [SerializeField] private Transform shieldPoint;

        // シールドの表示時間（秒）
        [SerializeField] private float shieldDuration = 3.0f;

        private GameObject currentShield;
        private bool isShield = false;

        [HideInInspector] public bool deathState = false;

        private bool isGrounded;
       // public Transform groundCheck;

        private Rigidbody2D rigidbody;
        private Animator animator;
        //private GameManager gameManager;

        //ライフUI
        private TextMeshProUGUI lifeText;
        //攻撃力UI
        private TextMeshProUGUI attackText;
        //コインUI
        private TextMeshProUGUI coinText;

        private PlayerStatus players;
        
        public bool gameover = false;
        
        public int defaultattackpower;
        public int defaultattackpower2;
        public int attackcount = 0;

        public int Maxlife;
        
        void Start()
        {
            scoreManager = GameObject.Find("ScoreManager").GetComponent<MainScoreManager>();
            rigidbody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            //gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            enemy = GameObject.Find("Enemy").GetComponent<Enemy>();
            lifeText = GameObject.Find("LifeText").GetComponent<TextMeshProUGUI>();
            attackText = GameObject.Find("AttackText").GetComponent<TextMeshProUGUI>();
            coinText = GameObject.Find("CoinText").GetComponent<TextMeshProUGUI>();
            players = GameObject.Find("PlayerStatus").GetComponent<PlayerStatus>();
            
            defaultattackpower = PlayerStatus.attackpower;
            defaultattackpower2 = defaultattackpower;
            
            Maxlife = PlayerStatus.life;
        }

        private void FixedUpdate()
        {
            //CheckGround();
        }

        void Update()
        {
            if (MainNoteSpawner.musicFinished == true)
            {
                PlayerStatus.attackpower = defaultattackpower;
                PlayerStatus.life = Maxlife;
            }
            
            lifeText.text = "×" + PlayerStatus.life.ToString();
            attackText.text = "×" + PlayerStatus.attackpower.ToString();
            coinText.text = "×" + players.getcoin.ToString();

            // ジャンプアニメーション
            /*if (!isGrounded)
            {
                animator.SetInteger("playerState", 2);
            }
            else
            {
                animator.SetInteger("playerState", 0);
            }*/

            // PCのマウスクリック または スマホのタップ
            if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.S)|| Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                //Jump();
                CreateShield();
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                if (attackcount >= 1)
                {
                    defaultattackpower--;
                }
                
                enemy.Enemylife -= PlayerStatus.attackpower;
                PlayerStatus.attackpower = defaultattackpower;
                    attackcount++;
            }

            if (PlayerStatus.life <= 0)
            {
                gameover = true;
                deathState = true;

                MainNoteSpawner.musicFinished = true;
            }
            else
            {
                deathState = false;
            }
        }

        void Jump()
        {
            rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        public void CreateShield()
        {
            if (isShield) return;

            StartCoroutine(ShieldCoroutine());
        }

        private IEnumerator ShieldCoroutine()
        {
            isShield = true;

            currentShield = Instantiate(
                shieldPrefab,
                shieldPoint.position,
                Quaternion.identity,
                transform
            );

            // 指定時間待つ
            yield return new WaitForSeconds(shieldDuration);

            Destroy(currentShield);

            currentShield = null;
            isShield = false;
        }

        /*void LateUpdate()
        {
            if (currentShield != null)
            {
                currentShield.transform.position = shieldPoint.position;
            }
        }*/

        /*private void CheckGround()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(
                groundCheck.position,
                0.2f
            );

            isGrounded = colliders.Length > 1;
        }*/
    }
}

//0.3

