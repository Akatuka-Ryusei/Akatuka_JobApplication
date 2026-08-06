using UnityEngine;
using Platformer;
using TMPro;

public class shop : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI upText;
    [SerializeField] private TextMeshProUGUI upupText;
    [SerializeField] private TextMeshProUGUI havecoinText;
    
    private Cursor cursor;
    //private PlayerStatus players;

    public int defaultHpup = 100;
    public int defaultup = 100;
    public int defaultupup = 10000;

    public static int Hpupprice = 0;
    public static int upprice = 0;
    public static int upupprice = 0;
    
    public static bool upup = false;

    private static int A = 0;
    
    void Start()
    {
        cursor = GameObject.Find("cursor").GetComponent<Cursor>();
       // players = GameObject.Find("PlayerStatus").GetComponent<PlayerStatus>();
        
        //Hpupprice = defaultHpup;
        //upprice = defaultup;
        //upupprice = defaultupup;
    }
    
    void Update()
    {
        if (A == 0)
        {
            Hpupprice = defaultHpup;
            upprice = defaultup;
            upupprice = defaultupup;
            A = 1;
        }
        else if (A == 1)
        {
            havecoinText.text = "＄" + PlayerStatus.havecoin.ToString();
            hpText.text = "＄" + Hpupprice;
            upText.text = "＄" + upprice;
            upupText.text = "＄" + upupprice;

            //HP
            if (cursor.buyhp == true)
            {
                PlayerStatus.havecoin -= Hpupprice;
                Hpupprice += defaultHpup;


                PlayerStatus.life += 1;
                cursor.buyhp = false;
            }

            //攻撃力
            if (cursor.buyup == true)
            {
                PlayerStatus.havecoin -= upprice;
                upprice += defaultup;

                PlayerStatus.attackpower += 1;
                cursor.buyup = false;
            }

            //能力
            if (cursor.buyupup == true)
            {
                if (upup == false)
                {
                    PlayerStatus.havecoin -= upupprice;
                    upupprice += defaultupup;

                    upup = true;
                    cursor.buyupup = false;
                }
                else
                {
                    return;
                }
            }
        }
    }
}
