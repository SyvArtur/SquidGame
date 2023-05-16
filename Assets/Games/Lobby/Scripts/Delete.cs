using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete : NetworkBehaviour
{
    public GameObject prefab;
    private float x = 0;
    public override void OnStartServer()
    {
        NetworkServer.SpawnObjects();
        StartCoroutine(WaitSecondsAndRun());
    }


    private IEnumerator WaitSecondsAndRun()
    {
        yield return new WaitForSeconds(2);
       
        x = x + 0.2f;
        SpawnTestObjects();
        StartCoroutine(WaitSecondsAndRun());
    }

    private void SpawnTestObjects()
    {
        GameObject platforms = Instantiate(prefab, new Vector3(x, 1, x), Quaternion.Euler(0, 0, 0));
        NetworkServer.Spawn(platforms);
        //platforms.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
    }

}
