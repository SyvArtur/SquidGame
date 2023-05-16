using Cinemachine;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class AllServerLogic_BlockParty : NetworkBehaviour
{
    /*private readonly SyncList<Color> _syncListColors = new SyncList<Color>();
    private readonly SyncList<GameObject> _platforms = new SyncList<GameObject>();

    [ClientRpc]
    private partial void PaintCorrectColorOnCanvas()
    {
        _imageUI.GetComponent<Image>().color = _rightColor;
    }

    [ClientRpc]
    private partial void RpcUpdateObjectMaterial(GameObject obj, Color color)
    {
        obj.GetComponent<MeshRenderer>().material.color = color;
    }

    [ClientRpc]
    private partial void WriteTimerTextOnCanvas(float seconds)
    {
        _timerText.GetComponent<Text>().text = string.Format("{0:0}", seconds);
    }

    [TargetRpc]
    private partial void TargetCameraObservation(NetworkConnection conn)
    {
        CinemachineVirtualCamera virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        virtualCamera.Follow = _observationPlace.transform;
    }

    [TargetRpc]
    private partial void TargetSetPosition(NetworkConnection conn, Vector3 newPosition)
    {
        conn.identity.gameObject.transform.position = newPosition;
    }

    [TargetRpc]
    private partial void TargetPlayerWin(NetworkConnection conn)
    {
        Debug.Log("Hallo");
        conn.identity.gameObject.GetComponent<PlayerProperties>().countOfWins++;
        conn.identity.gameObject.GetComponent<PlayerProperties>().CmdUpdatePlayerName();
    }*/
}
