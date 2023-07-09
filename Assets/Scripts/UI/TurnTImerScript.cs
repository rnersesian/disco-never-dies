using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnTImerScript : MonoBehaviour

{
    public Dancing danceManager;


    public Image left;
    public Image right;
    public Text turnLabel;
    
    public float timerDuration = 2f;
    public float timer = 0f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= timerDuration) timer -= timerDuration;
        timer += Time.deltaTime;


        float amount = 1f - timer / timerDuration;
        left.fillAmount = amount;
        right.fillAmount = amount;

        if (danceManager.playerTurn)
        {
            turnLabel.text = "Your Turn";
        } else
        {
            turnLabel.text = "Rival's Turn";
        }
    }
}
