using System;
using Units.Enemy.StateMachine.States;
using UnityEngine;

namespace Units.Enemy.StateMachine
{
    [Serializable]
    public class EnemyStateMachine
    {
        [SerializeField] private DefaultStateType _defaultStateType;
        [SerializeField] private DangerStateType _dangerStateTypeState;
        private State _defaultState;
        private State _dangerState;
        private State _currentState;
        
        public void Init(Enemy enemy)
        {
            switch (_defaultStateType)
            {
                case DefaultStateType.Off:
                    
                    _defaultState = new OffState(enemy);
                    break;
                case DefaultStateType.Idle:
                    _defaultState = new Idle(enemy);
                    break;
                case DefaultStateType.Patrol:
                    _defaultState = new Patrol(enemy);
                    break;
            }   
            
            switch (_dangerStateTypeState)
            {
                case DangerStateType.Follow:
                    break;
                case DangerStateType.FollowAndAttack:
                    break;
                case DangerStateType.KeepDistanceAndAttack:
                    break;
                case DangerStateType.RunAway:
                    break;
            }   
            _defaultState.IsStateChange += ChangeState;
            ChangeState();
        }

        public void Update()
        {
            _currentState?.OnUpdate();
        }

        private void ChangeState()
        {
            _currentState?.OnExit();
            _currentState = _currentState == _defaultState ? _dangerState : _defaultState;
            _currentState?.OnEnter();
        }
    }
}