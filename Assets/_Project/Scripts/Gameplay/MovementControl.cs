using System;
using UnityEngine;

namespace _Project.Scripts.Gameplay
{
    public class MovementControl : MonoBehaviour
    {
        [SerializeField] private float _maxSpeedFly;
        [SerializeField] private float _flyAcclereation;
        [SerializeField] private float _maxSpeedLanding;
        [SerializeField] private float _landingAccleration;

        private MovementState _movementState = MovementState.Landing;

        private float _currentSpeed;

        void Update()
        {
            
            if (Input.GetMouseButton(0))
            {
                ChangeMovementState(MovementState.Fly);
                Fly();
                return;
            }

            if (Input.GetMouseButtonUp(0))
            {
                ChangeMovementState(MovementState.Landing);
            }

            if (_movementState == MovementState.Landing)
            {
                Landing();
            }
        }

        private void Fly()
        {
            if(_movementState != MovementState.Fly) return;
            
            if (_currentSpeed < _maxSpeedFly)
            {
                _currentSpeed += _flyAcclereation * Time.deltaTime;
            }

            ChangePositin(Vector3.up * _currentSpeed);
        }

        private void ChangeMovementState(MovementState state)
        {
            _movementState = state;
        }

        private void Landing()
        {
            if (_movementState == MovementState.Landing)
            {
                if (_currentSpeed < _maxSpeedLanding)
                {
                    _currentSpeed -= _landingAccleration * Time.deltaTime;
                }

                ChangePositin(Vector3.up * _currentSpeed);
            }
        }

        private void ChangePositin(Vector3 position)
        { 
            if (transform.position.y <= 0f && _currentSpeed <= 0)
            {
                transform.position = Vector3.zero;
                _currentSpeed = 0;
                return;
            }
            
            transform.position += position;
        }
    }
}