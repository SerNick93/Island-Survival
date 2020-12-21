using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHandler : MonoBehaviour
{
    [SerializeField]
    private float horizontalSpeed = 1f;
    [SerializeField]
    private float verticalSpeed = 1f;
    private float xRotation = 0f;
    private float yRotation = 0f;
    private Camera cam;
    [SerializeField]
    PlayerController player;
    [SerializeField]
    private float interactionDistance;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!UIBaseManager.MyInstance.UiOn)
        {
            float mouseX = Input.GetAxis("Mouse X") * horizontalSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * verticalSpeed;

            yRotation += mouseX;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90, 90);

            cam.transform.eulerAngles = new Vector3(xRotation, yRotation, 0f);
            player.transform.eulerAngles = new Vector3(0f, yRotation, 0f);

            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            Debug.DrawRay(ray.origin, cam.transform.forward * interactionDistance, Color.red);
            if (Physics.Raycast(ray, out hit, interactionDistance))
            {

                if (Input.GetMouseButtonDown(0) && GameManager.MyInstance.GameStateEnum == gameState.gameMode)
                {
                    if (hit.transform.GetComponent<ResourceNodeInteraction>() != null)
                    {
                        hit.transform.GetComponent<ResourceNodeInteraction>().HarvestThisNode();
                    }
                    if (hit.transform.CompareTag("BuildingBlueprint") && hit.transform.GetComponent<BuildingScript>() != null)
                    {
                        //Debug.Log(hit.transform.GetComponent<BuildingScript>().thisBuilding.buildingName);
                        hit.transform.GetComponent<BuildingScript>().BuildObject();
                            
                    }
                }
            }

        }

    }
}
