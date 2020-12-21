using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditMode : MonoBehaviour
{
    public static EditMode myInstance;
    public static EditMode MyInstance
    {
        get
        {
            if (myInstance == null)
            {
                myInstance = FindObjectOfType<EditMode>();
            }
            return myInstance;
        }
    }
    GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableEditMode(GameObject objectToEdit)
    {
        //Edit mode will need a separate camers which can be controlled indepnedently of the player's camera.
        //the game will be in a paused state while you are in this mode to prevent an attack from a wild animal.
        GameManager.MyInstance.GameStateEnum = gameState.editMode;
        GameManager.MyInstance.CancelModesButton.SetActive(true);
        Instantiate(GameManager.MyInstance.TransformGizmos, objectToEdit.transform);
        UIController.MyInstance.TurnCursorOn();

    }
}
