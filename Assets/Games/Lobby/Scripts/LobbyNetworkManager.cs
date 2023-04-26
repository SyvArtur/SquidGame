using Mirror;
using Mirror.Discovery;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

class LobbyNetworkManager : NetworkManager
{
    private NetworkDiscovery networkDiscovery;
    private bool serverFound;
    private float timeElapsed = 0f;
    public float timeout = 4f;


    new void Start()
    {
        base.Start();
        Debug.Log(Environment.MachineName);

        networkDiscovery = gameObject.GetComponent<NetworkDiscovery>();
        //UnityEditor.Events.UnityEventTools.AddPersistentListener(networkDiscovery.OnServerFound, ServerIsFounded);
        //UnityEditor.Undo.RecordObjects(new UnityEngine.Object[] { this, networkDiscovery }, "Set NetworkDiscovery");

        networkDiscovery.OnServerFound.AddListener(ServerIsFounded);
        serverFound = false;

        networkDiscovery.StartDiscovery();
        //ConnectToServer("192.168.1.17");
    }

    public void ServerIsFounded(ServerResponse serverResponse)
    {
        serverFound = true;
        networkDiscovery.StopDiscovery();
        Debug.Log(serverResponse + "HALLO");
        ConnectToServer(serverResponse);
    }

    public void ConnectToServer(ServerResponse ipAddress)
    {
        //networkAddress = ipAddress.ToString();

        StartClient(ipAddress.uri);
    }

    new void Update()
    {
        base.Update();
        if (!serverFound)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed >= timeout)
            {
                Debug.Log("No servers found.");
                networkDiscovery.StopDiscovery();
                StartHost();
                networkDiscovery.AdvertiseServer();
                enabled = false;
            }
        }
    }
}