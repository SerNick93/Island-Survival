using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using custom.controllers;
using UnityEngine.UI;

public enum gameState { gameMode, editMode, buildMode}

public class GameManager : MonoBehaviour
{
    public static GameManager myInstance;
    public static GameManager MyInstance
    {
        get
        {
            if (myInstance == null)
            {
                myInstance = FindObjectOfType<GameManager>();
            }
            return myInstance;
        }
    }

    public Grid Grid { get => grid; set => grid = value; }
    public Vector3 MousePosition { get => mousePosition; set => mousePosition = value; }
    public gameState GameStateEnum { get => gameStateEnum; set => gameStateEnum = value; }
    public GameObject TransformGizmos { get => transformGizmos; set => transformGizmos = value; }
    public GameObject CancelModesButton { get => cancelModesButton; set => cancelModesButton = value; }

    private gameState gameStateEnum;

    Grid grid;
    Vector3 mousePosition;
    [SerializeField]
    GameObject transformGizmos;
    [SerializeField]
    GameObject cancelModesButton;
    // Start is called before the first frame update
    void Start()
    {
        GameStateEnum = gameState.gameMode;
        CancelModesButton.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MousePosition = CustomController.GetWorldPositionOnPlane(Input.mousePosition, 0f);
    }
    public void ChangeGameMode()
    {
        if (GameStateEnum != gameState.buildMode)
        {
            GameStateEnum = gameState.buildMode;

            //Add a grid to the map to make it easier to place objects on the ground

        }
        
    }


}
