using UnityEngine;
using UnityEngine.UI;

public class Cells : MonoBehaviour
{
    public int Index; //cell index in the grid 0-8

    private GameManager gm;
    private Button btn;
    private Image img;

    public Sprite XSprite;
    public Sprite OSprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gm = FindFirstObjectByType<GameManager>();
        btn = GetComponent<Button>();
        img = GetComponent<Image>();
        btn.onClick.AddListener(OnClick);


        img.sprite = null;
    }
    void OnClick()
    {
        gm.PlayerMove(Index);
    }

    public void SetSprite(int playerMove)
    {
        if (playerMove == 1)
        {
            img.sprite = XSprite;
        }
        else if (playerMove == -1)
        {
            img.sprite = OSprite;
        }
    }

    public void Disable()
    {
        btn.interactable = false;
    }
}
