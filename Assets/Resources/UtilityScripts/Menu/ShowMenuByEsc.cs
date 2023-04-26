using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShowMenuByEsc : MonoBehaviour
{
    private Menu _menu;

    void Awake()
    {
        _menu = new Menu();
        _menu.CreateMenu();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_menu.MenuActive)
            {
                _menu.ShowMenu();
            }
            else
            _menu.HideMenu();
        }
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus && _menu.MenuActive)
        {
            Cursor.visible = true;
        }
    }
}
