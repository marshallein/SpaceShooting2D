using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI : MonoBehaviour
{
    private Text scoreText;
    private int _enemyBombScore = 10;
    private int _enemyMiniScore = 5;
    public static int _score;

    private void Awake()
    {
        var _gui = GameObject.FindGameObjectWithTag("GUI");
        scoreText = _gui.transform.Find("ScoreText").GetComponent<Text>();
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
