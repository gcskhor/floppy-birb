using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Thirdweb;
using System.Threading.Tasks;

public class DisplayGameOverMessage : MonoBehaviour
{

    private int _score;
    private GameManager _gameManager;

    [SerializeField] private TextMeshProUGUI _gameOverMessage;
    [SerializeField] private TextMeshProUGUI _scoreText;

    void Start()
    {
        _gameManager = GameManager.instance;
        if (_gameManager.State == GameState.PostGame)
            {
                // Show messages
                _score = _gameManager.CurrentScore;

                _gameOverMessage.text = $"You have earned {_score} BIRB tokens.";
                _scoreText.text = $"Score: {_score}";
            }
    }
}
