using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingUIController : MonoBehaviour
{
    public static BuildingUIController myInstance;
    public static BuildingUIController MyInstance
    {
        get
        {
            if (myInstance == null)
            {
                myInstance = FindObjectOfType<BuildingUIController>();
            }
            return myInstance;
        }
    }

    public BuildingSO ActiveStructueOutline { get => activeStructueOutline; set => activeStructueOutline = value; }

    [Header("Item Details")]
    [SerializeField]
    private TextMeshProUGUI itemName;
    [SerializeField]
    private TextMeshProUGUI itemDescription;
    [SerializeField]
    private Image itemImage;

    [Header("Item Crafting Details")]
    [SerializeField]
    private GameObject itemSlot;
    [SerializeField]
    private Transform inputItemPanel;

    [SerializeField]
    private TextMeshProUGUI craftButton;
    private Button cB;
    BuildingSO activeStructueOutline;

    // Start is called before the first frame update
    public void UpdateUIElementsOnClick(BuildingSO clickedRecipe)
    {
        ActiveStructueOutline = clickedRecipe;
        itemName.text = clickedRecipe.buildingName;
        //itemImage.enabled = true;
        //itemImage.sprite = clickedRecipe.re;

        foreach (Item item in clickedRecipe.inputItems)
        {//Create Input Items Interface.

            GameObject inputItemImages = Instantiate(itemSlot, inputItemPanel);
            inputItemImages.GetComponent<Image>().sprite = item.ItemImage;
        }

        //foreach (Item item in clickedRecipe.toolsRequired)
        //{//Create Tools Items Interface.

        //}


    }

}
