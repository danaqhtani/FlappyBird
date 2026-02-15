using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button m_PlayButton;
    [SerializeField] private Button m_GameModeButton;
    [SerializeField] private Button m_QuitButton;
    // To show or hide the game mode window
    [SerializeField] private GameObject m_GameModeWindow;

    // To select mode ( Easy , hard , mission)
    [Header("Game Mode - Options"), SerializeField] private List<Button> m_GameModeOptionsList = new List<Button>();
    [SerializeField] private Button m_CloseGameModeWindow;
    // To select bird ( Red , Yellow , Blue)
    [Header("Flappy Bird Type - Options"), SerializeField] private List<Button> m_FlappyBirdTypeList = new List<Button>();
    [SerializeField] private RectTransform m_SelectionArrow;

    private IEnumerator Start()
    {
        // Play button
        m_PlayButton.onClick.RemoveAllListeners();
        m_PlayButton.onClick.AddListener(Play);
        // Game mode button
        m_GameModeButton.onClick.RemoveAllListeners();
        m_GameModeButton.onClick.AddListener(ShowGameModeWindow);
        // Quit button
        m_QuitButton.onClick.RemoveAllListeners();
        m_QuitButton.onClick.AddListener(Quit);


        // Initialize game mode options
        for (int i = 0; i < m_GameModeOptionsList.Count; i++)
        {
            // 0 == Easy , 1 == hard , 2 == mission  
            int optionIndex = i;
            m_GameModeOptionsList[i].onClick.RemoveAllListeners();
            m_GameModeOptionsList[i].onClick.AddListener(() => SelectGameMode(optionIndex));
        }
        // Close game mode window
        m_CloseGameModeWindow.onClick.RemoveAllListeners();
        m_CloseGameModeWindow.onClick.AddListener(CloseGameModeWindow);

        // Initialize Flappy Bird type options
        for (int i = 0; i < m_FlappyBirdTypeList.Count; i++)
        {
            // 0 == Red , 1 == Yellow , 2 == Blue  
            int optionIndex = i;
            m_FlappyBirdTypeList[i].onClick.RemoveAllListeners();
            m_FlappyBirdTypeList[i].onClick.AddListener(() => SelectFlappyBird(optionIndex));
        }

        yield return new WaitForSeconds(.001f);

        // Show current game mode
        UpdateSelectGameModeUI((int)GameManager.Instance.m_GameMode);
        // Show current bird
        UpdateSelectFlappyBirdUI((int)GameManager.Instance.m_FlappyBirdType);
    }

    // When the Play button is pressed, load the game scene
    private void Play()
    {
        GameManager.Instance?.LoadScene("Game");
    }
    // When the Select Game Mode button is pressed, show the Game Mode window
    private void ShowGameModeWindow()
    {
        m_GameModeWindow.SetActive(true);
    }
    // Quit the app 
    private void Quit()
    {
        Application.Quit();
    }
    // Select game mode
    private void SelectGameMode(int index)
    {
        GameManager.Instance.ChangeGameModeType((GameMode)index);
        // Update the UI to highlight the selected game mode
        // Use green to indicate the currently selected mode
        UpdateSelectGameModeUI(index);
    }
    private void UpdateSelectGameModeUI(int index)
    {
        for (int i = 0; i < m_GameModeOptionsList.Count; i++)
        {
            bool selected = (index == i);
            m_GameModeOptionsList[i].GetComponent<Image>().color = selected ? Color.green : Color.white;
        }
    }
    // Close game mode window
    private void CloseGameModeWindow()
    {
        m_GameModeWindow.SetActive(false);
    }

    // Select flappy bird
    private void SelectFlappyBird(int index)
    {
        GameManager.Instance.ChangeFlappyBirdType((FlappyBirdType)index);
        // Update selection arrow position
        UpdateSelectFlappyBirdUI(index);
    }
    // Update arrow position UI
    private void UpdateSelectFlappyBirdUI(int index)
    {
        var birdPosition = m_FlappyBirdTypeList[index].transform.localPosition;
        // Set arrow's position to bird's
        m_SelectionArrow.localPosition = birdPosition + (Vector3.up * 32);
    }

}
