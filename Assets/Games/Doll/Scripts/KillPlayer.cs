using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class KillPlayer : MonoBehaviour
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
        Sound.Instance.SubscribeToScanning(async () =>
        {
            await Task.Run(() =>
            {
                Thread.Sleep(1000);
                _scan = true;
            }
            ); 
        });
        Sound.Instance.SubscribeToRepeatSound(() =>
        _scan = false);
        _timeLeft = _time;
        StartCoroutine(StartTimer());
    }

    private IEnumerator StartTimer()
    {
        Countdown();
        yield return new WaitForSeconds(1);
        _timeLeft -= 1;
        if (_timeLeft > 0)
        {
            StartCoroutine(StartTimer());
        }
        else
        {
            _shootSound.PlayOneShot(_shootSound.clip);
            _kill = true;
            _animator.SetBool(_animIDDeath, _kill);
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
        float minutes = Mathf.FloorToInt(_timeLeft / 60);
        float seconds = Mathf.FloorToInt(_timeLeft % 60);
        _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void Update()
    {
        ScanningMotion();
    }
}
