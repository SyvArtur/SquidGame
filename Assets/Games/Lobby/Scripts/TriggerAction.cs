using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerAction : NetworkBehaviour
{
    private string nameGame;

    public string NameGame { get => nameGame; set => nameGame = value; }

    
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player") && collider.gameObject.GetComponent<NetworkIdentity>().Equals(MyNetworkManager.clientObjects[0].GetComponent<NetworkIdentity>()))
        {
            ChangeScene();
/*            if (!NetworkServer.active)
            {
                Debug.Log("Сервер не активный");
                NetworkManager.singleton.StartServer();
            }*/
            //SceneManager.LoadScene(NameGame);

            //Networksc.SwitchScene.sc(other.GetComponent<NetworkIdentity>().connectionToClient, newSceneName);
        }

        /*        if (collider.gameObject.name.Equals("PlayerArmature"))
                {
                    SceneManager.LoadScene(NameGame);
                }*/
    }
/*    private IEnumerator LoadSceneAfterAllPlayersReadyCoroutine(string sceneName)
    {
        // Wait until all clients are ready
*//*        while (NetworkServer.connections.Count(conn => conn.Value.isReady) < NetworkServer.connections.Count)
        {
            yield return null;
        }*//*

       
    }*/

    void ChangeScene()
    {
        MyNetworkManager.singleton.ServerChangeScene(NameGame);
        //StartCoroutine(LoadSceneAfterAllPlayersReadyCoroutine(NameGame));
    }
/*    public override void OnStartServer()
    {
        // вызываем метод, который создаст все нужные объекты при старте сервера
        SpawnPlayers();
    }

    private void SpawnPlayers()
    {
        // находим все зарегистрированные в NetworkManager префабы, которые можно создавать на сервере
        List<GameObject> spawnablePrefabs = NetworkManager.singleton.spawnPrefabs;

        // проходим по списку префабов и создаем объекты для каждого игрока, который подключился к серверу
        foreach (GameObject prefab in spawnablePrefabs)
        {
            if (prefab.GetComponent<PlayerNickname>() != null)
            {
                foreach (NetworkConnection conn in NetworkServer.connections.Values)
                {
                    GameObject player = Instantiate(prefab);
                    NetworkServer.Spawn(player, conn);
                }
            }
        }
    }*/
}
