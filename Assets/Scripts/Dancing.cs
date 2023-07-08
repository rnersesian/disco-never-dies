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
    private Dictionary<KeyCode,int> DanceMoves = new Dictionary<KeyCode,int>();


    // Start is called before the first frame update
    void Start()
    {
        DanceMoves.Add(KeyCode.UpArrow, 0);
        DanceMoves.Add(KeyCode.LeftArrow, 1);
        DanceMoves.Add(KeyCode.DownArrow, 2);
        DanceMoves.Add(KeyCode.RightArrow, 3);
    }

    // Update is called once per frame
    void Update()
    {

        turnTimer += Time.deltaTime;
        if (turnTimer > turnDuration) changeTurn();

        if (playerTurn)
        {
            // Save Player's moves 
            foreach(KeyValuePair<KeyCode, int> _move in DanceMoves)
            {
                if (Input.GetKeyDown(_move.Key)) {
                    GameObject.Instantiate(moves[_move.Value], PlayerSpawnPosition, transform.rotation);
                    moveHistory.Add(Tuple.Create(turnTimer, _move.Value));
                    break;
                }
            }
        } else
        {
            //IA repeats the player's moves
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
