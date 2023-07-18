using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    GameObject gameMenu;
    public GameObject howToPlayMenu;

    Button resumeButton;
    Button settingsButton;
    Button howToPlayButton;
    Button quitButton;
    // Start is called before the first frame update
    void Start()
    {
        gameMenu = GameObject.Find("Menu");
       

        resumeButton = GameObject.Find("ResumeButton").GetComponent<Button>();
        settingsButton = GameObject.Find("SettingsButton").GetComponent<Button>();
        howToPlayButton = GameObject.Find("HowToPlayButton").GetComponent<Button>();
        quitButton = GameObject.Find("QuitButton").GetComponent<Button>();

        resumeButton.onClick.AddListener(delegate { ResumeGame(); });
        settingsButton.onClick.AddListener(delegate { ShowSettings(); });
        howToPlayButton.onClick.AddListener(delegate { ShowHowToPlayGuide(); });
        quitButton.onClick.AddListener(delegate { ReturnToMenu(); });

        gameMenu.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameMenu.SetActive(true);
            GameManager.Instance.ChangeGameState(GameState.MenuOpen);
        }
    }

    void ResumeGame()
    {
        gameMenu.SetActive(false);
        GameManager.Instance.ChangeGameState(GameState.Normal);
    }

    void ShowSettings()
    {
        
    }

    void ShowHowToPlayGuide()
    {
        howToPlayMenu.SetActive(true);
    }

    void ReturnToMenu()
    {
        SceneNavigator.Instance.MoveToScene(0);
    }
}
