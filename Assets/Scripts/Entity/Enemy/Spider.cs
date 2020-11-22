using System.Collections;
using Entity.Player;
using Managers;
using Pathfinding;
using UnityEngine;

namespace Entity.Enemy
{
    public class Spider : StateMachine
    {
        [SerializeField][Tooltip("Distance in which enemy explodes.")]
        private float explodeDistance = 3f;

        [SerializeField][Tooltip("Distance in which enemy chases the target.")]
        private float chaseDistance = 10f;
                
        [SerializeField][Tooltip("Amount of damage the explosion deals.")]
        private int damage = 1;

        [SerializeField] [Tooltip("Time in seconds until spider explodes.")]
        private float exTime = .5f;
        
        private Animator _animator;
        
        //private bool _isExploding = false;

        private GameObject target;
        
        
        private void Start()
        {
            target = PlayerManager.Instance.GetPlayer();
            Origin = gameObject;
            AIDestSetter = GetComponent<AIDestinationSetter>();
            PlayerState = target.GetComponent<PlayerState>();
            _animator = GetComponent<Animator>();
        }


        private void Update()
        {
            Distance = Vector2.Distance(Origin.transform.position, target.transform.position);
            if (Distance < explodeDistance)
                SwitchState(State.Attack);
            else if (Distance < chaseDistance && _animator.GetBool("isExploding") == false)
                SwitchState(State.Chase);
            else 
                SwitchState(State.Idle);
        }

        protected override void Idle()
        {
            if (AIDestSetter.target != null && AIDestSetter.target != Origin.transform)
                AIDestSetter.target = Origin.transform;
            //TODO: implement patrolling or sth
        }

        /// <summary>
        /// Attack method of spider. Spider stops chasing and explodes.
        /// </summary>
        protected override void Attack()
        {
            AIDestSetter.target = Origin.transform;

            //Box Collider Offset
            Vector2 bcoffset = target.GetComponent<BoxCollider2D>().offset;
            
            //Position of origin (spider)
            Vector2 originPosition = Origin.transform.position;
            
            Vector2 targetPosition = target.transform.position;
            
            //Position of collider of the target (player)
            Vector2 finalTargetPos = targetPosition + bcoffset;
            
            RaycastHit2D hit = Physics2D.Raycast(originPosition, (finalTargetPos - originPosition).normalized, explodeDistance+1);
            Debug.DrawRay(originPosition, (finalTargetPos - originPosition), Color.red);
            StartCoroutine(WaitAndExplode(exTime, hit));
        }

        /// <summary>
        /// Spider waits and then explode. Player gets damage, if not behind cover.
        /// </summary>
        /// <param name="explodeTime">Time in seconds until spider explodes.</param>
        /// <param name="hit">The hit Raycast from the spider to the player.</param>
        /// <returns></returns>
        private IEnumerator WaitAndExplode(float explodeTime, RaycastHit2D hit)
        {
            
            yield return new WaitForSeconds(explodeTime);
            _animator.SetBool("isExploding", true);
            yield return new WaitForSeconds(0.5f);
            if (hit.collider != null && hit.collider.gameObject == target)
            {
                PlayerState.Hit(damage);
            }
            Destroy(gameObject);
        }

        protected override void Chase()
        {
            AIDestSetter.target = target.transform;
        }
    }
}
