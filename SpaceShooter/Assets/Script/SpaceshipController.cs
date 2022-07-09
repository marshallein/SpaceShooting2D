using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SpaceshipController : MonoBehaviour
{
    public float speed = 5.0f;
    private Dictionary<string, Action> keywordAction = new Dictionary<string, Action>();
    private KeywordRecognizer keywordRecognizer;
    private string command = "pending";
    public float max_Y = 3.75f;
    public float min_Y = -3.75f;

    void Start()
    {
        keywordAction.Add("move down", MoveDown);
        keywordAction.Add("move up", MoveUp);
        keywordRecognizer = new KeywordRecognizer(keywordAction.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += OnKeyWordsReconized;
        keywordRecognizer.Start();
    }

    private void OnKeyWordsReconized(PhraseRecognizedEventArgs args)
    {
        Debug.Log("Keyword: " + args.text);
        keywordAction[args.text].Invoke();
    }

    private void MoveDown()
    {
        command = "move down";
    }

    private void MoveUp()
    {
        command = "move up";
    }

    void Update()
    {
        MoveSpaceship();
    }

    void MoveSpaceship()
    {
        switch (command)
        {
            case "move up":
                Vector3 temp = transform.position;
                temp.y += speed * Time.deltaTime;
                if(temp.y > max_Y)
                {
                    temp.y = max_Y; 
                }
                transform.position = temp;
                //command = "pending";
                break;
            case "move down":
                Vector3 temp1 = transform.position;
                temp1.y -= speed * Time.deltaTime;
                if (temp1.y < min_Y)
                {
                    temp1.y = min_Y;
                }
                transform.position = temp1;
                //command = "pending";
                break;
        }
    }
}
