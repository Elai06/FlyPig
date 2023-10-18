using System;
using _Project.Scripts.Gameplay.Player;
using UnityEngine;

namespace _Project.Scripts.Infrastructure.Audio
{
    public class AudioService : MonoBehaviour
    {
        [SerializeField] private AudioBank _audioBank;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private PlayerManager _playerManager;
        [SerializeField] private PlayerMovement _playerMovement;

        private void OnEnable()
        {
            _playerManager.Died += OnDied;
            _playerMovement.StartFly += OnStartFly;
        }

        private void OnDisable()
        {
            _playerManager.Died -= OnDied;
            _playerMovement.StartFly -= OnStartFly;
        }

        private void OnStartFly()
        {
           // PlaySound(EAudioName.Fly);
           Debug.Log($"Play fly audio");
        }

        private void OnDied()
        {
            PlaySound(EAudioName.Die);
        }

        public void PlaySound(EAudioName eAudioName)
        {
            _audioSource.clip = _audioBank.GetAudioClip(eAudioName);
            _audioSource.Play();
        }
    }
}