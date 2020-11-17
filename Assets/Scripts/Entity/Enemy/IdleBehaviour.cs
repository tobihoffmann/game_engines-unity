using Pathfinding;
using UnityEngine;

namespace Entity.Enemy
{
    public class IdleBehaviour : StateMachineBehaviour
    {
        private Transform _playerPos;
        private Transform _enemyPos;
        private float _distance;
        private AIDestinationSetter _aids;
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _playerPos = GameObject.FindGameObjectWithTag("Player").transform;
            _enemyPos = animator.transform;
            _aids = animator.GetComponent<AIDestinationSetter>();
            _aids.target = _enemyPos;
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            
            _distance = Vector2.Distance(_playerPos.position, _enemyPos.position);
            if (_distance <= animator.GetFloat("explodeDistance"))
            {
                animator.SetBool("exploding", true);
            }
            else if (_distance < animator.GetFloat("chaseDistance"))
            {
                animator.SetBool("isChasing", true);
            }
        }
    }
}
