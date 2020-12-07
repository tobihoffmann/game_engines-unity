using Entity.Player;
using Managers;
using UnityEngine;

public class HealthKit : MonoBehaviour
{
    [SerializeField]
    private int healAmount;

    PlayerState ps;
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ps = PlayerManager.Instance.GetPlayerState();
        ps.Heal(healAmount);
        Destroy(this);
    }
}
