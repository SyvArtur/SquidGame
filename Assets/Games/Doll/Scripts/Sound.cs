using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Sound : MonoBehaviour
{
    private static Sound instance;
    public static Sound Instance
    {
        get
        {
            
            if (instance == null)
                instance = FindObjectOfType(typeof(Sound)) as Sound;

            return instance;
        }
        set
        {
            instance = value;
            
        }
    }

    private float time;
    private AudioSource kukulaku;

    private RepeatSound repeatSound;
    private Scan scanning;


    void Awake()
    {
        kukulaku = GetComponent<AudioSource>();

        SubscribeToRepeatSound (() =>
        {
            kukulaku.Play();
            time = kukulaku.clip.length;
        });


        repeatSound?.Invoke();


        StartCoroutine(StartTimer());

    }

    void Start()
    {
        time = time - 0.8f;
    }

    public void SubscribeToRepeatSound(Action action)
    {
        repeatSound += new RepeatSound(action);
    }

    public void SubscribeToScanning(Action action)
    {
        scanning += new Scan(action);
    }

    private bool b = true;
    private IEnumerator StartTimer()
    {
        if (time < -0.8)
        {
            repeatSound?.Invoke();
            b = true;
        }

        else
        {
            if (time < 3.9 & b)
            {
                scanning?.Invoke();
                b = false;
            }
        }
        time -= 0.1f;
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(StartTimer());
    }

    private delegate void RepeatSound();
    private delegate void Scan();

    void Update()
    {
        
    }

}
