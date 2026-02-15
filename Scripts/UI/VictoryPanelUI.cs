using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VictoryPanelUI : MonoBehaviour
{
    public TextMeshProUGUI m_TotalScoreText;
    // 0 == Gold , 1 == sliver
    public List<Sprite> m_MedalIconLits = new();
    public Image m_Medal;


    public Button m_NextLevelButton;
    public Button m_RestartButton;
    public Button m_MainMenuButton;

    private void OnEnable()
    {
        m_TotalScoreText.text = $"{GameUI.Instance.GetTotalScore}";
        UpdateMedalIcon();
    }
    // Update medal icon
    public void UpdateMedalIcon()
    {
        // Update medal icon
        if (GameUI.Instance.IsGoldMedal())
            m_Medal.sprite = m_MedalIconLits[0];
        else
            // Any value below the "Gold Requirement" amount is considered silver
            m_Medal.sprite = m_MedalIconLits[1];
    }
    private void Start()
    {
        // Restart button
        m_NextLevelButton.onClick.RemoveAllListeners();
        m_NextLevelButton.onClick.AddListener(NextLevel);
        // Restart button
        m_RestartButton.onClick.RemoveAllListeners();
        m_RestartButton.onClick.AddListener(Restart);
        // Main menu button
        m_MainMenuButton.onClick.RemoveAllListeners();
        m_MainMenuButton.onClick.AddListener(MainMenu);
    }

    // Load the next level
    public void NextLevel()
    {
        // Increase the current level index
        GameManager.Instance.NextLevel();
        // Reload the scene to start the next level
        GameManager.Instance.LoadScene("Game");
    }
    // Reload game scene
    public void Restart()
    {
        GameManager.Instance.LoadScene("Game");
    }
    // Go to main menu scene
    public void MainMenu()
    {
        GameManager.Instance.LoadScene("Main Menu");
    }

    public void ShowVictoryPanel()
    {
        // Pause the game by stopping time
        Time.timeScale = 0;
        gameObject.SetActive(true);
    }
    void OnDisable()
    {
        // Resume the game by restoring time
        Time.timeScale = 1;
    }
}
