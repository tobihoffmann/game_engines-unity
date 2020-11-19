using Entity.Player;
using Managers;
using Pathfinding;
using UnityEngine;

namespace Entity.Enemy
{
    public class ExplodeBehaviour : StateMachineBehaviour
    {
        
        // [SerializeField][Tooltip("Amount of damage explosion deals")]
        // private int dmg = 1;
        //
        // private GameObject player = PlayerManager.Instance.GetPlayer();
        //
        // private Vector3 _playerPos;
        // private Transform _enemyPos;
        // private AIDestinationSetter _aids;
        // private PlayerState _ps;
        // private float _distance;
        //
        //
        // override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        // {
        //     _aids = animator.GetComponent<AIDestinationSetter>();
        //     _aids.target = null;
        //
        //     _playerPos = PlayerManager.Instance.GetPlayerPosition();
        //     _enemyPos = animator.transform;
        //
        //     _ps = player.GetComponent<PlayerState>();
        //     
        // }
        //
        // override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        // {
        //     _distance = Vector2.Distance(_playerPos, _enemyPos.position);
        //     
        //     RaycastHit2D hit = Physics2D.Raycast(_enemyPos.position, (_playerPos - _enemyPos.position).normalized, _distance);
        //     
        //     Debug.DrawRay(_enemyPos.position, hit.point, Color.red);
        //     Debug.Log(hit.collider.name);
        //     Debug.Log(_playerPos);
        //     //if player not behind cover, deal damage
        //     if (hit.collider != null && hit.collider.gameObject == player)
        //     {
        //         _ps.Hit(dmg);
        //     }
        // }
    
        //damage calculation based on the distance (linear)
        // private int CalcDmg(int dmg)
        // {
        //     return (int)(dmg / _distance);
        // }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}
        
    }
}
