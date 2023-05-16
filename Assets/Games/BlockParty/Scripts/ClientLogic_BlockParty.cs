using Cinemachine;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ClientLogic_BlockParty : NetworkBehaviour
{
    [SerializeField] private GameObject _timerText;
    [SerializeField] private GameObject _imageUI;
    [SerializeField] private GameObject _observationPlace;

    [HideInInspector] public readonly SyncList<Color> _syncListColors = new SyncList<Color>();
    [HideInInspector] public readonly SyncList<GameObject> _platforms = new SyncList<GameObject>();
    [HideInInspector] public readonly SyncList<Vector3> _playerStartedPositins = new SyncList<Vector3>();

    [SyncVar(hook = nameof(RightColorChanged))]
    [HideInInspector]
    public Color _rightColor;

    [SyncVar(hook = nameof(TextTimerChanged))]
    [HideInInspector]
    public string textTimer;



    private void Awake()
    {
        _platforms.Callback += OnObjectsListUpdated;
        _playerStartedPositins.Callback += OnPlayerStartedPositinsChanged;
/*        if (isServer)
        {
            foreach (var player in MyNetworkManager.clientObjects)
            {
                TargetPlayerWin(player.GetComponent<NetworkIdentity>().connectionToClient);
            }
        }*/
    }

    private void OnPlayerStartedPositinsChanged(SyncList<Vector3>.Operation op, int index, Vector3 oldPosition, Vector3 newPosition)
    {
        if (op == SyncList<Vector3>.Operation.OP_ADD)
        {
            MyNetworkManager.clientObjects[MyNetworkManager.clientObjects.Count-1].transform.position = newPosition;
        }
    }

    public void SetPlayersPositions(int _sizeMatrixPlatform)
    {
        for (int i = 1; i < _sizeMatrixPlatform - 1; i++)
        {
            for (int j = 1; j < _sizeMatrixPlatform - 1; j++)
            {
                if ((i - 1) * (_sizeMatrixPlatform - 1) + j - 1 < MyNetworkManager.clientObjects.Count)
                {
                    //ClientLogic._playerStartedPositins.Add(new Vector3(i * 3, 8, j * 3));
                    //LobbyNetworkManager.clientObjects[(i - 1) * (_sizeMatrixPlatform - 1) + j - 1].transform.position = new Vector3(i * 3, 8, j * 3);
                    TargetSetPlayerPosition(MyNetworkManager.clientObjects[(i - 1) * (_sizeMatrixPlatform - 1) + j - 1].GetComponent<NetworkIdentity>().connectionToClient, new Vector3(i * 3, 8, j * 3));
                }
            }
        }
    }

    private void RightColorChanged(Color oldColor, Color newColor)
    {
        _imageUI.GetComponent<UnityEngine.UI.Image>().color = newColor;
    }
    private void TextTimerChanged(string oldText, string newText)
    {
        _timerText.GetComponent<Text>().text = newText;
    }


    public void OnObjectsListUpdated(SyncList<GameObject>.Operation op, int index, GameObject oldObject, GameObject newObject)
    {
        if (op == SyncList<GameObject>.Operation.OP_ADD)
        {
            //Debug.Log(newObject.transform.position + "  " + _syncListColors[_platforms.Count - 1]);
            //RpcUpdateObjectMaterial(newObject, _syncListColors[_platforms.Count - 1]);
            newObject.GetComponent<MeshRenderer>().material.color = _syncListColors[_platforms.Count - 1];
        }
        /*else if (op == SyncList<GameObject>.Operation.OP_CLEAR)
        {
            RpcClearObjectsList();
        }*/
    }

/*    [ClientRpc]
    public void RpcUpdateObjectMaterial(GameObject obj, Color color)
    {
        obj.GetComponent<MeshRenderer>().material.color = color;
    }*/


/*    [ClientRpc]
    public void RpcWriteTextOnSelectedElement(GameObject objectText, string text)
    {
        objectText.GetComponent<UnityEngine.UI.Text>().text = text;
    }*/

    [TargetRpc]
    public void TargetCameraObservation(NetworkConnectionToClient conn)
    {
        CinemachineVirtualCamera virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        virtualCamera.Follow = _observationPlace.transform;
    }

    [TargetRpc]
    public void TargetSetPlayerPosition(NetworkConnectionToClient conn, Vector3 newPosition)
    {
        /*conn.identity.gameObject.GetComponent<NetworkIdentity>().AssignClientAuthority(conn);
        CmdMoveObject(conn.identity.gameObject.GetComponent<NetworkTransform>(), newPosition);*/
        conn.identity.gameObject.transform.position = newPosition;
    }

/*    [Command]
    private void CmdMoveObject(NetworkTransform networkTransform, Vector3 newPosition)
    {
        networkTransform.transform.position = newPosition;
        networkTransform.SetSyncVarDirtyBit(1UL);
    }*/

/*    [TargetRpc]
    public void TargetPlayerWin(NetworkConnectionToClient conn)
    {
*//*        Debug.Log("Hallo");
        PlayerProperties playerProperties = conn.identity.gameObject.GetComponent<PlayerProperties>();
        
        int countOfWin = playerProperties.countOfWins + 1;
        playerProperties.CmdUpdatePlayerName();*//*
    }*/

/*    [ClientRpc]
    public void RPCPaintColorOnImage(GameObject objectImage, Color color)
    {
        objectImage.GetComponent<UnityEngine.UI.Image>().color = color;
    }*/


/*    [ClientRpc]
    public void RpcMakePlayerActive(GameObject player, bool active)
    {
        player.SetActive(active);
    }*/
}
