using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpaceshipHealth : MonoBehaviour
{
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public int curent_Health = 3;
    void Start()
    {
        SpaceshipStateControl.SpaceshipTakeDamage += OnSpaceshipTakeDamege;
    }

    private void OnSpaceshipTakeDamege(object sender, EventArgs e)
    {
        curent_Health -= 1;
        //Debug.Log(curent_Health);
        if (curent_Health <= 0)
        {
            heart1.gameObject.SetActive(false);
            var spaceship = sender as SpaceshipStateControl;
            var _gui = GameObject.FindGameObjectWithTag("GUI");
            var gameovermenu = _gui.transform.Find("GameOverMenu");
            gameovermenu.gameObject.SetActive(true);
            string score = GUI._score.ToString();
            gameovermenu.transform.Find("Point").GetComponent<Text>().text += score;
            Time.timeScale = 0;

        }
        if(curent_Health == 2)
        {
            heart3.gameObject.SetActive(false);
        }
        if (curent_Health == 1)
        {
            heart2.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        
    }
}
