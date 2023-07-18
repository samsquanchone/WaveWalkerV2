using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigator : MonoBehaviour
{
    public static SceneNavigator Instance => m_instance;
    private static SceneNavigator m_instance;

    private void Start()
    {
        m_instance = this; 
    }

    public void MoveToScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
