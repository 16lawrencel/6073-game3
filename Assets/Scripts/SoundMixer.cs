using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTesting : MonoBehaviour
{

    [HideInInspector]
    public AudioSource bgm;

    public AudioClip track1;
    public AudioClip track2;
    public AudioClip test;

    // Start is called before the first frame update
    void Start()
    {
        bgm = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (bgm.mute) bgm.mute = false;
            else bgm.mute = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            int playtime = bgm.timeSamples;
            bgm.clip = track1;
            bgm.timeSamples = playtime;
            bgm.Play();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            int playtime = bgm.timeSamples;
            bgm.clip = track2;
            bgm.timeSamples = playtime;
            bgm.Play();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            bgm.PlayOneShot(test);
        }
    }
}

