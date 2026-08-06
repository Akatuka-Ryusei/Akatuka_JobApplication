using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    [SerializeField] private RectTransform image;
    
    private int currentIndex = 0;
    
    void Start()
    {
        SelectButton();
        image.anchoredPosition = new Vector2(-439f, -129f);
    }
    
    void Update()
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

         //下
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentIndex++;

            if (currentIndex >= buttons.Length)
                currentIndex = 0;

            SelectButton();
        }

        // 上
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentIndex--;

            if (currentIndex < 0)
                currentIndex = buttons.Length - 1;

            SelectButton();
        }

        if (currentIndex == 0)
        {
            image.anchoredPosition = new Vector2(-439f, -129f);
        }
        else if (currentIndex == 1)
        {
            image.anchoredPosition = new Vector2(-439f, -283f);
        }
            
        // 決定
        if (Input.GetKeyDown(KeyCode.Return))
        {
            buttons[currentIndex].onClick.Invoke();
                
            //戻る
            if (currentIndex == 0)
            {
                SceneManager.LoadScene("mati");
            }
            //ステージ１
            else if (currentIndex == 1)
            {
                Application.Quit();
            }
        }
    }
    
    void SelectButton()
    {
        EventSystem.current.SetSelectedGameObject(buttons[currentIndex].gameObject);
    }
}
