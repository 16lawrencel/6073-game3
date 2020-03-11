using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float startTime;
    internal float currentTime;

    public Text timerText;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = Math.Max(0f, currentTime - Time.deltaTime);

        if (currentTime == 0f)
        {
            // set boss
            GameFlow.Instance.SetBoss();
            Destroy(gameObject);
        }

        timerText.text = "Time left: " + GetTimeString(currentTime);
    }

    private string GetTimeString(float time)
    {
        string minutes = ((int)(time / 60)).ToString("00");
        string seconds = ((int)(time % 60)).ToString("00");
        return minutes + ":" + seconds;
    }
}
