using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{
    public Button RestBtn;
    public Button MainMenuBtn;

    public TMP_Text TurnText;
    public TMP_Text winnerText;

    private GameManger gm;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gm = FindFirstObjectByType<GameManger>();

        RestBtn.onClick.AddListener(() =>
        {
           SceneManager.LoadScene("boardview");
        });
        MainMenuBtn.onClick.AddListener(() =>
        {
           SceneManager.LoadScene("mainmenu");
        });
    }

    // Update is called once per frame
    void Update()
    {
        Winner();

        if (gm.isPlayerTurn)
        {
            TurnText.text = "Player's Turn";
        }
        else
        {
            TurnText.text = "AI's Turn";
        }
    }

    private void Winner()
    {
        if(gm.PlayerWin == true)
            winnerText.text = "HOWWWWWWW!";
        else if (gm.AIWin == true)
            winnerText.text = "AI Wins!";
        else if (gm.Draw == true)
            winnerText.text = "It's a Draw!";
        else
            winnerText.text = "";
    }

    private void OnDestroy()
    {
        RestBtn.onClick.RemoveAllListeners();
        MainMenuBtn.onClick.RemoveAllListeners();
    }


}
