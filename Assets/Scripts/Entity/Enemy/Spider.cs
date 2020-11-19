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

        [SerializeField] [Tooltip("Distance in which enemy chases the target.")]
        private float chaseDistance = 10f;
                
        [SerializeField] [Tooltip("Amount of damage the explosion deals.")]
        private int damage = 1;

        //[SerializeField] [Tooltip("Target to chase.")]
        private GameObject _target;

        private GameObject _player;
        
        private void Awake()
        {
            _target = PlayerManager.Instance.GetPlayer();
            Origin = gameObject;
            
            AIDestSetter = GetComponent<AIDestinationSetter>();
            Debug.Log(AIDestSetter.target);
            PlayerState = _target.GetComponent<PlayerState>();
        }


        private void Update()
        {
            
            
            Distance = Vector2.Distance(Origin.transform.position, _target.transform.position);
            if (Distance < explodeDistance)
                SwitchState(State.Attack);
            else if (Distance < chaseDistance)
                SwitchState(State.Chase);
            else 
                SwitchState(State.Idle);
                
        }

        protected override void Idle()
        {
            //TODO: implement patrolling or sth
        }

        protected override void Attack()
        {
            AIDestSetter.target = Origin.transform;
            
            Vector2 originPosition = Origin.transform.position;
            Vector2 targetPosition = new Vector2(_target.transform.position.x, _target.transform.position.y - .6f);
            RaycastHit2D hit = Physics2D.Raycast(originPosition, (targetPosition - originPosition).normalized, explodeDistance);
            //Debug.Log(Distance);
            Debug.DrawRay(originPosition, (targetPosition - originPosition), Color.red);

            //if target not behind cover, deal damage.
            if (hit.collider.gameObject == _target)
            {
                PlayerState.Hit(damage);
            }
        }

        protected override void Chase()
        {
            AIDestSetter.target = _target.transform;
        }
    }
}
