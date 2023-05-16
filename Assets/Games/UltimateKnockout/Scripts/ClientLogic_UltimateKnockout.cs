using Cinemachine;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientLogic_UltimateKnockout : MonoBehaviour
{
    [SerializeField] private GameObject _observationPlace;

    [TargetRpc]
    public void TargetCameraObservation(NetworkConnectionToClient conn)
    {
        CinemachineVirtualCamera virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        virtualCamera.Follow = _observationPlace.transform;
    }


}

