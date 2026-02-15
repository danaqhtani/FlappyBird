using UnityEngine;
using UnityEngine.SceneManagement;

public partial class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    // Game mode
    public GameMode m_GameMode = GameMode.Easy;
    // Flappy bird type
    public FlappyBirdType m_FlappyBirdType = FlappyBirdType.Red;

    // Used to change the level sprites, such as the sky and pipes
    public int m_LevelIndex = 0;
    private int maxLevelIndex = 2;
    public void NextLevel()
    {
        // Make the level index wrap around
        m_LevelIndex = (m_LevelIndex + 1) % maxLevelIndex;
    }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Sets the current bird type.
    /// </summary>
    public void ChangeFlappyBirdType(FlappyBirdType flappyBirdType)
    {
        m_FlappyBirdType = flappyBirdType;
    }

    /// <summary>
    /// Sets a new game mode to apply during gameplay.
    /// </summary>
    public void ChangeGameModeType(GameMode gameMode)
    {
        m_GameMode = gameMode;
    }

}
