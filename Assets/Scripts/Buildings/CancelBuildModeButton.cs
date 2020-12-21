using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CancelBuildModeButton : MonoBehaviour,IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.MyInstance.GameStateEnum == gameState.buildMode)
        {
            BuildMode.MyInstance.UnassignBuildMode();
        }
        else if(GameManager.MyInstance.GameStateEnum == gameState.editMode)
        {

        }
    }
}
