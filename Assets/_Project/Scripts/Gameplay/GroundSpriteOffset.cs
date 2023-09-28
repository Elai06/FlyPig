using _Project.Scripts.Gameplay.Obstacles;
using UnityEngine;

namespace _Project.Scripts.Gameplay
{
    public class GroundSpriteOffset : MonoBehaviour
    { 
        private readonly float _speedOfssed = 6.666f;

        [SerializeField] private GameManager _gameManager;
        [SerializeField] private ObstaclesSpawner _obstaclesSpawner;

        private Material _material;
        private Vector2 _offsetPosition;

        private void Awake()
        {
            var image = gameObject.GetComponent<SpriteRenderer>();
            _material = image.material;
            _offsetPosition = _material.mainTextureOffset;
        }

        private void OnDisable()
        {
            ResetPosition();
        }

        private void Update()
        {
            if (!_gameManager.IsCanPlay) return;
            Offset();
        }

        private void ResetPosition()
        {
            _material.mainTextureOffset = Vector2.zero;
        }

        private void Offset()
        {
            var speed = _obstaclesSpawner.Speed / _speedOfssed;
            
            _offsetPosition.x += speed * Time.deltaTime;
            _material.mainTextureOffset = _offsetPosition;
        }
    }
}