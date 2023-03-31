using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class KillPlayer_Doll : MonoBehaviour
{
    [SerializeField] private float _time;
    [SerializeField] private Text _timerText;
    [SerializeField] private AudioSource _shootSound;
    [SerializeField] private GameObject _lineWin;
    private Animator _animator;
    private float _timeLeft = 0f;
    private int _animIDDeath;
    private int _animIDSpeed;
    private bool _kill = false;
    private bool _scan = false;


    void Awake()
    {
        _animator = GetComponent<Animator>();
        _animIDDeath = Animator.StringToHash("Kill");
        _animIDSpeed = Animator.StringToHash("Speed");
        Sound_Doll.Instance.SubscribeToScanning(() =>
        {  Task.Run(() =>
            {
                Thread.Sleep(1000);
                _scan = true;
            }
            ); 
        });
        Sound_Doll.Instance.SubscribeToRepeatSound(() =>
        _scan = false);
    }

    void Start()
    {
        _timeLeft = _time;
        StartCoroutine(StartTimer());
    }

    private IEnumerator StartTimer()
    {
        _timeLeft -= 1;
        yield return new WaitForSeconds(1);
        Countdown();
        if (_timeLeft > 0)
        {
            StartCoroutine(StartTimer());
        }
        else
        {
            if (!_kill)
            {
                _shootSound.PlayOneShot(_shootSound.clip);
                _kill = true;
                _animator.SetBool(_animIDDeath, _kill);
            }
        }
        
    }


    private void ScanningMotion()
    {
        if (_scan)
        {
            if ((_animator.GetFloat(_animIDSpeed) > 0.001f || transform.position.y > 0) & !_kill & transform.position.x < _lineWin.transform.position.x)
            {
                _shootSound.PlayOneShot(_shootSound.clip);
                _kill = true;
                _animator.SetBool(_animIDDeath, _kill);
            }
        }

    }

    private void Countdown()
    {
        //float minutes = Mathf.FloorToInt(_timeLeft / 60);
        float seconds = Mathf.FloorToInt(_timeLeft % 60);
        _timerText.text = string.Format("{0:00}", /*minutes,*/ seconds);
    }

    void Update()
    {
        ScanningMotion();
    }
}
