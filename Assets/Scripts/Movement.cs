using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Globals;

public class Movement : MonoBehaviour
{
    internal float speed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float movex = 0;
        float movey = 0;

        if (Input.GetKey(Globals.KEY_UP))
        {
            movey += speed;
        }

        if (Input.GetKey(Globals.KEY_DOWN))
        {
            movey -= speed;
        }

        if (Input.GetKey(Globals.KEY_LEFT))
        {
            movex -= speed;
        }

        if (Input.GetKey(Globals.KEY_RIGHT))
        {
            movex += speed;
        }

        var move = new Vector3(movex, movey, 0);
        transform.position += move * Time.deltaTime;
    }
}
