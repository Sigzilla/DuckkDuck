using UnityEngine;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private int StartingLives = 3;
    [SerializeField] private TMP_Text LivesText;
    [SerializeField] private GameObject GameOverScreen;

    private int CurrentLives;

    private void Start()
    {
        CurrentLives = StartingLives;
        
        if (GameOverScreen != null)
        {
            GameOverScreen.SetActive(false);
        }
    }

    public void reduceLives()
    {
        CurrentLives--;

        UpdateLivesText();

        if (CurrentLives <= 0)
        {
            GameOver();
        }
    }

    private void UpdateLivesText()
    {
        if (LivesText != null)
        {
            LivesText.text = "Lives: " + CurrentLives;
        }
    }

    private void GameOver()
    {
        GameOverScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResetLevel()
    {
        GameOverScreen.SetActive(false);

        CurrentLives = StartingLives;
        UpdateLivesText();

        Time.timeScale = 1.0f;
    }
}
