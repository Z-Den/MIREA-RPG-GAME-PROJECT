using PivotConnection;
using Units.Input;
using UnityEngine;

namespace Units.Player
{
    public class Player : Unit, IPivot, IUnitActionController 
    {
        [SerializeField] private Transform _pivotTransform;
        [SerializeField] private PhysicalMover _physicalMover;
        [SerializeField] private Rigidbody _rigidbody;
        private IUnitInput _inputActions;
        public Transform PivotTransform => _pivotTransform;

        private void Start()
        {
            _inputActions.RunStarted += _physicalMover.RunStarted;
            _inputActions.RunCanceled += _physicalMover.RunCanceled;
        }

        private void OnDisable()
        {
            _inputActions.RunStarted -= _physicalMover.RunStarted;
            _inputActions.RunCanceled -= _physicalMover.RunCanceled;
        }

        private void Update()
        {
            var moveDirection = (_inputActions.MoveDirection.y * _rigidbody.transform.forward + 
                                 _inputActions.MoveDirection.x * _rigidbody.transform.right);
            _physicalMover.SetMoveDirection(moveDirection);
            _physicalMover.SetRotationDegree(_inputActions.Rotation);
        }

        IUnitInput IUnitActionController.InputActions
        {
            get => _inputActions;
            set => _inputActions = value;
        }
    }
}
