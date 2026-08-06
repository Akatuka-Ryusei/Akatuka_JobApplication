using UnityEngine;
using TMPro;

public class MainScoreManager : MonoBehaviour
{
    private PlayerStatus players;
    
    //スコアUI
    private TextMeshProUGUI scoreText;
    //コンボUI
    private TextMeshProUGUI comboText;
    //攻撃力UI
    private TextMeshProUGUI attackText;
    //コインUI
    private TextMeshProUGUI coinText;
    
    public int score = 100;
    public int mainscore = 0;
    private int combo = 0;
    public int attack = 0;
    public int coin = 0;
    
    private bool upup = false;
    
    void Start()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        comboText = GameObject.FindGameObjectWithTag("ComboText").GetComponent<TextMeshProUGUI>();
        attackText = GameObject.Find("AttackText").GetComponent<TextMeshProUGUI>();
        coinText = GameObject.Find("CoinText").GetComponent<TextMeshProUGUI>();
        players = GameObject.Find("PlayerStatus").GetComponent<PlayerStatus>();
    }
    
    public void AddScore(int point)
    {
        combo++;

        float bonus = 1f;

        if (combo >= 15)
            bonus = 1.3f;
        else if (combo >= 10)
            bonus = 1.2f;
        else if (combo >= 5)
            bonus = 1.1f;

        score += Mathf.RoundToInt(point * bonus);
           
        scoreText.text = "Score:" + score;
        comboText.text = "Combo:" + combo;
    }
    
    public void AddAttack(int attackpoint)
    {
        //combo++;

        float bonus = 1f;

        if (shop.upup == true)
        {
            if (combo >= 15)
                bonus = 1.3f;
            else if (combo >= 10)
                bonus = 1.2f;
            else if (combo >= 5)
                bonus = 1.1f;
        }
        
        attack = Mathf.RoundToInt(attackpoint * bonus);
        
        attackText.text = "×" +  attack;
    }
    
    public void AddCoin(int coinpoint)
    {
        //combo++;

        float bonus = 1f;

        /*if (combo >= 15)
            bonus = 1.3f;
        else if (combo >= 10)
            bonus = 1.2f;
        else if (combo >= 5)
            bonus = 1.1f;*/

        coin = Mathf.RoundToInt(coinpoint * bonus);
        
        coinText.text = "×" +  coin;
    }

    // ミス
    public void Miss()
    {
        combo = 0;
        
        comboText.text = "Combo:" + combo;
        //scoreText.text = "Score" + score;
    }
}
