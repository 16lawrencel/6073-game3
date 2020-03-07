using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Globals
{
    public static KeyCode   KEY_UP = KeyCode.W;
    public static KeyCode   KEY_LEFT = KeyCode.A;
    public static KeyCode   KEY_DOWN = KeyCode.S;
    public static KeyCode   KEY_RIGHT = KeyCode.D;
    public static float     BULLET_SPEED = 30f;
    public static float     ENEMY_ELEPHANT_SPEED = 1f;
    public static float     ENEMY_SHOOTER_SPEED = 2f;
    public static int       ENEMY_SHOOTER_NEW_DESTINATION_SECONDS = 2;
    public static float     ENEMY_SHOOTER_NEW_BULLET_SECONDS = 3f;
    public static float     ENEMY_SHOOTER_BULLET_SPEED = 15f;


    public static int       WALL_LAYER = 8;
    public static int       MINIMAP_LAYER = 9;
}
