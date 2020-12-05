using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts.Item_Management {

    [Serializable]
    public class Item {

        public enum ItemType
        {
            SpeedBuff,
            JuggernautBuff,
            ShootFastBuff
        }

        public ItemType itemType;

        public Sprite GetSprite()
        {
            switch(itemType)
            {
                default:
                case ItemType.SpeedBuff: return ItemAssets.Instance.speedBuffSprite;
                case ItemType.JuggernautBuff: return ItemAssets.Instance.juggernautBuffSprite;
                case ItemType.ShootFastBuff: return ItemAssets.Instance.shootFastBuffSprite;
            }
        }

        public int GetValue()
        {
            switch(itemType)
            {
                default:
                case ItemType.SpeedBuff: return 3;
                case ItemType.JuggernautBuff: return 2;
                case ItemType.ShootFastBuff: return 2;
            }
        }

        public ItemType GetItemType()
        {
            return itemType;
        }
        
    }
}