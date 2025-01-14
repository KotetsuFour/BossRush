using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleEffect : MonoBehaviour
{
    private bool usingTimer;
    private float timer;
    public void playTimed(float time)
    {
        usingTimer = true;
        timer = time;
        GetComponent<ParticleSystem>().Play();
    }
    public void play()
    {
        GetComponent<ParticleSystem>().Play();
    }
    public void stop()
    {
        GetComponent<ParticleSystem>().Stop();
    }
    // Update is called once per frame
    void Update()
    {
        if (usingTimer)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                usingTimer = false;
                GetComponent<ParticleSystem>().Stop();
            }
        }
    }
}
