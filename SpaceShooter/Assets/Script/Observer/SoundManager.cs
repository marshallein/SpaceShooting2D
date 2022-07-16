using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip;

    void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        MiniEnemyController.MiniEnemyDestroyed += Play;
        BombEnemyController.BombEnemyDestroyed += Play;
    }

    void OnDestroy()
    {
        MiniEnemyController.MiniEnemyDestroyed -= Play;
        BombEnemyController.BombEnemyDestroyed -= Play;
    }

    public void Play(object sender, EventArgs e)
    {
        Debug.Log("Play sound");
        audioSource.PlayOneShot(audioClip);
    }

    void Update()
    {

    }
}
