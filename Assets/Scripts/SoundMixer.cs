using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMixer : MonoBehaviour
{

    public static AudioSource bgm;
    public static AudioSource soundeffect;
    public static AudioSource gun;

    public AudioClip track1;
    public AudioClip track2;

    public AudioClip Player_Damage;
    public AudioClip Shoot_Bubble;
    public AudioClip Rev_Up;
    public AudioClip Rev_Down;
    public AudioClip Rev;
    public AudioClip Powerup;
    public AudioClip Splash;

    public static readonly Dictionary<string, AudioClip> sounds = new Dictionary<string, AudioClip>();


    // Start is called before the first frame update
    void Start()
    {

        bgm = this.gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        soundeffect = this.gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        gun = this.gameObject.AddComponent(typeof(AudioSource)) as AudioSource;

        bgm.loop = true;
        bgm.clip = track2;
        bgm.Play();
        gun.volume = 0.2f;
        gun.loop = true;

        sounds.Add("Player_Damage", Player_Damage);
        sounds.Add("Shoot_Bubble", Shoot_Bubble);
        sounds.Add("Rev_Up", Rev_Up);
        sounds.Add("Rev_Down", Rev_Down);
        sounds.Add("Rev", Rev);
        sounds.Add("Powerup", Powerup);
        sounds.Add("Splash", Splash);

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
    }

/*    public static void PlaySound(AudioSource source, string sound, float volume = 1.0f)
    {
        source.PlayOneShot(sounds[sound], volume);
    }*/

}

