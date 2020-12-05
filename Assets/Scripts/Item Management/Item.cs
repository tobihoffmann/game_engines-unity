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
            JuggernautBuff
        }

        public ItemType itemType;

        public Sprite GetSprite()
        {
            switch(itemType)
            {
                default:
                case ItemType.SpeedBuff: return ItemAssets.Instance.speedBuffSprite;
                case ItemType.JuggernautBuff: return ItemAssets.Instance.juggernautBuffSprite;
            }
        }

        public int GetValue()
        {
            switch(itemType)
            {
                default:
                case ItemType.SpeedBuff: return 3;
                case ItemType.JuggernautBuff: return 4;
            }
        }

        public ItemType GetItemType()
        {
            return itemType;
        }
        
    }
}