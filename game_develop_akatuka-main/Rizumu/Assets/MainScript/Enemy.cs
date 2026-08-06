using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Image hpBar;
    
    private Image targetImage;

    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite deathSprite;
    public int Enemylife;
    
    public int EnemyMaxlife = 100;
    public int Enemyattack = 1;
   
    void Start()
    {
        targetImage = GameObject.Find("enemyimage").GetComponent<Image>();
        Enemylife = EnemyMaxlife;
    }
    
    void Update()
    {
        hpBar.fillAmount = Mathf.Lerp(
            hpBar.fillAmount,
            (float)Enemylife / EnemyMaxlife,
            5f * Time.deltaTime);

        if (Enemylife <= 0)
        {
            targetImage.sprite = deathSprite;
        }
        else
        {
            targetImage.sprite = normalSprite;
        }
    }
}
