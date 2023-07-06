// using System.Diagnostics;
using System.Threading;
// using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopBackground : MonoBehaviour
{
    [SerializeField] private float _speed = 0.5f;
    [SerializeField] private float _width = 6f;

    private SpriteRenderer _spriteRenderer;
    private Vector2 _startSize;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _startSize = new Vector2(_spriteRenderer.size.x, _spriteRenderer.size.y);
    }

    void Update()
    {
        _spriteRenderer.size = new Vector2(_spriteRenderer.size.x + _speed * Time.deltaTime, _spriteRenderer.size.y);

        if (_spriteRenderer.size.x > 2 * _startSize.x) 
        {
            _spriteRenderer.size = _startSize;
        }
    }
}
