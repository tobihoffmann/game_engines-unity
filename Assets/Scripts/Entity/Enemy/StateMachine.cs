using System;
using Entity.Player;
using Pathfinding;
using UnityEngine;

namespace Entity.Enemy
{
    public abstract class StateMachine : MonoBehaviour
    {
        protected AIDestinationSetter AIDestSetter;
        
        protected float Distance;
        protected GameObject Target;
        protected GameObject Origin;

        protected PlayerState PlayerState;
        protected enum State
        {
            Idle,
            Chase,
            Attack,
            Heal,
            Dead
        }

        protected State _state;
        


        protected void SwitchState(State _state)
        {
            switch (_state)
            {
                case State.Idle:
                    Idle();
                    break;
                case State.Chase:
                    Chase();
                    break;
                case State.Attack:
                    Attack();
                    break;
                case State.Heal:
                    Heal();
                    break;
                case State.Dead:
                    Dead();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        protected virtual void Idle()
        {
            throw new NotImplementedException();
        }

        protected virtual void Chase()
        {
            throw new NotImplementedException();
        }

        protected virtual void Attack()
        {
            throw new NotImplementedException();
        }

        protected virtual void Heal()
        {
            throw new NotImplementedException();
        }

        protected virtual void Dead()
        {
            throw new NotImplementedException();
        }
        
    }
}
