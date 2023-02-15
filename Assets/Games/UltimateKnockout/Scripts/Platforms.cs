using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    [SerializeField] private GameObject _prefabBlock;
    [SerializeField] public static float _timeBeforeDestroy = 8f;

    private GameObject[,] _platforms;
    public static int _sizeMatrixPlatform = 5;

    /*private static Platforms instance;

    public static Platforms getInstance()
    {
        if (instance == null)
            instance = new Platforms();
        return instance;
    }*/

    void Awake()
    {
        KeyEvents_UltimateKnockout.Instance.SubscribeToChoosePlatformForPlayer(() => {
            ResetMaterialsFromPlatforms();
            StartCoroutine(CountdownToDestroyPlatforms());
        });

        KeyEvents_UltimateKnockout.Instance.SubscribeToRevising(() =>
        {
            DestroyAllPlatforms();
            CreatePlatforms();
        });

        KeyEvents_UltimateKnockout.Instance.SubscribeToNewRound(() =>
        {
            PaintPlatforms();
        });



        /*        KeyEvents.getInstance().SubscribeNewRound(DestroyAllPlatforms);
                KeyEvents.getInstance().SubscribeNewRound(CreatePlatforms);*/

    }


    private void ResetMaterialsFromPlatforms()
    {
        for (int i = 0; i < _sizeMatrixPlatform; i++)
        {
            for (int j = 0; j < _sizeMatrixPlatform; j++)
            {
                _platforms[i, j].GetComponent<MeshRenderer>().material = _prefabBlock.GetComponent<MeshRenderer>().sharedMaterial;
                //platform.AddComponent<MyCollisionDetector>();
            }
        }
    }


    private void CreatePlatforms()
    {
        _platforms = new GameObject[_sizeMatrixPlatform, _sizeMatrixPlatform];
        for (int i = 0; i < _sizeMatrixPlatform; i++)
        {
            for (int j = 0; j < _sizeMatrixPlatform; j++)
            {
                _platforms[i, j] = Instantiate(_prefabBlock, new Vector3(i * 3.4f, 0, j * 3.4f), Quaternion.Euler(0, 0, 0));
                //platform.AddComponent<MyCollisionDetector>();
            }
        }
    }

    private void PaintPlatforms()
    {
        for (int i = 0; i < _sizeMatrixPlatform; i++)
        {
            for (int j = 0; j < _sizeMatrixPlatform; j++)
            {
                _platforms[i, j].GetComponent<MeshRenderer>().material = MaterialActions._materials[j + i * _sizeMatrixPlatform];
            }
        }
    }


    private void DestroyPlatformsByMaterial()
    {
        for (int i = 0; i < _sizeMatrixPlatform; i++)
        {
            for (int j = 0; j < _sizeMatrixPlatform; j++)
            {
                /*if (!Equals(_platforms[i, j].GetComponent<MeshRenderer>().material.name, _screens.GetComponent<MeshRenderer>().material.name))
                {
                    Destroy(_platforms[i, j]);
                }*/
                if (!Equals(MaterialActions._materials[i * _sizeMatrixPlatform + j], MaterialActions._correctMaterial))
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

    private IEnumerator CountdownToDestroyPlatforms()
    {
        yield return new WaitForSeconds(_timeBeforeDestroy);
        DestroyPlatformsByMaterial();
        PaintPlatforms();
    }

}
