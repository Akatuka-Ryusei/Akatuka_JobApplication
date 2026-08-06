using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer
{
    public class MainGameManager : MonoBehaviour
    {
        //public int coinsCounter = 0;

        public GameObject playerGameObject;
        private MainPlayerController player;
        public GameObject deathPlayerPrefab;
       // public Text coinText;

        void Start()
        {
            player = GameObject.Find("Player").GetComponent<MainPlayerController>();
        }

        void Update()
        {
            //coinText.text = coinsCounter.ToString();
            if(player.deathState == true)
            {
                playerGameObject.SetActive(false);
                GameObject deathPlayer = (GameObject)Instantiate(deathPlayerPrefab, playerGameObject.transform.position, playerGameObject.transform.rotation);
                deathPlayer.transform.localScale = new Vector3(playerGameObject.transform.localScale.x, playerGameObject.transform.localScale.y, playerGameObject.transform.localScale.z);
                player.deathState = false;
            }
        }
        
        //またシーンを読み込む
        private void ReloadLevel()
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}
