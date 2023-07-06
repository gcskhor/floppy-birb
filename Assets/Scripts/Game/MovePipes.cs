using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePipes : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position += Vector3.left * _speed * Time.deltaTime;
    }
}
