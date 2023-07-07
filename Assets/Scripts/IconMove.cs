using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconMove : MonoBehaviour
{
    public float duration = 5f;
    public float timer = 0f;
    public float speed = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        transform.Translate(speed * Time.deltaTime * Vector2.up);
        if (timer > duration)
        {
            Destroy(gameObject);
        }
    }
}
