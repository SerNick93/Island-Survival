using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftingListing : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    public CraftingRecipe recipe;
    public void InitRecipe(CraftingRecipe initiatedRecipe)
    {
        recipe = initiatedRecipe;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        CraftingUIController.MyInstance.UpdateUIElementsOnClick(recipe);
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
