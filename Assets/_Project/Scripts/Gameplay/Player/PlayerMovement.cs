using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Scripts.Gameplay.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _maxSpeedFly;
        [SerializeField] private float _flyAcclereation;
        [SerializeField] private float _maxSpeedLanding;
        [SerializeField] private float _landingAccleration;

        [Header("Sprites")] [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite[] _sprites;

        [SerializeField] private GameManager _gameManager;

        private MovementState _movementState = MovementState.Landing;

        private float _currentSpeed;

        public void MovementUpdate()
        {
            SpriteAnimation();
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
            if (_movementState != MovementState.Fly || transform.position.y >= 9)
            {
                ChangeMovementState(MovementState.Landing);
                ResetSpeed();
                return;
            }

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
                ResetSpeed();
                return;
            }

            transform.position += position;
        }

        private void SpriteAnimation()
        {
            var stepAnimation = (_maxSpeedFly - 0.125) / _sprites.Length;
            var index = (int)(_currentSpeed / stepAnimation);

            if (index < _sprites.Length && index >= 0)
            {
                _spriteRenderer.sprite = _sprites[index];
            }
        }

        public void ResetSpeed()
        {
            _currentSpeed = 0;
            SpriteAnimation();
        }
    }
}