using System;
using System.Collections;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class AllGameLogic_BlockParty : MonoBehaviour
{
    [SerializeField] private GameObject _prefabBlock;
    [SerializeField] private Material[] _colors;
    [SerializeField] private Text _timerText;
    [SerializeField] private float _timeBeforeDestroy = 10;
    [SerializeField] private GameObject _colorUI;
    [SerializeField] private GameObject _player;
    private float _timeLeft;
    private GameObject[,] _platforms;
    private Color _rightColor;
    private int _sizeMatrixPlatform = 12;
    private Menu _menu;

    void Awake()
    {
        _menu = new Menu();
        _menu.CreateMenu();
    }
    

    void Start()
    {
        StartNewRound();
        StartCoroutine(CheckPositionAndKill());
    }

    private IEnumerator CountdownToDestroy()
    {
        if (_timeLeft < 0)
        {
            DestroyPlatformsByColor();
            StartCoroutine(WaitSecondsAndRun(4, () => StartNewRound()));
            
        }
        else
        {
            float seconds = Mathf.FloorToInt(_timeLeft % 60);
            _timerText.text = string.Format("{0:0}", seconds);
            _timeLeft -= 1;
            yield return new WaitForSeconds(1);
            StartCoroutine(CountdownToDestroy());
        }
    }

   


    private IEnumerator WaitSecondsAndRun(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
        action();
    }

    private void CreatePlatforms()
    {
        _platforms = new GameObject[_sizeMatrixPlatform, _sizeMatrixPlatform];
        for (int i = 0; i < _sizeMatrixPlatform; i++)
        {
            for (int j = 0; j < _sizeMatrixPlatform; j++)
            {
                _platforms[i, j] = Instantiate(_prefabBlock, new Vector3(i * 3, 3, j * 3), Quaternion.Euler(0, 0, 0));
                _platforms[i, j].GetComponent<MeshRenderer>().material.color = _colors[Random.Range(0, _colors.Length)].color;
                //platform.AddComponent<MyCollisionDetector>();
            }
        }
    }


    private void StartNewRound()
    {
        DestroyAllPlatforms();
        ChooseRightColor();
        _timeLeft = _timeBeforeDestroy;
        if (_timeBeforeDestroy > 2) _timeBeforeDestroy--;
        CreatePlatforms();
        StartCoroutine(CountdownToDestroy());
    }

    private void ChooseRightColor()
    {
        _rightColor = _colors[Random.Range(0, _colors.Length)].color;
        _colorUI.GetComponent<Image>().color = _rightColor;
    }

    private void DestroyPlatformsByColor()
    {
        for (int i = 0; i < _sizeMatrixPlatform; i++)
        {
            for (int j = 0; j < _sizeMatrixPlatform; j++)
            {
                if (_platforms[i, j].GetComponent<MeshRenderer>().material.color != _rightColor)
                {
                    Destroy(_platforms[i, j]);
                }
            }
        }
    }

    private void DestroyAllPlatforms()
    {
        if (_platforms != null)
        {
            for (int i = 0; i < _sizeMatrixPlatform; i++)
            {
                for (int j = 0; j < _sizeMatrixPlatform; j++)
                {
                    Destroy(_platforms[i, j]);
                }
            }
        }
    }

    private IEnumerator CheckPositionAndKill()
    {
        if (_player != null)
        {
            float y = _player.transform.position.y;
            yield return new WaitForSeconds(2);
            if (_player != null)
            {
                if (_player.transform.position.y + _player.transform.localScale.y * 3 < y)
                {
                    Destroy(_player);
                    _menu.ShowMenu();
                }
            }
            StartCoroutine(CheckPositionAndKill());
        }
    }
}
