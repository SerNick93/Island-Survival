using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBaseManager : MonoBehaviour
{

    public static UIBaseManager myInstance;
    public static UIBaseManager MyInstance
    {
        get
        {
            if (myInstance == null)
            {
                myInstance = FindObjectOfType<UIBaseManager>();
            }
            return myInstance;
        }
    }

    public CanvasGroup AssignedCanvasBase { get => assignedCanvasBase; set => assignedCanvasBase = value; }
    public bool UiOn { get => uiOn; set => uiOn = value; }

    [SerializeField]
    CanvasGroup assignedCanvasBase, mainPlayerInterface;
    bool uiOn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!UiOn && !assignedCanvasBase && GameManager.MyInstance.GameStateEnum != gameState.buildMode)
            {
                TurnOnMainInterface();
            }

            else if (UiOn)
            {
                TurnOffMainInterface();
            }
        }
        if (Input.GetKeyDown(KeyCode.P))
        {

        }
    }

    public void TurnOnMainInterface()
    {
        assignedCanvasBase = mainPlayerInterface;
        assignedCanvasBase.alpha = 1;
        assignedCanvasBase.blocksRaycasts = true;
        UiOn = true;
        UIController.MyInstance.TurnCursorOn();

    }
    public void TurnOffMainInterface()
    {
        assignedCanvasBase.alpha = 0;
        assignedCanvasBase.blocksRaycasts = false;
        UiOn = false;
        assignedCanvasBase = null;
        UIController.MyInstance.TurnCursorOff();

    }

}
