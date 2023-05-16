using Cinemachine;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : NetworkBehaviour
{
    [SerializeField] private GameObject Player;
    void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        if (other.gameObject == Player)
        {
            ClientLogic.TargetCameraObservation(MyNetworkManager.clientObjects[i].GetComponent<NetworkIdentity>().connectionToClient);
            MyNetworkManager.clientObjects[i].GetComponent<PlayerProperties>().playerActive = false;
            /*            Camera.main.GetComponent<CinemachineBrain>().enabled = false;
                        Camera.main.transform.position = new Vector3(15, 30, 6);
                        Camera.main.transform.rotation = Quaternion.Euler(68f, 270f, 0);*/
        }
    }

}
