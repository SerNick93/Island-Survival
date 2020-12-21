using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

//TODO:: Set limits on how far in the x and z axis an instanced structure can go
public class BuildMode : MonoBehaviour
{
    public static BuildMode myInstance;
    public static BuildMode MyInstance
    {
        get
        {
            if (myInstance == null)
            {
                myInstance = FindObjectOfType<BuildMode>();
            }
            return myInstance;
        }
    }

    public BuildingSO activeStructureOuline;
    public GameObject EditableStructure;
    [SerializeField]
    Transform tempStructureParent, worldParent;
    Vector3 liveMousePosition;
    GameObject instancedStructureOutline;


    public void FixedUpdate()
    {
        if (instancedStructureOutline != null)
        {
            liveMousePosition = GameManager.MyInstance.MousePosition;
            liveMousePosition.y = (instancedStructureOutline.transform.position.y);
            instancedStructureOutline.transform.position = liveMousePosition;
        }
 

    }
    public void LateUpdate()
    {
        if (instancedStructureOutline != null && Input.GetMouseButtonDown(0))
        {
            GameObject worldStructure = Instantiate(instancedStructureOutline, liveMousePosition, Quaternion.identity, worldParent);
            //Open a small interface here which will allow the player to edit the placement and rotation of the object.
            worldStructure.GetComponent<BuildingScript>().enabled = true;
            Destroy(instancedStructureOutline);
            instancedStructureOutline = null;
            Debug.Log(worldStructure);
            EditableStructure = worldStructure;
            EditMode.MyInstance.EnableEditMode(EditableStructure);

            //DelayOpeningInterface();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnassignBuildMode();
        }
    }
    public async void DelayOpeningInterface()
    {
        await Task.Delay(500);
        GameManager.MyInstance.GameStateEnum = gameState.gameMode;

        //Instantiate(XTransformGizmo, )
        //TODO::Change the mode to the edit mode when the functionallity is added in to transform a placed object.
        UIBaseManager.MyInstance.TurnOnMainInterface();

    }
    public void SetBuildModeActive()
    {
        GameManager.MyInstance.GameStateEnum = gameState.buildMode;
        GameManager.MyInstance.CancelModesButton.SetActive(true);
        UIController.MyInstance.TurnCursorOn();
        //close the playermaininterface
        UIBaseManager.MyInstance.TurnOffMainInterface();
        //set the selected object as the activestructureoutline
        activeStructureOuline = BuildingUIController.MyInstance.ActiveStructueOutline;
        //add the selected object to the crosshair
        instancedStructureOutline = Instantiate(activeStructureOuline.structurePrefab, liveMousePosition, Quaternion.identity, tempStructureParent);

        //disable the ability to harvest nodes
    }
    public void UnassignBuildMode()
    {
        GameManager.MyInstance.GameStateEnum = gameState.gameMode;
        UIBaseManager.MyInstance.TurnOnMainInterface();
        Destroy(instancedStructureOutline);
        //Destroy(activeStructureOuline);
        activeStructureOuline = null;
        instancedStructureOutline = null;
    }
}
