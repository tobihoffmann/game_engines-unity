using UnityEngine;

namespace Assets.Scripts.Item_Management
{
    public class ItemAssets : MonoBehaviour
    {
        public static ItemAssets Instance { get; private set; }
        
        public Transform pfItemWorld;
        
        public Sprite juggernautBuffSprite;
        public Sprite speedBuffSprite;


        private void Awake()
        {
            Instance = this;
        }
        
    }
    
}