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
    private Dictionary<int, string> danceAnimations = new Dictionary<int, string>();

    public GameObject player;
    public GameObject enemy;

    private int danceIter = 0;


    // Start is called before the first frame update
    void Start()
    {
        DanceMoves.Add(KeyCode.UpArrow, 0);
        danceAnimations.Add(0, "Discomec_haut");

        DanceMoves.Add(KeyCode.LeftArrow, 1);
        danceAnimations.Add(1, "Discomec_gauche");


        DanceMoves.Add(KeyCode.DownArrow, 2);
        danceAnimations.Add(2, "Discomec_bas");


        DanceMoves.Add(KeyCode.RightArrow, 3);
        danceAnimations.Add(3, "Discomec_droite");

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
            //enemy.GetComponent<Animator>().Play("Discomec_droite");

            if (moveHistory.Count > 0)
            {
                Debug.Log(danceIter);
                if (danceIter < moveHistory.Count && moveHistory[danceIter].Item1 < turnTimer)
                {
                    GameObject.Instantiate(moves[moveHistory[danceIter].Item2], EnemySpawnPosition, transform.rotation);
                    danceIter++;
                }
            }
        }
    }

    public void changeTurn()
    {
        turnTimer = 0;
        danceIter = 0;
        if (!playerTurn)
        {
            moveHistory = new List<Tuple<float, int>>();
            enemy.GetComponent<PlayerAnimation>().dance(danceAnimations, moveHistory);

        } else
        {
            enemy.GetComponent<PlayerAnimation>().dance(danceAnimations, moveHistory);
        }
        playerTurn = !playerTurn;
    }
}
