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
    public float upper_Y = 3.75f;
    public float lower_Y = -3.75f;
    public float mid_Y = 0f;
    private float pivot;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private Transform bullet_spawn;
    public float attack_Timer = 0.35f;
    private float current_Attack_Timer;
    private bool canAttack;
    void Start()
    {
        current_Attack_Timer = attack_Timer;
        keywordAction.Add("move down", MoveDown);
        keywordAction.Add("move up", MoveUp);
        keywordAction.Add("shoot", Shoot);
        keywordRecognizer = new KeywordRecognizer(keywordAction.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += OnKeyWordsReconized;
        keywordRecognizer.Start();
    }

    private void Shoot()
    {
        command = "shoot";
    }

    private void OnKeyWordsReconized(PhraseRecognizedEventArgs args)
    {
        Debug.Log("Keyword: " + args.text);
        keywordAction[args.text].Invoke();
    }

    private void MoveDown()
    {
        command = "move down";
        Vector3 temp = transform.position;
        pivot = temp.y;
    }

    private void MoveUp()
    {
        command = "move up";
        Vector3 temp = transform.position;
        pivot = temp.y;
    }

    void Update()
    {
        attack_Timer += Time.deltaTime;
        if (attack_Timer > current_Attack_Timer)
        {
            canAttack = true;
        }
        SpaceshipCommand();
        Debug.Log(command);
    }

    float GetLimit()
    {
        if (command == "move up" && (pivot == mid_Y || pivot == upper_Y))
        {
            return upper_Y;
        }
        else if (command == "move up" && pivot == lower_Y)
        {
            return mid_Y;
        }
        else if (command == "move down" && pivot == upper_Y)
        {
            return mid_Y;
        }
        else if (command == "move down" && (pivot == mid_Y || pivot == lower_Y))
        {
            return lower_Y;
        }
        return float.MaxValue;
    }

    void SpaceshipCommand()
    {
        switch (command)
        {
            case "move up":
                Vector3 temp = transform.position;
                temp.y += speed * Time.deltaTime;
                float limit = GetLimit();
                if (temp.y > limit)
                {
                    temp.y = limit;
                    command = "pending";
                }
                transform.position = temp;
                break;
            case "move down":
                Vector3 temp1 = transform.position;
                temp1.y -= speed * Time.deltaTime;
                float limit1 = GetLimit();
                if (temp1.y < limit1)
                {
                    temp1.y = limit1;
                    command = "pending";
                }
                transform.position = temp1;
                break;
            case "shoot":
                if (canAttack)
                {
                    canAttack = false;
                    attack_Timer = 0f;
                    Instantiate(bullet, bullet_spawn.position, Quaternion.identity);
                    command = "pending";
                }
                break;
        }
    }
}
