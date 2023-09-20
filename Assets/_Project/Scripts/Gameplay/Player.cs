using System;
using UnityEngine;

namespace _Project.Scripts.Gameplay
{
    public class Player : MonoBehaviour
    {
        private const int OBSTACCLE_INDEX = 6;

        [SerializeField] private GameManager _gameManager;
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.layer == OBSTACCLE_INDEX)
            {
                Died();
            }
        }

        private void Died()
        {
            _gameManager.Reset();
        }
    }
}