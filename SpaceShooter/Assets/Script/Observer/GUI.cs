using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;
    private int _enemyBombScore = 10;
    private int _enemyMiniScore = 5;
    private int _score;

    private void Awake()
    {
        _score = 0;
        MiniEnemyController.MiniEnemyDestroyed += OnMiniEnemyDestroyed;
        BombEnemyController.BombEnemyDestroyed += OnBombEnemyDestroyed;
    }

    private void OnBombEnemyDestroyed(object sender, EventArgs e)
    {
        _score += _enemyBombScore;
        UpdateUI();
    }

    private void OnMiniEnemyDestroyed(object sender, EventArgs e)
    {
        _score += _enemyMiniScore;
        UpdateUI();
    }

    void Start()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        scoreText.text = _score.ToString();
    }
}
