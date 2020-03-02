using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Globals;

public class Movement : MonoBehaviour
{
    public float speed = 15f;
    internal Rigidbody2D rigidbody;
    internal int stuncounter;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        stuncounter = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (stuncounter > 0)
		{
            stuncounter--;
            return;
		}

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
        rigidbody.velocity = move;
    }

    public void SetStunned()
	{
        stuncounter = 30;
        rigidbody.velocity = new Vector2(0, 0);
	}
}
