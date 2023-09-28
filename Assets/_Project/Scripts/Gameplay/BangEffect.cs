using UnityEngine;

namespace _Project.Scripts.Gameplay
{
    public class BangEffect : MonoBehaviour
    {
        [SerializeField] private Sprite[] _bangSprites;
        [SerializeField] private SpriteRenderer _bangSpriteRenderer;

        private float _time;

        private void Update()
        {
            if (gameObject.activeSelf)
            {
                Animation();
            }
        }

        private void OnDisable()
        {
            _time = 0;
        }

        private void Animation()
        {
            _time += Time.deltaTime * 4f;

            if (_time < _bangSprites.Length && _time >= 0)
            {
                _bangSpriteRenderer.sprite = _bangSprites[(int)_time];
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}