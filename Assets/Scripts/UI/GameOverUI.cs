using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private Button restartButton;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Color[] backgroundColors;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        restartButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
        });
        Hide();
    }
    public void Show(bool win)
    {
        TurnSystem.Instance.SetIsGameOver(true);
        if (win)
        {
            backgroundImage.color = backgroundColors[1];
            gameOverText.text = "YOU WON";
        }
        else
        {
            backgroundImage.color = backgroundColors[0];
            gameOverText.text = "YOU LOST";
        }
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
