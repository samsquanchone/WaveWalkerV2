using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    private Button playButton;
    private Button howToPlayButton;
    private Button quitButton;
    // Start is called before the first frame update
    void Start()
    {
        playButton = GameObject.Find("PlayButton").GetComponent<Button>();
        howToPlayButton = GameObject.Find("HowToPlayButton").GetComponent<Button>();
        quitButton = GameObject.Find("QuitButton").GetComponent<Button>();

        playButton.onClick.AddListener(delegate { MenuButtonPressed(2); });
        howToPlayButton.onClick.AddListener(delegate { MenuButtonPressed(1); });
        quitButton.onClick.AddListener(delegate { QuitButtonPressed(); });
    }

    void MenuButtonPressed(int sceneIndex)
    {
        SceneNavigator.Instance.MoveToScene(sceneIndex);
    }

    void QuitButtonPressed()
    {
        SceneNavigator.Instance.QuitGame();
    }
}
