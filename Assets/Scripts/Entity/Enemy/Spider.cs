using Entity.Player;
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

        [SerializeField] [Tooltip("Target to chase.")]
        private GameObject Target;
        
        
        private void Awake()
        {
            AIDestSetter = GetComponent<AIDestinationSetter>();
            
            Debug.Log(AIDestSetter.target);

            Origin = gameObject;
            
            PlayerState = Target.GetComponent<PlayerState>();
        }


        void Update()
        {
            Distance = Vector2.Distance(Origin.transform.position, Target.transform.position);
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
            
            Vector3 originPosition = Origin.transform.position;
            Vector3 targetPosition = Target.transform.position;
            RaycastHit2D hit = Physics2D.Raycast(originPosition, (targetPosition - originPosition).normalized, Distance);
            
            Debug.DrawRay(originPosition, hit.point, Color.red);

            //if target not behind cover, deal damage.
            if (hit.collider.gameObject == Target)
            {
                PlayerState.Hit(damage);
            }
        }

        protected override void Chase()
        {
            AIDestSetter.target = Target.transform;
        }
    }
}
