﻿using System;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Player
{
    public class PlayerManager : MonoBehaviour
    {
        public event Action Died;

        private const int OBSTACCLE_INDEX = 6;

        [SerializeField] private BangEffect _bangEffect;
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private ParticleSystem _particleSystem;

        private void OnEnable()
        {
            _gameManager.Reset += ResetPosition;
        }

        private void OnDisable()
        {
            _gameManager.Reset -= ResetPosition;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.layer == OBSTACCLE_INDEX)
            {
                Die();
            }
        }

        private void Die()
        {
            _bangEffect.gameObject.SetActive(true);
            _particleSystem.Play();
            GetComponent<SpriteRenderer>().enabled = false;
            Died?.Invoke();
        }

        private void ResetPosition()
        {
            transform.position = Vector3.zero;
            GetComponent<SpriteRenderer>().enabled = true;
            _particleSystem.Clear();
            _particleSystem.Stop();
            _bangEffect.gameObject.SetActive(false);
        }
    }
}