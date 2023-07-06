using System.Threading;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("SCREENS")]
    [SerializeField] private GameObject _startScreen;
    [SerializeField] private GameObject _inGameScreen;
    [SerializeField] private GameObject _gameOverCanvas;

    [Header("GAME OBJECTS")]
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _score;

    [Header("STATES")]
    [SerializeField] private bool _isWalletConnected;
    
    public GameState State;
    public int CurrentScore = 0;

    private PipeSpawner _pipeSpawner;

    public void WalletDisconnect()
    {
        UpdateGameState(GameState.PreConnect);
        _isWalletConnected = false;
    }    

    private void Awake()
    {
        if (instance == null) 
        {
            instance = this;
        }

        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        UpdateGameState(GameState.PostGame);
    }

    public void RestartGame()
    {
        if (_isWalletConnected) 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        UpdateGameState(GameState.PreConnect);

    }

    void Start()
    {
        _pipeSpawner = PipeSpawner.instance;
        UpdateGameState(GameState.PreConnect);
    }

    void Update()
    {
        // Start Game
        if (
            (
                State == GameState.Connected 
                || _isWalletConnected
            ) && Keyboard.current.spaceKey.wasPressedThisFrame 
            && State != GameState.InGame 
            && State != GameState.PostGame
            )
        {
            Debug.Log("StartGame Called");
            UpdateGameState(GameState.InGame);
        }

        // Restart Game
        if (
            State == GameState.PostGame
            && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("RestartGame Called");
            _gameOverCanvas.SetActive(false);
            RestartGame();
        }
    }

    public void UpdateGameState(GameState newState) {
        State = newState;
        Debug.Log("newState: " + newState);

        switch (newState) {
            case GameState.PreConnect:
                _startScreen.SetActive(true);
                Time.timeScale = 0f;
                break;
            case GameState.Connected:
                _isWalletConnected = true;
                Time.timeScale = 0f;
                break;
            case GameState.InGame:
                _startScreen.SetActive(false);
                _gameOverCanvas.SetActive(false);
                _inGameScreen.SetActive(true);
                Time.timeScale = 1f;
                break;
            case GameState.PostGame:
                _gameOverCanvas.SetActive(true);
                _inGameScreen.SetActive(false);
                Time.timeScale = 0f;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }
}

public enum GameState {
    PreConnect,
    Connected,
    InGame,
    PostGame
}