using UnityEngine;

namespace Assets.Scripts.Item_Management
{
    public class ItemAssets : MonoBehaviour
    {
        public static ItemAssets Instance { get; private set; }
        
        public Transform pfItemWorld;
        
        public Sprite juggernautBuffSprite;
        public Sprite speedBuffSprite;
        public Sprite shootDamageBuffSprite;
        
        public Sprite juggernautBuffIconSprite;
        public Sprite speedBuffIconSprite;
        public Sprite shootDamageBuffIconSprite;

        

        private void Awake()
        {
            Instance = this;
        }
        
    }
    
}