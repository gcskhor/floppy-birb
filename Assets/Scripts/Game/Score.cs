using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public static Score instance;

    private GameManager _gameManager;
    public int CurrentScore;

    [SerializeField] private TextMeshProUGUI _currentScoreText;
    [SerializeField] private TextMeshProUGUI _highScoreText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject); // Keeps the object alive across scenes
        }
        else if (instance != this)
        {
            Destroy(this.gameObject); // Ensures there's only one Score object
        }
    }

    private void OnDestroy()
    {
        Debug.Log("Score instance destroyed");
    }

    void Start()
    {
        _gameManager = GameManager.instance;
        CurrentScore = _gameManager.CurrentScore;

        _currentScoreText.text = CurrentScore.ToString();
        _highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
        UpdateHighScore();
    }

    private void UpdateHighScore()
    {
        if (_gameManager.CurrentScore > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", _gameManager.CurrentScore);
            _highScoreText.text = _gameManager.CurrentScore.ToString();
        }
    }

    public void UpdateScore()
    {
        _gameManager.CurrentScore++;
        _currentScoreText.text = _gameManager.CurrentScore.ToString();
        UpdateHighScore();
    }
}
