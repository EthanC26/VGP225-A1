using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button playBtn;
    public Button quitBtn;

    public TMP_Text titleText;

    private GameManger gm;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FindFirstObjectByType<GameManger>();

        playBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("boardview");
        });
        quitBtn.onClick.AddListener(() =>
        {
           QuitGame();
        });
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
