using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Video;


namespace Assets.Scripts.Item_Management
{
    public class Inventory
    {
        private List<Item> itemList;
        public event EventHandler OnItemListChanged;


        public Inventory()
        {
            itemList = new List<Item>();
            AddItem(new Item{itemType = Item.ItemType.JuggernautBuff, amount = 1});
            AddItem(new Item{itemType = Item.ItemType.SpeedBuff, amount = 1});

        }

        public void AddItem(Item item)
        {
            itemList.Add(item);
            OnItemListChanged?.Invoke(this, EventArgs.Empty);
        }

        public List<Item> GetItemList()
        {
            return itemList;
        }
    }
}