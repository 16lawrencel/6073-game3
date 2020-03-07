using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyElephant : EnemyAI
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        SetSpeed(Globals.ENEMY_ELEPHANT_SPEED);
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        if (player != null)
        {
            MoveTowards(player.transform.position);
        }
    }
}
