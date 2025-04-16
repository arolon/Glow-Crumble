using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance;

    public GameObject inventorySlotPrefab;
    public Transform inventoryGrid;

    // Dictionary to hold items and their quantities
    public Dictionary<string, int> inventoryItems = new Dictionary<string, int>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddItem(string rawItemName)
    {
        string normalizedItemName = NormalizeItemName(rawItemName);

        if (inventoryItems.ContainsKey(normalizedItemName))
            inventoryItems[normalizedItemName]++;
        else
            inventoryItems[normalizedItemName] = 1;

        //UpdateInventoryUI();
    }

    string NormalizeItemName(string itemName)
    {
        int parenIndex = itemName.IndexOf(" (");
        return parenIndex > -1 ? itemName.Substring(0, parenIndex) : itemName;
    }

    // Check if the inventory has at least one of the item
    public bool HasItem(string rawItemName)
    {
        string normalized = NormalizeItemName(rawItemName);
        return inventoryItems.ContainsKey(normalized) && inventoryItems[normalized] > 0;
    }

    // Check if the inventory has a specific quantity of the item
    public bool HasItem(string rawItemName, int requiredAmount)
    {
        string normalized = NormalizeItemName(rawItemName);
        return inventoryItems.ContainsKey(normalized) && inventoryItems[normalized] >= requiredAmount;
    }

    // Optional: Remove a specific quantity of an item
    public bool RemoveItem(string rawItemName, int amount)
    {
        string normalized = NormalizeItemName(rawItemName);

        if (!inventoryItems.ContainsKey(normalized) || inventoryItems[normalized] < amount)
            return false;

        inventoryItems[normalized] -= amount;

        if (inventoryItems[normalized] <= 0)
            inventoryItems.Remove(normalized);

        //UpdateInventoryUI();
        return true;
    }

    // Optional: Get current count of an item
    public int GetItemCount(string rawItemName)
    {
        string normalized = NormalizeItemName(rawItemName);
        return inventoryItems.ContainsKey(normalized) ? inventoryItems[normalized] : 0;
    }

}
