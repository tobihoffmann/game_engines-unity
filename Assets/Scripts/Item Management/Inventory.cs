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
        }

        public void AddItem(Item item)
        {
            itemList.Add(item);
            OnItemListChanged?.Invoke(this, EventArgs.Empty);
        }

        public void RemoveItem(Item item)
        {
            itemList.Remove(item);
            OnItemListChanged?.Invoke(this, EventArgs.Empty);
        }

        public List<Item> GetItemList()
        {
            return itemList;
        }
    }
}