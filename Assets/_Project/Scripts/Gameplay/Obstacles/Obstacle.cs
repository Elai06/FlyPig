using UnityEngine;

namespace _Project.Scripts.Gameplay.Obstacles
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private Transform _endPoint;

        public Transform EndPoint => _endPoint;
    }
}