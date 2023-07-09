using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator anim;
    public float animationSpeed = 1f;
    private float animDuration = 0.2f;


    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //anim.Play("Discomec_bassin");
    }

    public void dance(Dictionary<int, string> anims, List<Tuple<float, int>> moves)
    {
        StartCoroutine(coDance(anims, moves));
    }

    public IEnumerator coDance(Dictionary<int, string> anims, List<Tuple<float, int>> moves)
    {


        if (moves.Count > 0)
        {
            yield return new WaitForSeconds(moves[0].Item1);

            Debug.Log(moves[0].Item2);
            List<Tuple<float, int>> movesWithDuration = new List<Tuple<float, int>>();

            for (int i = 0; i < moves.Count - 1; i++)
            {
                movesWithDuration.Add(Tuple.Create(moves[i + 1].Item1 - moves[i].Item1, moves[i].Item2));
            }

            foreach (Tuple<float, int> _move in  movesWithDuration)
            {
                float realAnimSpeed = animationSpeed / _move.Item1;
                anim.speed = realAnimSpeed;
                anim.Play(anims[_move.Item2], 0);
                yield return new WaitForSeconds(_move.Item1);
            }
            anim.speed = 1;
            anim.Play(anims[moves[moves.Count - 1].Item2]);
        } else
        {
            anim.speed = 1;
            anim.Play("Discomec_EnPosition");
        }
        
            
    }
}
