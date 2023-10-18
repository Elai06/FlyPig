using System;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public event Action StartFly; 

        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private float _forceFly = 20;
        [SerializeField] private float _speedSpriteAnimation = 0.25f;

        [Header("Sprites")] 
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite[] _sprites;

        private float _flyTime;

        public void MovementUpdate()
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartFly?.Invoke();
            }
            
            if (Input.GetMouseButton(0))
            {
                _flyTime += Time.fixedDeltaTime;
                Fly();
            }

            if (Input.GetMouseButtonUp(0))
            {
                _flyTime = 0;
            }

            SpriteAnimation(_flyTime);
        }

        private void Fly()
        {
            _rb.AddRelativeForce(new Vector2(0, _forceFly));
        }

        private void SpriteAnimation(float time)
        {
            var index = (int)(time / _speedSpriteAnimation);

            if (index < _sprites.Length && index >= 0)
            {
                _spriteRenderer.sprite = _sprites[index];
            }
        }

        public void ResetSpeed()
        {
            SpriteAnimation(0);
        }

        public void GravitySwitch(bool isActive)
        {
            _rb.simulated = !isActive;
        }
    }
}