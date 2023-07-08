using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metronome : MonoBehaviour
{

    public float bpm;
    public float maxRadius = 3f;
    public float radius = 0f;

    public float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        transform.localScale = Vector3.one * maxRadius * Mathf.Abs(Mathf.Sin(timer/Mathf.PI * 10*bpm/60));
    }
}
