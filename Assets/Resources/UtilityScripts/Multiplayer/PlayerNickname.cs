using Mirror;
using TMPro;
using UnityEngine;

public class PlayerNickname : NetworkBehaviour
{
    [SerializeField] private GameObject _nickname;

    /*    public override void OnStartClient()
        {
            base.OnStartClient();


        }

        public override void OnStartLocalPlayer()
        {
            base.OnStartLocalPlayer();
            *//*_nickname.GetComponent<TextMeshPro>().text = NetworkClient.connection.identity.connectionToServer.connectionId.ToString();*//*
            // Устанавливаем никнейм для локального игрока
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
            // Устанавливаем никнейм для всех игроков, кроме локального
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

    void OnNameChanged(string oldName, string newName)
    {
        _nickname.GetComponent<TextMeshPro>().text = newName;
    }

    private void Start()
    {
        playerName = Random.Range(0, 1000).ToString();

        //_nickname.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
    }

    void Update()
    {
        if (isOwned /*|| isLocalPlayer*/) { return; }
        _nickname.transform.LookAt(2* _nickname.transform.position - new Vector3(Camera.main.transform.position.x, _nickname.transform.position.y, Camera.main.transform.position.z));
        //transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y - 180, transform.rotation.z);
    }
}
