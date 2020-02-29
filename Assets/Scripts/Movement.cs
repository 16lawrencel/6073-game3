using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Globals;

public class Movement : MonoBehaviour
{
    public float speed = 6f;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
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

        Vector2 move = new Vector2(movex, movey);
        rb.velocity = move;
    }
}
