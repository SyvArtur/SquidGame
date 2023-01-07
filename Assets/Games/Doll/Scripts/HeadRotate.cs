using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class HeadRotate : MonoBehaviour
{

    private GameObject head;

    [HideInInspector] public Vector3 rotateTo = new Vector3();

    [SerializeField] private int _speedRotate = 1;

    [HideInInspector] private bool _rotating = false;


    void Awake()
    {
        head = gameObject;
        Sound.Instance.SubscribeToRepeatSound(RotateAwayFromPlayer);
        Sound.Instance.SubscribeToScanning(RotateTOPlayer);
    }

    private void RotateAwayFromPlayer()
    {
        rotateTo.y = 180;
        _rotating = true;
    }

    private void RotateTOPlayer()
    {
        rotateTo.y = 0;
        _rotating = true;
    }

    private void Rotate()
    {
        if (_rotating)
        {
            if (Vector3.SqrMagnitude(transform.eulerAngles - rotateTo) > 0.01f)
            {
                head.transform.eulerAngles = Vector3.Lerp(head.transform.rotation.eulerAngles, rotateTo, Time.deltaTime * 4);
            }
            else
            {
                transform.eulerAngles = rotateTo;
                _rotating = false;
            }
        }
    }

    public void Update()
    {
        Rotate();
        
    }



}
