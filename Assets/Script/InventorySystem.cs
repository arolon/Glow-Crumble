using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    public GameObject inventorySlotPrefab;
    public Transform inventoryGrid;
    public List<string> inventoryItems = new List<string>();

    public void AddItem(string itemName)
    {
        inventoryItems.Add(itemName);
        UpdateInventoryUI();
    }

    void UpdateInventoryUI()
    {
        foreach (Transform child in inventoryGrid)
            Destroy(child.gameObject);

        foreach (string item in inventoryItems)
        {
            GameObject slot = Instantiate(inventorySlotPrefab, inventoryGrid);
            Image itemImage = slot.GetComponentInChildren<Image>();
            // Set sprite dynamically based on item name or reference
            itemImage.sprite = GetItemSprite(item);
            slot.GetComponent<Button>().onClick.AddListener(() => DragItemToCookingArea(item));
        }
    }

    Sprite GetItemSprite(string itemName)
    {
        // Retrieve sprite based on item name (add sprites to Resources folder or an enum-based switch)
        // For example, return Resources.Load<Sprite>($"Sprites/{itemName}");
        return null;
    }

    void DragItemToCookingArea(string itemName)
    {
        FindObjectOfType<CookingStation>().AddIngredient(itemName);
    }
}
