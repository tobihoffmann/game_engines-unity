using Entity.Player;
using Pathfinding;
using UnityEngine;

namespace Entity.Enemy
{
    public class ExplodeBehaviour : StateMachineBehaviour
    {
        
        [SerializeField][Tooltip("Amount of damage explosion deals")]
        private int dmg = 5;
        
        private GameObject _player;
        private Transform _playerPos;
        private Transform _enemyPos;
        private AIDestinationSetter _aids;
        private PlayerState _ps;
        private float _distance;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _aids = animator.GetComponent<AIDestinationSetter>();
            _aids.target = null;

            _player = GameObject.FindGameObjectWithTag("Player");
            _playerPos = _player.transform;
            _enemyPos = animator.transform;

            _ps = _player.GetComponent<PlayerState>();
            
        }
        
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _distance = Vector2.Distance(_playerPos.position, _enemyPos.position);
            //if player not behind cover, deal damage
            RaycastHit2D hit = Physics2D.Raycast(_enemyPos.position, _playerPos.position - _enemyPos.position, _distance+1);
            Debug.DrawRay(_enemyPos.position, (_playerPos.position - _enemyPos.position), Color.red);
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.name);
            }
            
            if (hit.collider != null && hit.collider.gameObject.CompareTag("Player"))
            {
                Debug.Log(hit.collider.name);
                _ps.Hit(CalcDmg(dmg));
            }
        }
    
        //damage calculation based on the distance (linear)
        private int CalcDmg(int dmg)
        {
            return (int)(dmg / _distance);
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}
        
    }
}
