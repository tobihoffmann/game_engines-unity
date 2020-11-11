
using Pathfinding;
using UnityEngine;

public class ChaseBehaviour : StateMachineBehaviour
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
        _aids.target = _playerPos;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _distance = Vector2.Distance(_playerPos.position, _enemyPos.position);
        if (_distance > animator.GetFloat("chaseDistance") || _distance < animator.GetFloat("explodeDistance"))
        {
            animator.SetBool("isChasing", false);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _aids.target = null;
    }
}
