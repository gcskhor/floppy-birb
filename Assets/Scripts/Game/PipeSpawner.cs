using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public static PipeSpawner instance;

    [SerializeField] private float _maxTime = 1.5f;
    [SerializeField] private float _heightRange = 5f;
    [SerializeField] private float _pipeLifespan = 8f;

    [SerializeField] private GameObject _pipe;

    private float _timer;
    private List<GameObject> _spawnedPipes = new List<GameObject>();

    private void Awake()
    {
        if (instance == null) 
        {
            instance = this;
        }
    }

    private void Start()
    {
        SpawnPipe();
    }

    private void Update()
    {
        if (_timer > _maxTime)
        {
            SpawnPipe();
            _timer = 0;
        }

        _timer += Time.deltaTime;
    }

    private void SpawnPipe()
    {
        Vector3 spawnPos = transform.position + new Vector3(0, Random.Range(-_heightRange, _heightRange));
        GameObject pipe = Instantiate(_pipe, spawnPos, Quaternion.identity);

        Destroy(pipe, _pipeLifespan);
    }

    async public void ClearPipes()
    {
        for (int i = 0; i < _spawnedPipes.Count; i++)
        {
            if (_spawnedPipes[i] != null)
            {
                Destroy(_spawnedPipes[i]);
            }
        }

        _spawnedPipes.Clear();
    }
}
