using System;
using UnityEngine;
using System.Collections.Generic;
using Entity.Player;
using Managers;
using UnityEngine.Video;


namespace Assets.Scripts.Item_Management
{
    public class Inventory
    {
        private List<Item> itemList;
        public event EventHandler OnItemListChanged;
        
        public delegate void MovementSpeedEquiped(int newMovementSpeedVal);
        
        public delegate void JuggernautEquiped(int newHealthValue);
        
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
            if (item.GetItemType() == Item.ItemType.JuggernautBuff) onJuggernautEquiped?.Invoke(item.GetValue());
            
                
                
                
            Debug.Log(PlayerManager.Instance.GetPlayerState().maxHitPoints1);
        }

        public void RemoveItem(Item item)
        {
            itemList.Remove(item);
            OnItemListChanged?.Invoke(this, EventArgs.Empty);
            if (item.GetItemType() == Item.ItemType.SpeedBuff) onMovementSpeedUpdate?.Invoke(-(item.GetValue()));
            if (item.GetItemType() == Item.ItemType.JuggernautBuff) onJuggernautEquiped?.Invoke(-(item.GetValue()));
        }

        public List<Item> GetItemList()
        {
            return itemList;
        }
    }
}