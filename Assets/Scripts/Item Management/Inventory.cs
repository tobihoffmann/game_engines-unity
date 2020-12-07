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
        
        private List<Item> _itemList;
        
        public event EventHandler OnItemListChanged;
        public static event OnPowerUpUpdate OnJuggernautUpdate;
        public static event OnPowerUpUpdate OnMovementSpeedUpdate;
        public static event OnPowerUpUpdate OnShootingDamageUpdate;
        
        public Inventory()
        {
            _itemList = new List<Item>();
        }

        public void AddItem(Item item)
        {
            _itemList.Add(item);
            OnItemListChanged?.Invoke(this, EventArgs.Empty);
            if (item.GetItemType() == Item.ItemType.SpeedBuff) OnMovementSpeedUpdate?.Invoke(item.GetValue());
            if (item.GetItemType() == Item.ItemType.JuggernautBuff) OnJuggernautUpdate?.Invoke(item.GetValue());
            if (item.GetItemType() == Item.ItemType.ShootDamageBuff) OnShootingDamageUpdate?.Invoke(item.GetValue());
        }

        public void RemoveItem(Item item)
        {
            _itemList.Remove(item);
            OnItemListChanged?.Invoke(this, EventArgs.Empty);
            if (item.GetItemType() == Item.ItemType.SpeedBuff) OnMovementSpeedUpdate?.Invoke(-item.GetValue());
            if (item.GetItemType() == Item.ItemType.JuggernautBuff) OnJuggernautUpdate?.Invoke(-item.GetValue());
            if (item.GetItemType() == Item.ItemType.ShootDamageBuff) OnShootingDamageUpdate?.Invoke(-item.GetValue());
        }

        public List<Item> GetItemList()
        {
            return _itemList;
        }
    }
}