using Entity.Player;
using Managers;
using Pathfinding;
using UnityEngine;

namespace Entity.Enemy
{
    public class Demon : StateMachine
    {
        [SerializeField][Tooltip("Distance in which enemy chases the target.")]
        private float chaseDistance = 10f;

        [SerializeField] [Tooltip("Dinstance in which ranged enemy starts attacking")]
        private float attackDistance;
        
        [SerializeField] [Tooltip("Wander radius of AI.")]
        private float radius = 2f;
        
        [SerializeField][Tooltip("Prefab for Bullet Object")]
        private GameObject bulletPrefab;
        
        [SerializeField][Tooltip("Float variable for the Bullet Force")]
        private float bulletForce = 50f;
        
        [SerializeField][Tooltip("Cooldown time for shooting")]
        private float coolDown;

        private IAstarAI _ai;
        private AIPath _aiPath;
        

        private GameObject _target;
        private Vector2 _shootDirection;
        private float _t;

        private Animator _animator;
        private void Start()
        {
            _t = coolDown;
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
            if (Distance < chaseDistance)
            {
                AnimationDecision();
            }
            Distance = Vector2.Distance(Origin.transform.position, _target.transform.position);
            if (Distance < attackDistance)
                SwitchState(State.Attack);
            else if (Distance < chaseDistance)
                SwitchState(State.Chase);
            else 
                SwitchState(State.Idle);
        }
        
        protected override void Idle()
        {
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
        
        private Vector3 PickRandomPoint () {
            Vector3 point = Random.insideUnitSphere * radius;
            point.z = 0;
            point += _ai.position;
            return point;
        }
        
        protected override void Chase()
        {
            //Increase movement speed while in chase mode
            _aiPath.maxSpeed = 4f;
            AIDestSetter.target = _target.transform;
        }
        
        protected override void Attack()
        {
            _aiPath.maxSpeed = 0f;
            AIDestSetter.target = Origin.transform;

            //Box Collider Offset
            Vector2 bcoffset = _target.GetComponent<BoxCollider2D>().offset;
            
            //Position of origin (demon)
            Vector2 originPosition = Origin.transform.position;
            
            Vector2 targetPosition = _target.transform.position;
            
            //Position of collider of the target (player)
            Vector2 finalTargetPos = targetPosition + bcoffset;
            
            _t = _t - Time.deltaTime;
            
            if (_t <= 0)
            {
                Shoot(originPosition, targetPosition);
            }

            Debug.DrawRay(originPosition, (finalTargetPos - originPosition), Color.red);
        }
        
        private void Shoot(Vector2 originPos, Vector2 targetPos)
        {
            
            _shootDirection = new Ray2D(originPos, targetPos - originPos).direction;

            //initiate the bullet
            GameObject bullet = Instantiate(bulletPrefab, originPos, Origin.transform.rotation);
        
            // Makes sure the bullet doesn't collide with the Object that is shooting it
            Physics2D.IgnoreCollision(bullet.GetComponent<BoxCollider2D>(), transform.GetComponent<BoxCollider2D>());

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            //turn the bullet in the correct direction
            float angle = Mathf.Atan2(_shootDirection.y, _shootDirection.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = angle;

            //addForce in direction of target position
            rb.AddForce(_shootDirection * bulletForce, ForceMode2D.Impulse);

            _t = coolDown;
        }

        private void AnimationDecision()
        {
            Vector2 pos = (_target.transform.position - Origin.transform.position);
            float angle = Vector2.SignedAngle(Vector2.up, pos);
            //FRONT
            if (angle < 45 && angle > -45)
                _animator.Play("Demon_back");
            //LEFT
            else if (angle >= 45 && angle <= 135)
                _animator.Play("Demon_left");
            //RIGHT
            else if (angle <= -45 && angle >= -135)
                _animator.Play("Demon_right");
            //BACK
            else
                _animator.Play("Demon_Idle");
        }
        
        /// <summary>
        /// Visualize Demons
        /// </summary>
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, 1);
        }
    }
}