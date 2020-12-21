using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingListing : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    public BuildingSO recipe;

    public void InitRecipe(BuildingSO initiatedRecipe)
    {
        recipe = initiatedRecipe;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        BuildingUIController.MyInstance.UpdateUIElementsOnClick(recipe);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}