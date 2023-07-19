using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState {Normal, MenuOpen };

public class GameManager : MonoBehaviour
{
    public GameState gameState { get; private set; }
    public static GameManager Instance => m_instance;
    private static GameManager m_instance;
    // Start is called before the first frame update
    void Awake()
    {
        m_instance = this;
    }

    private void Start()
    {
        gameState = GameState.Normal;
    }

    public void ResetGameScene()
    {
       // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ChangeGameState(GameState newState)
    {
        gameState = newState;
    }

    public void GameWon()
    {
        SceneNavigator.Instance.MoveToScene(3);
    }

    public GameState GetGameState()
    {
        GameState currentState = gameState;

        return currentState;
    }
}
