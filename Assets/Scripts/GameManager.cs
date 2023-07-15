using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance => m_instance;
    private static GameManager m_instance;
    // Start is called before the first frame update
    void Awake()
    {
        m_instance = this;
    }

    public void ResetGameScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
