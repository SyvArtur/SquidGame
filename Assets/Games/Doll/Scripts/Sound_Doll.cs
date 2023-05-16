using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Sound_Doll : NetworkBehaviour
{
/*    private static Sound_Doll instance;
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
    }*/

    [SerializeField] private ClientLogic_Doll ClientLogic;

    private RepeatSound repeatSound;
    private Scan scanning;

    private delegate void RepeatSound();
    private delegate void Scan();

    private float timeForTimer;
    void Awake()
    {
        //_kukulaku = GetComponent<AudioSource>();
        /*if (isServer)
        {
            repeatSound += () =>
            {
                timeForTimer = _kukulaku.clip.length;
                RpcPlaySound();
            };
        }*/
    }

    IEnumerator Start()
    {
        if (isServer)
        {
            repeatSound += () =>
            {
                timeForTimer = ClientLogic._kukulaku.clip.length;
                ClientLogic.RpcPlaySound();
            };

            while (!MyNetworkManager.allClientsReady)
            {
                yield return new WaitForEndOfFrame();
            }
            StartCoroutine(InitializeWithDelay());
        }
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

    private bool soundRepeat = true;

    private IEnumerator StartTimer()
    {
        if (timeForTimer < -0.8)
        {
            repeatSound?.Invoke();
            soundRepeat = true;
        }

        else
        {
            if (timeForTimer < 4.1 /*3.9*/ & soundRepeat)
            {
                scanning?.Invoke();
                soundRepeat = false;
            }
        }
        timeForTimer -= 0.1f;
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(StartTimer());
    }


}
