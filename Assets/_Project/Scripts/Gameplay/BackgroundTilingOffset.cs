using UnityEngine;

namespace _Project.Scripts.Gameplay
{
    public class BackgroundTilingOffset : MonoBehaviour
    {
        [SerializeField] private float _speedOfssed = 0.00025f;

        [SerializeField] private GameManager _gameManager;

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
            if (!_gameManager.IsStart) return;
            Offset();
        }

        private void ResetPosition()
        {
            _material.mainTextureOffset = Vector2.zero;
        }

        private void Offset()
        {
            _offsetPosition.x += _speedOfssed * Time.deltaTime;
            _material.mainTextureOffset = _offsetPosition;
        }
    }
}