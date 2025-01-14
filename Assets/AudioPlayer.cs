using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    private bool usingTimer;
    private float timer;
    public void playOnce(AudioClip clip)
    {
        GetComponent<AudioSource>().clip = clip;
        timer = clip.length;
        usingTimer = true;
        GetComponent<AudioSource>().Play();
    }
    public void playTimed(AudioClip clip, float time)
    {
        GetComponent<AudioSource>().clip = clip;
        timer = Mathf.Min(time, clip.length);
        usingTimer = true;
        GetComponent<AudioSource>().Play();
    }
    public void playContinual(AudioClip clip)
    {
        GetComponent<AudioSource>().clip = clip;
        usingTimer = false;
        GetComponent<AudioSource>().loop = true;
        GetComponent<AudioSource>().Play();
    }
    // Update is called once per frame
    void Update()
    {
        if (usingTimer)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
