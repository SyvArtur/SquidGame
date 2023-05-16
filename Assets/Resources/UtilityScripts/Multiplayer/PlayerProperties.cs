using Cinemachine;
using Mirror;
using StarterAssets;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class PlayerProperties : NetworkBehaviour
{
    [SerializeField] private GameObject _nickname;
    private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private GameObject cinemachineCameraTarget;
    [SerializeField] private SkinnedMeshRenderer meshRenderer;

    [SyncVar]
    public int countOfWins;
    
    /*    public override void OnStartClient()
        {
            base.OnStartClient();


        }

        public override void OnStartLocalPlayer()
        {
            base.OnStartLocalPlayer();
            *//*_nickname.GetComponent<TextMeshPro>().text = NetworkClient.connection.identity.connectionToServer.connectionId.ToString();*//*
            // ”станавливаем никнейм дл€ локального игрока
            *//*        _nickname.GetComponent<TextMeshProUGUI>().text = Environment.MachineName;
                    _nickname.SetActive(false);*//*
            //_nickname.GetComponent<TextMeshPro>().text = Environment.MachineName;
            //Debug.Log(_nickname.GetComponent<TextMeshProUGUI>().text);
            if (isOwned)
            {

                Debug.Log("OKJA");
                SetNickname();
                //_nickname.GetComponent<TextMeshPro>().text = Environment.MachineName;
                _nickname.SetActive(true);
            }
        }

        public override void OnStartServer()
        {
            //TextMesh
            base.OnStartServer();
            _nickname.GetComponent<NetworkIdentity>().only();
            //_nickname.GetComponent<TextMeshPro>().text = NetworkClient.connection.identity.connectionToServer.connectionId.ToString();
            // ”станавливаем никнейм дл€ всех игроков, кроме локального
            *//*        if (!isOwned)
                    {
                        //_nickname.GetComponent<TextMeshPro>().text = Environment.MachineName;

                    }*//*
        }

        [Command]
        private void SetNickname()
        {
            //if (!isLocalPlayer) { return; }
            _nickname.GetComponent<TextMeshPro>().text = Random.Range(0, 1000).ToString();
        }*/

    [SyncVar(hook = nameof(OnNameChanged))]
    public string playerName;

    [SyncVar]
    private string computerName;

    [SyncVar(hook = nameof(OnActiveChanged))]
    public bool playerActive;


    void OnNameChanged(string oldName, string newName)
    {
        _nickname.GetComponent<TextMeshPro>().text = newName;
        if (isLocalPlayer)
        {
            _nickname.SetActive(false);
        }
    }

    void OnActiveChanged(bool oldActive, bool newActive)
    {
        _nickname.GetComponent<TextMeshPro>().enabled = newActive;
        gameObject.GetComponent<ThirdPersonController>().enabled = newActive;
        meshRenderer.enabled = newActive;
    }

    private IEnumerator WaitAllClient(Scene scene)
    {
        while (!MyNetworkManager.allClientsReady)
        {
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(0.5f);

        if (!playerActive)
        {
            playerActive = true;
        }

        if (scene.name.Equals("Lobby"))
        {
            //Debug.Log(UnityEngine.Random.Range(12, 16) + "  " + gameObject.transform.position);
            Vector3 myPosition = new Vector3(UnityEngine.Random.Range(-8, 17), 2, UnityEngine.Random.Range(-10, 7));

            TargetSetPoisionForLobby(connectionToClient, scene, myPosition);
            //Debug.Log(gameObject.transform.position);
        }
    }

    [TargetRpc]
    private void TargetSetPoisionForLobby(NetworkConnectionToClient conn, Scene scene, Vector3 myPosition)
    {
        StartCoroutine(SetPositionForLobbyScene(scene, myPosition));
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        /*        if (scene.name.Equals("BlockParty"))
                {
                    //Debug.Log(UnityEngine.Random.Range(12, 16) + "  " + gameObject.transform.position);
                    gameObject.transform.position = new Vector3 (UnityEngine.Random.Range(10, 18), UnityEngine.Random.Range(6, 20), UnityEngine.Random.Range(10, 18));
                    //Debug.Log(gameObject.transform.position);
                }*/

        StartCoroutine(WaitAllClient(scene));
        TargetBoundCameraToPlayer(gameObject.GetComponent<NetworkIdentity>().connectionToClient);
/*        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        virtualCamera.Follow = cinemachineCameraTarget.transform;*/

        //NetworkServer.SpawnObjects();
    }

    private IEnumerator SetPositionForLobbyScene(Scene scene, Vector3 myPosition)
    {   
        //Debug.Log(UnityEngine.Random.Range(12, 16) + "  " + gameObject.transform.position);
        gameObject.transform.position = myPosition;
        yield return new WaitForSeconds(0.5f);
        //Debug.Log(gameObject.transform.position);
        if (Vector3.Distance(gameObject.transform.position, myPosition) > 5) 
        {
            SetPositionForLobbyScene(scene, myPosition);
        } else
        {
            yield break;
        }
        
    }


    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        countOfWins = 0;
        playerActive = true;
        /*        if (isServer)
                {
                    countOfWins = countOfWins + Random.Range(1, 10);
                    //playerName = computerName + " (" + countOfWins + ")";
                }*/

        if (isLocalPlayer)
        {
            CmdSetComputerNameForLocalPlayer(Environment.MachineName);
            CmdUpdatePlayerName();
            virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
            virtualCamera.Follow = cinemachineCameraTarget.transform;
            //CmdChangePlayerActive(false);
        }
        if (isServer)
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            //UpdatePlayerNameFromServer();
        }
        //OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }
    void OnClientReady(NetworkConnection conn, ReadyMessage msg)
    {
        //  од, который нужно выполнить при готовности клиента
    }

    [Command]
    public void CmdUpdatePlayerName()
    {
        playerName = computerName + " (" + countOfWins + ")";
    }

    public void UpdatePlayerNameFromServer()
    {
        playerName = computerName + " (" + countOfWins + ")";
    }

    [Command]
    public void CmdSetComputerNameForLocalPlayer(string compName)
    {
        computerName = compName;
    }

    [Command]
    public void CmdChangePlayerActive(bool active)
    {
        Debug.Log("And it");
        playerActive = active;
    }

    [TargetRpc]
    public void TargetBoundCameraToPlayer(NetworkConnectionToClient conn)
    {
        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        virtualCamera.Follow = cinemachineCameraTarget.transform;
    }

    public override void OnStopClient()
    {
        base.OnStopClient();

        if (isServer)
        {
            // Ётот код будет выполнен только на локальном клиенте (игроке)
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    void Update()
    {
        if (isOwned /*|| isLocalPlayer*/) { return; }
        _nickname.transform.LookAt(2* _nickname.transform.position - new Vector3(Camera.main.transform.position.x, _nickname.transform.position.y, Camera.main.transform.position.z));
        //transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y - 180, transform.rotation.z);
    }
}
