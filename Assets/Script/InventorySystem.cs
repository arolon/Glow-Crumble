using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    public GameObject inventorySlotPrefab;
    public Transform inventoryGrid;
    public List<string> inventoryItems = new List<string>();
    
    public List<Sprite> itemSprites = new List<Sprite>();
    public List<string> itemNames = new List<string>();
    
    private Dictionary<string, Sprite> itemDictionary = new Dictionary<string, Sprite>();

    void Start()
    {
        for (int i = 0; i < Mathf.Min(itemNames.Count, itemSprites.Count); i++)
        {
            if (!string.IsNullOrEmpty(itemNames[i]) && itemSprites[i] != null)
            {
                itemDictionary[itemNames[i]] = itemSprites[i];
            }
        }

        UpdateInventoryUI();
    }

    public void AddItem(string itemName)
    {
        inventoryItems.Add(itemName);
        Debug.Log("Added " + itemName + " to inventory");
        UpdateInventoryUI();
    }

    public void RemoveItem(string itemName)
    {
        if (inventoryItems.Contains(itemName))
        {
            inventoryItems.Remove(itemName);
            UpdateInventoryUI();
        }
    }

    void UpdateInventoryUI()
    {

        foreach (Transform child in inventoryGrid)
        {
            Destroy(child.gameObject);
        }


        foreach (string item in inventoryItems)
        {
            GameObject slot = Instantiate(inventorySlotPrefab, inventoryGrid);
            
  
            Image itemImage = slot.GetComponentInChildren<Image>();
            if (itemImage != null)
            {
                itemImage.sprite = GetItemSprite(item);
            }
            

            Text itemText = slot.GetComponentInChildren<Text>();
            if (itemText != null)
            {
                itemText.text = item;
            }
            
            Button button = slot.GetComponent<Button>();
            if (button != null)
            {
                string itemName = item; 
                button.onClick.AddListener(() => UseItem(itemName));
            }
        }
    }

    Sprite GetItemSprite(string itemName)
    {
        if (itemDictionary.ContainsKey(itemName))
        {
            return itemDictionary[itemName];
        }
        
        Sprite sprite = Resources.Load<Sprite>("Sprites/" + itemName);
        
        
        if (sprite == null)
        {
            sprite = Resources.Load<Sprite>("Sprites/DefaultItem");
            Debug.LogWarning("Could not find sprite for " + itemName);
        }
        
        return sprite;
    }

    void UseItem(string itemName)
    {
        Debug.Log("Using item: " + itemName);
        
        CookingStation cookingStation = FindObjectOfType<CookingStation>();
        if (cookingStation != null)
        {
            cookingStation.AddIngredient(itemName);
        }
    }
    
    public bool HasAnyItems()
    {
        return inventoryItems.Count > 0;
    }
    
    public bool HasItem(string itemName)
    {
        return inventoryItems.Contains(itemName);
    }
}