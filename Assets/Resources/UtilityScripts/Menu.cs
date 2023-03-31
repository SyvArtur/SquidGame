using System;
using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Menu
{
    /*private static Menu instance;
    public static Menu Instance
    {
        get
        {

            if (instance == null)
                instance = FindObjectOfType(typeof(Menu)) as Menu;

            return instance;
        }
        private set
        {
            instance = value;

        }
    } */
    private GameObject _mainCanvas;


    public void CreateMenu()
    {
        // create game object and child object
        _mainCanvas = new GameObject();


        // give them names for fun
        _mainCanvas.name = "CanvasMenu";


        // add a canvas to the parent
        _mainCanvas.AddComponent<Canvas>();
        _mainCanvas.AddComponent<CanvasScaler>();
        _mainCanvas.AddComponent<GraphicRaycaster>();
        // add a recttransform to the child


        // make a reference to the parent canvas and use the ref to set its properties
        Canvas myCanvas = _mainCanvas.GetComponent<Canvas>();
        myCanvas.renderMode = RenderMode.ScreenSpaceOverlay;

        _mainCanvas.AddComponent<Image>();
        Image image = _mainCanvas.GetComponent<Image>();
        image.color = new Color32(180, 150, 100, 255);

        _mainCanvas.GetComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        HideMenu();

        CreateBtnRestart();
        CreateBtnBackToLobby();




        //RectTransform parentRectTransform = _mainCanvas.GetComponent<RectTransform>();



        /*        _btnRestart.AddComponent<Text>();
                // set text font type and material at runtime from font stored in Resources folder
                Text textComponent = _btnRestart.GetComponent<Text>();
                Material newMaterialRef = Resources.Load<Material>("3DTextCoolVetica");

                Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
                textComponent.font = ArialFont;
                textComponent.material = ArialFont.material;*/



        // set the font text
        //textComponent.text = "Ошибка соединения с базой";

    }
    private GameObject CreateDeffaultButton(string name, float positionX, float positionY, float sizeDeltaX, float sizeDeltaY,  Color32 color)
    {
        GameObject button = new GameObject();
        button.name = name;

        button.transform.parent = _mainCanvas.transform;
        button.AddComponent<RectTransform>();
        RectTransform childRectTransform = button.GetComponent<RectTransform>();
        
        childRectTransform.anchoredPosition3D = new Vector3(positionX, positionY, 0f);
        childRectTransform.sizeDelta = new Vector2(sizeDeltaX, sizeDeltaY);
        childRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        childRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        childRectTransform.pivot = new Vector2(0.5f, 0.5f);
        button.AddComponent<Image>();
        var image = button.GetComponent<Image>();
        image.color = color;
        button.AddComponent<Button>();
        return button;
    }

    private GameObject CreateDeffaultTextForButton(GameObject parrent, string selectText, int fontSize)
    {
        GameObject text = new GameObject();
        text.name = "text";

        text.transform.parent = parrent.transform;
        text.AddComponent<RectTransform>();
        RectTransform childRectTransform = text.GetComponent<RectTransform>();

        childRectTransform.anchoredPosition3D = new Vector3(0, 0, 0f);
        childRectTransform.sizeDelta = new Vector2(parrent.GetComponent<RectTransform>().sizeDelta.x, parrent.GetComponent<RectTransform>().sizeDelta.y);
        childRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        childRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        childRectTransform.pivot = new Vector2(0.5f, 0.5f);

        text.AddComponent<Text>();
        Text myText = text.GetComponent<Text>();
        myText.text = selectText;
        myText.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        myText.fontSize = fontSize;
        myText.alignment = TextAnchor.MiddleCenter;
        return text;
    }

    private void CreateBtnRestart()
    {
        GameObject btnRestart = CreateDeffaultButton("btnRestart", 0, 100, 700, 130, new Color32(255, 140, 40, 255));
        GameObject text = CreateDeffaultTextForButton(btnRestart, "Restart", 50);

        try
        {
            btnRestart.GetComponent<Image>().sprite = Resources.Load<Sprite>("BlueButton");
        }
        catch
        {
            Debug.Log("File BlueButton not found");
        }

        //Time.timeScale = 0f;
        btnRestart.GetComponent<Button>().onClick.AddListener(() => {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });

    }

    private void CreateBtnBackToLobby()
    {
        GameObject btnBackToLobby = CreateDeffaultButton("btnBackToLobby", 0, -100, 700, 130, new Color32(255, 140, 40, 255));
        GameObject text = CreateDeffaultTextForButton(btnBackToLobby, "Back to Lobby", 50);
        
        try
        {
            btnBackToLobby.GetComponent<Image>().sprite = Resources.Load<Sprite>("BlueButton");
        }
        catch
        {
            Debug.Log("File BlueButton not found");
        }

        btnBackToLobby.GetComponent<Button>().onClick.AddListener(() => {
            SceneManager.LoadScene("Lobby");
        });


    }

    public void ShowMenu()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        _mainCanvas.SetActive(true);
    }

    public void HideMenu()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _mainCanvas.SetActive(false);
    }
}

