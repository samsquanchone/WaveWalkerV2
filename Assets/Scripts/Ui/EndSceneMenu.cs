using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndSceneMenu : MonoBehaviour
{
    Button returnToMenuButton;
    Button restartButton;
    // Start is called before the first frame update
    void Start()
    {
        restartButton = GameObject.Find("RestartButton").GetComponent<Button>();
        returnToMenuButton = GameObject.Find("ReturnToMenuButton").GetComponent<Button>();

        restartButton.onClick.AddListener(delegate { ButtonClicked(2); });
        returnToMenuButton.onClick.AddListener(delegate { ButtonClicked(0); });


    }

    void ButtonClicked(int sceneIndex)
    {
        SceneNavigator.Instance.MoveToScene(sceneIndex);
    }

}
