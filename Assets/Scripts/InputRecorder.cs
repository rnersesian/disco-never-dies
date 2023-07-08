using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using UnityEngine.UIElements;
using UnityEditor.Profiling.Memory.Experimental;
using System.Linq;

public class InputRecorder : MonoBehaviour
{
    public Text countdown;
    public float cdDuration = 3f;
    public float cdTimer;

    public GameObject[] moves;


    public float timer;
    public string fileName;

    public Metronome metronome;
    public AudioSource musicPlayer;

    public List<Tuple<float, int>> moveHistory = new List<Tuple<float, int>>();
    private Dictionary<KeyCode, int> DanceMoves = new Dictionary<KeyCode, int>();

    // Start is called before the first frame update
    void Start()
    {
        DanceMoves.Add(KeyCode.UpArrow, 0);
        DanceMoves.Add(KeyCode.LeftArrow, 1);
        DanceMoves.Add(KeyCode.DownArrow, 2);
        DanceMoves.Add(KeyCode.RightArrow, 3);

        musicPlayer = GameObject.Find("MusicPlayer").GetComponent<AudioSource>();

        cdTimer = cdDuration;
        StartCoroutine(startCountDown());
    }

    // Update is called once per frame
    void Update()
    {
        if (metronome.isActive == false) return;

        timer += Time.deltaTime;

        foreach (KeyValuePair<KeyCode, int> _move in DanceMoves)
        {
            if (Input.GetKeyDown(_move.Key))
            {
                moveHistory.Add(Tuple.Create(timer, _move.Value));
                GameObject.Instantiate(moves[_move.Value], new Vector2(3,0), transform.rotation);

                break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            saveFile();
        }
    }

    private void saveFile()
    {
        string path = Application.dataPath + "/Levels/" + fileName + ".txt";
        if (!File.Exists(path))
        {

            List<string> content = new List<string>();
            foreach (Tuple<float, int> _move in moveHistory)
            {
                content.Add(_move.Item1.ToString() + "|" + _move.Item2.ToString());
            }
            foreach (string c in content)
            {
                Debug.Log(c);
            }
            File.WriteAllLines(path, content);
        }
    }

    IEnumerator startCountDown()
    {
        countdown.text = "3";
        yield return new WaitForSeconds(1f);
        countdown.text = "2";
        yield return new WaitForSeconds(1f);
        countdown.text = "1";
        yield return new WaitForSeconds(1f);
        countdown.text = "Let's Dance !";
        metronome.setMetronome(true);
        musicPlayer.enabled = true;
        yield return new WaitForSeconds(2f);
        countdown.gameObject.SetActive(false);
    }
}
