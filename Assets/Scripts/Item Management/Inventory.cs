using System;
using UnityEngine;
using System.Collections.Generic;
using Entity.Player;
using UnityEngine.Video;


namespace Assets.Scripts.Item_Management
{
    public class Inventory
    {
        private List<Item> itemList;
        public event EventHandler OnItemListChanged;
        
        public delegate void MovementSpeedEquiped(int newMovementSpeedVal);
        
        public delegate void JuggernautEquiped(int newHealtValue);


        public static event JuggernautEquiped onJuggernautEquiped;    

        public static event MovementSpeedEquiped onMovementSpeedUpdate;
        
        
        [SerializeField] private PlayerMovementWalking _playerMovementWalking;

        public Inventory()
        {
            itemList = new List<Item>();
        }

        public void AddItem(Item item)
        {
            itemList.Add(item);
            OnItemListChanged?.Invoke(this, EventArgs.Empty);
            if (item.GetItemType() == Item.ItemType.SpeedBuff) onMovementSpeedUpdate?.Invoke(item.GetValue());

        }

        public void RemoveItem(Item item)
        {
            itemList.Remove(item);
            OnItemListChanged?.Invoke(this, EventArgs.Empty);
            if (item.GetItemType() == Item.ItemType.SpeedBuff) onMovementSpeedUpdate?.Invoke(-(item.GetValue()));
        }

        public List<Item> GetItemList()
        {
            return itemList;
        }
    }
}