using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    [SerializeField]
    private Text _timerText;
    private float secondsCount;

    void Update()
    {
        secondsCount += Time.deltaTime;
        _timerText.text = ((int)secondsCount).ToString() + "s";
    }
}
