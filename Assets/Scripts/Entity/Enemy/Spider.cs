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
        
        [SerializeField] [Tooltip("Wander radius of AI.")]
        private float radius = 2f;

        private IAstarAI _ai;
        private AIPath _aiPath;
        
        private bool _isExploding;

        private GameObject _target;
        
        
        private void Start()
        {
            _target = PlayerManager.Instance.GetPlayer();
            _ai = GetComponent<IAstarAI>();
            _aiPath = GetComponent<AIPath>();
            Origin = gameObject;
            AIDestSetter = GetComponent<AIDestinationSetter>();
            PlayerState = _target.GetComponent<PlayerState>();
            _animator = GetComponent<Animator>();
        }


        private void Update()
        {
            Distance = Vector2.Distance(Origin.transform.position, _target.transform.position);
            if (_isExploding == false)
            {
                if (Distance < explodeDistance)
                    SwitchState(State.Attack);
                else if (Distance < chaseDistance)
                    SwitchState(State.Chase);
                else 
                    SwitchState(State.Idle);
            }
        }
        

        protected override void Idle()
        {
            _animator.Play("Spider_idle");
            AIDestSetter.target = null;
            //Lower movement speed while patrolling
            _aiPath.maxSpeed = 1.5f;
            // Update the destination of the AI if
            // the AI is not already calculating a path and
            // the ai has reached the end of the path or it has no path at all
            if (!_ai.pathPending && (_ai.reachedEndOfPath || !_ai.hasPath)) {
                _ai.destination = PickRandomPoint();
                _ai.SearchPath();
            }
        }

        /// <summary>
        /// Attack method of spider. Spider stops chasing and explodes.
        /// </summary>
        protected override void Attack()
        {
            _isExploding = true;
            
            _aiPath.maxSpeed = 0f;
            AIDestSetter.target = Origin.transform;

            //Box Collider Offset
            Vector2 bcoffset = _target.GetComponent<BoxCollider2D>().offset;
            
            //Position of origin (spider)
            Vector2 originPosition = Origin.transform.position;
            
            Vector2 targetPosition = _target.transform.position;
            
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
            _isExploding = true;
            _animator.Play("Spider_attack");
            yield return new WaitForSeconds(explodeTime);
            _animator.Play("Spider_explode");
            yield return new WaitForSeconds(.25f);
            if (hit.collider != null && hit.collider.gameObject == _target && Distance <= explodeDistance)
            {
                PlayerState.Hit(damage);
            }
            Destroy(gameObject);
        }
        
        protected override void Chase()
        {
            _animator.Play("Spider_chase");
            //Increase movement speed while in chase mode
            _aiPath.maxSpeed = 8f;
            AIDestSetter.target = _target.transform;
        }
        
        private Vector3 PickRandomPoint () {
            Vector3 point = Random.insideUnitSphere * radius;
            point.z = 0;
            point += _ai.position;
            return point;
        }
    }
}