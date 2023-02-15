using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class KeyEvents_UltimateKnockout : MonoBehaviour
{
    [SerializeField] private Text _timerText;
    [SerializeField] private float _timeToRemember = 13f;
    [SerializeField] private GameObject _player;

    private float _timeLeft;

    private static KeyEvents_UltimateKnockout instance;
    public static KeyEvents_UltimateKnockout Instance
    {
        get
        {

            if (instance == null)
                instance = FindObjectOfType(typeof(KeyEvents_UltimateKnockout)) as KeyEvents_UltimateKnockout;

            return instance;
        }
        set
        {
            instance = value;

        }
    }

    private delegate void NewRound();
    private delegate void ChoosePlatformForPlayer();
    private delegate void Revising();

    private NewRound newRound;
    private ChoosePlatformForPlayer choosePlatformForPlayer;
    private Revising revising;

    public void SubscribeToRevising(Action action)
    {
        revising += new Revising(action);
    }

    public void SubscribeToNewRound(Action action)
    {
        newRound += new NewRound(action);
    }

    public void SubscribeToChoosePlatformForPlayer(Action action)
    {
        choosePlatformForPlayer += new ChoosePlatformForPlayer(action);
    }

    void Awake()
    {
        SubscribeToRevising(() => Debug.Log("OKAY"));

        SubscribeToChoosePlatformForPlayer(() => {
            StartCoroutine(WaitSecondsAndRun(Platforms._timeBeforeDestroy + 4f, StartNewRound));
        });

        SubscribeToNewRound(() => {
                _timeLeft = _timeToRemember;
                if (_timeToRemember > 2) _timeToRemember--;
                StartCoroutine(CountdownToChooseRightMaterial());
            });
      
        
/*        KeyEvents.getInstance().SubscribeNewRound(DestroyAllPlatforms);
        KeyEvents.getInstance().SubscribeNewRound(CreatePlatforms);*/
    }

    private void StartNewRound()
    {
        revising();
        StartCoroutine(WaitSecondsAndRun(3.7f, () => newRound()));
    }

    void Start()
    {
        StartNewRound();
        //StartCoroutine(CheckPositionAndKill());
    }

    
    private IEnumerator CountdownToChooseRightMaterial()
    {
        if (_timeLeft < 0)
        {
            choosePlatformForPlayer();
            //StartCoroutine(WaitSecondsAndRun(4, () => StartNewRound()));
        }
        else
        {
            float seconds = Mathf.FloorToInt(_timeLeft % 60);
            _timerText.text = string.Format("{0:0}", seconds);
            _timeLeft -= 1;
            yield return new WaitForSeconds(1);
            StartCoroutine(CountdownToChooseRightMaterial());
        }
    }


    private IEnumerator WaitSecondsAndRun(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
        action();
    }

    
    /*private IEnumerator CheckPositionAndKill()
    {
        if (_player != null)
        {
            float y = _player.transform.position.y;
            yield return new WaitForSeconds(3);
            if ((_player.transform.position.y + _player.transform.localScale.y * 3 < y))
            {
                Destroy(_player);
                Camera.main.GetComponent<CinemachineBrain>().enabled = false;
                Camera.main.transform.position = new Vector3(15,30,6);
                Camera.main.transform.rotation = Quaternion.Euler(68f, 270f, 0);
            }
            else
            {
                StartCoroutine(CheckPositionAndKill());
            }
        }
    }*/
}
