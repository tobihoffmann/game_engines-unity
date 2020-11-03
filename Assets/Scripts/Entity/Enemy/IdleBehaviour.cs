using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class IdleBehaviour : StateMachineBehaviour
{
    private Transform playerPos;
    private Transform enemyPos;
    private float distance;
    private AIDestinationSetter aids;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        enemyPos = animator.transform;
        aids = animator.GetComponent<AIDestinationSetter>();
        aids.target = enemyPos;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        distance = Vector3.Distance(playerPos.position, enemyPos.position);
        if (distance < animator.GetFloat("chaseDistance"))
        {
            animator.SetBool("isChasing", true);
        }
        Debug.Log(distance);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        aids.target = playerPos;
    }
}
