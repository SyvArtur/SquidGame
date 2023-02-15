using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MaterialActions : MonoBehaviour
{
    [SerializeField] private Material[] _pictures;
    [SerializeField] public static float _timeBeforeChooseRightMaterial = 10f;
    [SerializeField] private GameObject[] _screens;

    [HideInInspector] public static Material[] _materials;
    [HideInInspector] public static Material _correctMaterial;


    void Awake()
    {
        SetPropertyMaterials();

        KeyEvents_UltimateKnockout.Instance.SubscribeToChoosePlatformForPlayer(() => {
            ChooseRightMaterial();
        });


        KeyEvents_UltimateKnockout.Instance.SubscribeToRevising(() =>
        {
            Shuffle(_materials);
            ResetScreens();
        });


    }


    void Start()
    {
        
    }

    private void Shuffle<T>(T[] arr)
    {
        for (int i = arr.Length - 1; i >= 1; i--)
        {
            int j = Random.Range(0, i);

            T tmp = arr[j];
            arr[j] = arr[i];
            arr[i] = tmp;
        }
    }

    private Material[] SetPropertyMaterials()
    {
        _materials = new Material[Platforms._sizeMatrixPlatform * Platforms._sizeMatrixPlatform];

        for (int i = 0; i < Platforms._sizeMatrixPlatform * Platforms._sizeMatrixPlatform; i++)
        {
            _materials[i] = _pictures[i % _pictures.Length];
        }
        return _materials;
    }



    private void ResetScreens()
    {
        for (int i = 0; i < _screens.Length; i++)
        {
            _screens[i].GetComponent<MeshRenderer>().material = null;
            _screens[i].GetComponent<MeshRenderer>().material.color = Color.HSVToRGB(221f, 221f, 221f);
        }
    }

    private void ChooseRightMaterial()
    {
        float tillingX = 1.6f;
        _correctMaterial = _pictures[Random.Range(0, _pictures.Length)];
        for (int i = 0; i < _screens.Length; i++)
        {
            _screens[i].GetComponent<MeshRenderer>().material = new Material(_correctMaterial);
            _screens[i].GetComponent<MeshRenderer>().material.mainTextureScale = new Vector2(Mathf.Max(transform.localScale.x, transform.localScale.z) * tillingX, transform.localScale.y * 1);
            _screens[i].GetComponent<MeshRenderer>().material.SetTextureOffset("_MainTex", new Vector2((1 - tillingX) / 2, 0));
        }
    }

}
