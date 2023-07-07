using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Dancing : MonoBehaviour
{
    public GameObject[] moves;
    public Vector2 PlayerSpawnPosition = new(3,0);
    public Vector2 EnemySpawnPosition = new(-3,0);

    public bool playerTurn = true;
    public float turnDuration = 2f;
    public float turnTimer = 0f;

    public List<Tuple<float,int>> moveHistory = new List<Tuple<float,int>>();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        turnTimer += Time.deltaTime;
        if (turnTimer > turnDuration) changeTurn();

        if (playerTurn)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                GameObject.Instantiate(moves[0], PlayerSpawnPosition, transform.rotation);
                moveHistory.Add(Tuple.Create(turnTimer, 0));
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                GameObject.Instantiate(moves[1], PlayerSpawnPosition, transform.rotation);
                moveHistory.Add(Tuple.Create(turnTimer, 1));
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                GameObject.Instantiate(moves[2], PlayerSpawnPosition, transform.rotation);
                moveHistory.Add(Tuple.Create(turnTimer, 2));
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                GameObject.Instantiate(moves[3], PlayerSpawnPosition, transform.rotation);
                moveHistory.Add(Tuple.Create(turnTimer, 3));
            }
        } else
        {
            if (moveHistory.Count > 0)
            {
                if (moveHistory[0].Item1 < turnTimer)
                {
                    GameObject.Instantiate(moves[moveHistory[0].Item2], EnemySpawnPosition, transform.rotation);
                    moveHistory.RemoveRange(0, 1);
                }
            }
        }
    }

    public void changeTurn()
    {
        turnTimer = 0;
        if (!playerTurn)
        {
            moveHistory = new List<Tuple<float, int>>();
        }
        playerTurn = !playerTurn;
    }
}
