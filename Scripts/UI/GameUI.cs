using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public static GameUI Instance { get; private set; }

    public TextMeshProUGUI m_ScoreText;
    public TextMeshProUGUI m_CoinText;
    public TextMeshProUGUI m_TimerText;

    public int ScoreAmount { get; private set; } = 0;
    public int CoinAmount { get; private set; } = 0;
    public int GetTotalScore
    {
        get
        {
            return ScoreAmount + CoinAmount;
        }
    }
    private float timer = 0f;

    /// <summary>
    /// Adds a point to the score and updates the UI.
    /// </summary>
    public void AddScore()
    {
        ScoreAmount++;
        // Update the score UI text
        m_ScoreText.text = $"Score: {ScoreAmount}";
    }

    /// <summary>
    /// Adds a coin to the total and updates the UI.
    /// </summary>
    public void AddCoin()
    {
        CoinAmount++;
        // Update the coin UI text
        m_CoinText.text = $"Coin: {CoinAmount}";
    }

    private Bird bird;
    public GameObject m_TapToStart;

    [Header("Requirements to complete the game")]
    // Gold needed for the gold medal
    public int m_GoldRequirement = 10;
    // Pipes needed to complete the level
    public int m_EasyPipeRequirement = 5;
    public int m_HardPipeRequirement = 20;
    // Maximum timer duration for Mission mode
    public float m_MissionMaxTimer = 5f;

    public bool IsGoldMedal()
    {
        return CoinAmount >= m_GoldRequirement;
    }

    private void Start()
    {
        Instance = this;

        // Find and assign a reference to the Bird instance
        bird = FindAnyObjectByType<Bird>();
        // Hide timer ui if we not in "Mission" game mode
        m_TimerText.transform.parent.gameObject.SetActive(GameManager.Instance.m_GameMode == GameMode.Mission);
    }
    private void Update()
    {
        if (bird.IsReadyAndAlive)
        {
            timer = Mathf.Clamp(timer + (Time.deltaTime * .1f), 0, m_MissionMaxTimer);
            m_TimerText.text = $"Timer: {timer:F1} / {m_MissionMaxTimer}m";


            switch (GameManager.Instance.m_GameMode)
            {
                //  Use the pipe requirement "Easy"
                case GameMode.Easy:
                    if (ScoreAmount >= m_EasyPipeRequirement)
                        ShowVictoryPanel();
                    break;
                case GameMode.Mission:
                    // In Mission mode, end the game when the timer runs out
                    if (timer >= m_MissionMaxTimer)
                        ShowVictoryPanel();
                    break;
                case GameMode.Hard:
                    if (ScoreAmount >= m_HardPipeRequirement)
                        ShowVictoryPanel();
                    break;

            }
        }
    }
    private void ShowVictoryPanel()
    {
        FindAnyObjectByType<VictoryPanelUI>(FindObjectsInactive.Include).ShowVictoryPanel();
    }

    public void HideTapToStartUI()
    {
        m_TapToStart.SetActive(false);
    }
}
