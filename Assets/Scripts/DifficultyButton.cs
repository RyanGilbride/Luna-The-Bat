using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    [SerializeField] private int difficulty;

    private Button button;
    private GameManager gameManager;

    void Start()
    {
        button = GetComponent<Button>();
        gameManager = GameObject.Find("GameManager")?.GetComponent<GameManager>();

        if (button != null && gameManager != null)
        {
            button.onClick.AddListener(SelectLevel);
        }
    }

    private void SelectLevel()
    {
        gameManager?.StartGame(difficulty);
    }
}