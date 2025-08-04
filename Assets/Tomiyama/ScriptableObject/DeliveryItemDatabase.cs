using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DeliveryItemDatabase", menuName = "ScriptableObjects/DeliveryItemDatabase", order = 1)]
public class DeliveryItemDatabase : ScriptableObject
{
    [Serializable]
    public struct DeliveryItem
    {
        [SerializeField] private ItemType itemType;
        [SerializeField] private GameObject itemPrefab;
        public ItemType ItemType => itemType;

        public GameObject ItemPrefab => itemPrefab;
    }
    
    [SerializeField ] [Tooltip("配達可能なアイテムのリスト")]
    private DeliveryItem[] deliveryItems;
    private readonly Dictionary<ItemType, GameObject> _itemDictionary = new();

    private void OnValidate()
    {
        _itemDictionary.Clear();
        foreach (var item in deliveryItems)
        {
            if (!_itemDictionary.ContainsKey(item.ItemType))
            {
                _itemDictionary[item.ItemType] = item.ItemPrefab;
            }
        }
    }
    
    public GameObject GetItemPrefab(ItemType itemType)
    {
        if (_itemDictionary.TryGetValue(itemType, out var prefab))
        {
            return prefab;
        }
        Debug.LogWarning($"Item type {itemType} not found in DeliveryItemDatabase.");
        return null;
    }
}
