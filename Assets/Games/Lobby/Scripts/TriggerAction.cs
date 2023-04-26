using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerAction : MonoBehaviour
{
    private string nameGame;

    public string NameGame { get => nameGame; set => nameGame = value; }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name.Equals("PlayerArmature"))
        {
            SceneManager.LoadScene(NameGame);
        }
    }
}
