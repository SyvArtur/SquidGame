using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Sound_Doll : MonoBehaviour
{
    private static Sound_Doll instance;
    public static Sound_Doll Instance
    {
        get
        {
            
            if (instance == null)
                instance = FindObjectOfType(typeof(Sound_Doll)) as Sound_Doll;

            return instance;
        }
        private set
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
        repeatSound += () =>
        {
            time = kukulaku.clip.length;
            kukulaku.Play();
        };
    }

    void Start()
    {
        StartCoroutine(InitializeWithDelay());
    }

    private IEnumerator InitializeWithDelay() {
        yield return new WaitForSeconds(2);

        repeatSound?.Invoke();

        StartCoroutine(StartTimer());
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
