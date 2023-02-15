using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class HeadRotate_Doll : MonoBehaviour
{

    private GameObject _head;

    [HideInInspector] public Vector3 _rotateTo = new Vector3();

    [SerializeField] private int _speedRotate = 1;

    [HideInInspector] private bool _rotating = false;


    void Awake()
    {
        _head = gameObject;
        Sound_Doll.Instance.SubscribeToRepeatSound(RotateAwayFromPlayer);
        Sound_Doll.Instance.SubscribeToScanning(RotateTOPlayer);
    }

    private void RotateAwayFromPlayer()
    {
        _rotateTo.y = 180;
        _rotating = true;
    }

    private void RotateTOPlayer()
    {
        _rotateTo.y = 0;
        _rotating = true;
    }

    private void Rotate()
    {
        if (_rotating)
        {
            if (Vector3.SqrMagnitude(transform.eulerAngles - _rotateTo) > 0.01f)
            {
                _head.transform.eulerAngles = Vector3.Lerp(_head.transform.rotation.eulerAngles, _rotateTo, Time.deltaTime * 4);
            }
            else
            {
                transform.eulerAngles = _rotateTo;
                _rotating = false;
            }
        }
    }

    public void Update()
    {
        Rotate();
        
    }



}
