using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public int enemy_count;
        public float rate;
    }

    public enum WaveState
    {
        Spawning,
        Waiting,
        Counting
    }

    [SerializeField]
    Transform[] spawnPoints;
    [SerializeField]
    Transform[] lazerThresholds;
    [SerializeField]
    Transform lazerSpawnPoint;

    private EnemySpawner _enemyPool;
    [SerializeField]
    private BasicEnemy[] _enemies;
    private double _accumulatedWeight;
    private System.Random _random = new System.Random();
    private float _waveCountdown;
    public float TimeBetweenWave { get; private set; } = 5f;
    private WaveState _waveState;
    private Wave _wave;
    private int _moveRatio = 0;

    private void Awake()
    {
        CalculatedWeight();
    }
    // Start is called before the first frame update
    void Start()
    {
        _enemyPool = GetComponent<EnemySpawner>();
        _waveCountdown = TimeBetweenWave;
        _waveState = WaveState.Counting;
        _wave = new Wave { enemy_count = 5, rate = 1.5f };
    }

    // Update is called once per frame
    void Update()
    {
        if (_waveState == WaveState.Waiting)
        {
            WaveComplete();
        }
        if (_waveCountdown <= 0)
        {
            if (_waveState != WaveState.Spawning)
            {
                // spawn here
                StartCoroutine(SpawnWave(_wave, _moveRatio));
            }
        }
        else
        {
            _waveCountdown -= Time.deltaTime;
        }
    }

    void SpawnEnemy(int moveRatio)
    {
        int randomIndexLocation = Random.Range(0, spawnPoints.Length);
        BasicEnemy randomEnemy = _enemies[GetRandomEnemyIndex()];
        switch (randomEnemy.name)
        {
            case "MiniEnemy":
                var _enemyMini = _enemyPool.MiniPool.Get();
                if (_enemyMini != null)
                {
                    _enemyMini.gameObject.transform.position = spawnPoints[randomIndexLocation].position;
                    _enemyMini.MoveSpeed += moveRatio;
                }

                break;
            case "BombEnemy":
                var _enemyBomb = _enemyPool.BombPool.Get();
                if (_enemyBomb != null)
                {
                    _enemyBomb.gameObject.transform.position = spawnPoints[randomIndexLocation].position;
                    _enemyBomb.MoveSpeed += moveRatio;
                }
                break;
            case "LazerEnemy":
                var _enemyLazer = _enemyPool.LazerPool.Get();
                if (_enemyLazer != null)
                {
                    _enemyLazer.transform.position = lazerSpawnPoint.position;
                    _enemyLazer.Threshold = lazerThresholds[Random.Range(0, lazerThresholds.Length)];
                    _enemyLazer.MoveSpeed += moveRatio;
                }
                break;
            default:
                return;

        }
    }
    void WaveComplete()
    {
        Debug.Log("Wave complete");
        _waveCountdown = TimeBetweenWave + 10;
        _wave = new Wave { enemy_count = _wave.enemy_count + 2, rate = _wave.rate += 0.04f };
        _moveRatio += (int)_wave.rate;

        _waveState = WaveState.Counting;
    }
    IEnumerator SpawnWave(Wave wave, int moveRation)
    {
        Debug.Log($"Spawning new Wave with {wave.enemy_count} and {wave.rate}");
        _waveState = WaveState.Spawning;

        for (int i = 0; i < wave.enemy_count; i++)
        {
            SpawnEnemy(moveRation);
            yield return new WaitForSeconds(1f / wave.rate);
        }

        _waveState = WaveState.Waiting;

        yield break;
    }

    private int GetRandomEnemyIndex()
    {
        double randomIndex = _random.NextDouble() * _accumulatedWeight;

        for (int i = 0; i < _enemies.Length; i++)
        {
            if (_enemies[i]._weight >= randomIndex)
            {
                return i;
            }
        }
        return 0;
    }

    private void CalculatedWeight()
    {
        _accumulatedWeight = 0f;
        foreach (var enemy in _enemies)
        {
            _accumulatedWeight += enemy.percentage;
            enemy._weight = _accumulatedWeight;
        }

    }

}
