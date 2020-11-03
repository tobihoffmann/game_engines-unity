using UnityEngine;

namespace PowerUps
{
    [CreateAssetMenu(fileName = "New Power-Up", menuName = "Power-Up")]
    public class PowerUp : ScriptableObject
    {
        [SerializeField]
        private string title;
        
        [SerializeField]
        private Sprite icon;
        
        [SerializeField]
        private int bonusHp;
        
        [SerializeField]
        private int bonusAmmo;
        
        [SerializeField]
        private int bonusMeleeDamage;
        
        [SerializeField]
        private int bonusRangeDamage;
        
        [SerializeField]
        private WeaponModifier weaponMod;
    }
}