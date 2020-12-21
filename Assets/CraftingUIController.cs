using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CraftingUIController : MonoBehaviour
{
    public static CraftingUIController myInstance;
    public static CraftingUIController MyInstance
    {
        get
        {
            if (myInstance == null)
            {
                myInstance = FindObjectOfType<CraftingUIController>();
            }
            return myInstance;
        }
    }

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
    private Transform outputItemPanel;
    [SerializeField]
    private Transform toolItemPanel;


    [SerializeField]
    private TextMeshProUGUI craftButton;
    private Button cB;
    List<Item> localOutputItems = new List<Item>();

    private void Start()
    {
        cB = craftButton.GetComponent<Button>();
        cB.onClick.AddListener(() => CraftItems());

    }

    //TODO:: Check to make sure you have the correct items in the inventory before crafting the item.
    //TODO:: Implement multiples of input items and multiples of output items
    //TODO:: Add scripts to the item slots with hovers on so you can see things related to that item
    public void UpdateUIElementsOnClick(CraftingRecipe clickedRecipe)
    {
        itemName.text = clickedRecipe.recipeName;
        //itemImage.enabled = true;
        //itemImage.sprite = clickedRecipe.re;

        foreach (Item item in clickedRecipe.inputItems)
        {//Create Input Items Interface.

            GameObject inputItemImages = Instantiate(itemSlot, inputItemPanel);
            inputItemImages.GetComponent<Image>().sprite = item.ItemImage;
        }
        foreach (Item item in clickedRecipe.outputItems)
        {//Create Output Items Interface.

            GameObject outputItemsImages = Instantiate(itemSlot, outputItemPanel);
            outputItemsImages.GetComponent<Image>().sprite = item.ItemImage;

            localOutputItems.Add(item);
            Debug.Log(localOutputItems);

        }
        foreach (Item item in clickedRecipe.toolsRequired)
        {//Create Tools Items Interface.

        }


    }
    public void CraftItems()
    {
        foreach(Item item in localOutputItems)
        {
            InventoryController.MyInstance.AddItem(item);
        }
        
    }

}
