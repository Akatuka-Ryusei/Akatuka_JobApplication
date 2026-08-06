
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [Header("プレイヤーのライフ")]
    public static int life = 10;
        
    [Header("プレイヤーの攻撃力")]
    public static int attackpower = 10;
        
    [Header("プレイヤーの所持金")]
    public static int havecoin = 0;
    public int getcoin = 0;
    void Start()
    {
    }
    
    void Update()
    {
    }
}
