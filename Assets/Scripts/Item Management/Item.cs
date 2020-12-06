using System;
using Assets.Scripts.Item_Management;
using UnityEngine;

namespace Item_Management {

    [Serializable]
    public class Item {

        public enum ItemType
        {
            SpeedBuff,
            JuggernautBuff,
            ShootDamageBuff
        }

        public ItemType itemType;

        public Sprite GetSprite()
        {
            switch(itemType)
            {
                case ItemType.SpeedBuff: return ItemAssets.Instance.speedBuffSprite;
                case ItemType.JuggernautBuff: return ItemAssets.Instance.juggernautBuffSprite;
                case ItemType.ShootDamageBuff: return ItemAssets.Instance.shootDamageBuffSprite;
                default: return null;
            }
        }
        
        public Sprite GetIconSprite()
        {
            switch(itemType)
            {
                case ItemType.SpeedBuff: return ItemAssets.Instance.speedBuffIconSprite;
                case ItemType.JuggernautBuff: return ItemAssets.Instance.juggernautBuffIconSprite;
                case ItemType.ShootDamageBuff: return ItemAssets.Instance.shootDamageBuffIconSprite;
                default: return null;
            }
        }

        public int GetValue()
        {
            switch(itemType)
            {
                case ItemType.SpeedBuff: return 1;
                case ItemType.JuggernautBuff: return 2;
                case ItemType.ShootDamageBuff: return 1;
                default: return 0;
            }
        }

        public ItemType GetItemType()
        {
            return itemType;
        }
        
    }
}