using Mirror;
using Mirror.Discovery;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindHost : MonoBehaviour
{
    /*[SerializeField] private NetworkDiscovery networkDiscovery;
    private bool serverFound;
    private float timeElapsed = 0f;
    [SerializeField] private float timeout = 10f;


    void Start()
    {
        //networkDiscovery = gameObject.GetComponent<NetworkDiscovery>();
        //UnityEditor.Events.UnityEventTools.AddPersistentListener(networkDiscovery.OnServerFound, ServerIsFounded);
        //UnityEditor.Undo.RecordObjects(new UnityEngine.Object[] { this, networkDiscovery }, "Set NetworkDiscovery");

        networkDiscovery.OnServerFound.AddListener(ServerIsFounded);
        Debug.Log(networkDiscovery);
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
        NetworkManager.singleton.StartClient(ipAddress.uri);
    }

    void Update()
    {
        if (!serverFound)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed >= timeout)
            {
                Debug.Log("No servers found.");
                networkDiscovery.StopDiscovery();
                NetworkManager.singleton.StartHost();
                networkDiscovery.AdvertiseServer();
                enabled = false;
            }
        }
    }*/
}
