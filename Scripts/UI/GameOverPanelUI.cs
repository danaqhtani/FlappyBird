using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanelUI : MonoBehaviour
{
    public TextMeshProUGUI m_TotalScoreText;

    public Button m_RestartButton;
    public Button m_MainMenuButton;

    private void OnEnable()
    {
        m_TotalScoreText.text = $"<color=#E6611D> Best </color>\n{GameUI.Instance.GetTotalScore}";
    }
    private void Start()
    {
        // Restart button
        m_RestartButton.onClick.RemoveAllListeners();
        m_RestartButton.onClick.AddListener(Restart);
        // Main menu button
        m_MainMenuButton.onClick.RemoveAllListeners();
        m_MainMenuButton.onClick.AddListener(MainMenu);
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

    public void ShowGameOverPanel()
    {
        gameObject.SetActive(true);
    }
}
