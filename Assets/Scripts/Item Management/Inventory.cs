using System;
using System.Collections.Generic;
using Assets.Scripts.Item_Management;
using Managers;
using UnityEngine;

namespace Item_Management
{
    public class Inventory
    { 
        public delegate void OnPowerUpUpdate(int newValue);
        
        private List<Item> itemList;
        
        public event EventHandler OnItemListChanged;
        public static event OnPowerUpUpdate OnJuggernautUpdate;
        public static event OnPowerUpUpdate onMovementSpeedUpdate;
        
        public Inventory()
        {
            itemList = new List<Item>();
        }

        public void AddItem(Item item)
        {
            itemList.Add(item);
            OnItemListChanged?.Invoke(this, EventArgs.Empty);
            if (item.GetItemType() == Item.ItemType.SpeedBuff) onMovementSpeedUpdate?.Invoke(item.GetValue());
            if (item.GetItemType() == Item.ItemType.JuggernautBuff) OnJuggernautUpdate?.Invoke(item.GetValue());
        }

        public void RemoveItem(Item item)
        {
            itemList.Remove(item);
            OnItemListChanged?.Invoke(this, EventArgs.Empty);
            if (item.GetItemType() == Item.ItemType.SpeedBuff) onMovementSpeedUpdate?.Invoke(-item.GetValue());
            if (item.GetItemType() == Item.ItemType.JuggernautBuff) OnJuggernautUpdate?.Invoke(-item.GetValue());
        }

        public List<Item> GetItemList()
        {
            return itemList;
        }
    }
}