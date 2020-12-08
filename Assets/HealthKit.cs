using Entity.Player;
using Managers;
using UnityEngine;

public class HealthKit : MonoBehaviour
{
    [SerializeField]
    private int healAmount;

    private void  OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("DA");
        if (other.GetComponent<PlayerState>() != null) 
        {
            Debug.Log("HIER");
            PlayerManager.Instance.GetPlayerState().Heal(healAmount);
            Destroy(gameObject);
        }
    }
}
