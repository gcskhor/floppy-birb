using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlyBehavior : MonoBehaviour
{

    [SerializeField] private float _velocity = 7f;
    [SerializeField] private float _rotationSpeed = 15f;
    [SerializeField] private bool _enableLose = true;


    private Rigidbody2D _rb;
    private GameManager _gameManager;

    private Vector3 _originalPosition;
    private Quaternion _originalRotation; 

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _originalPosition = transform.position;
        _originalRotation = transform.rotation;

        _gameManager = GameManager.instance;

    }

    void Start()
    {
        _gameManager = GameManager.instance;
    }

    void Update()
    {
        if (_gameManager.State == GameState.InGame && (Mouse.current.leftButton.wasPressedThisFrame || 
        Keyboard.current.spaceKey.wasPressedThisFrame ||
        Keyboard.current.fKey.wasPressedThisFrame
        )) 
        {
            _rb.velocity = Vector2.up * _velocity;
        }
    }

    void OnEnable()
    {
        transform.position = _originalPosition; 
        transform.rotation = _originalRotation; 
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0,0, _rb.velocity.y * _rotationSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_enableLose) 
        {
            GameManager.instance.GameOver();
        }
    }
}
