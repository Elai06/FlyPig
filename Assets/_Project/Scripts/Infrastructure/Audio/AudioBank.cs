using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Infrastructure.Audio
{
    [CreateAssetMenu(menuName = "Data/CreateAudioBank", fileName = "AudioBank", order = 0)]
    public class AudioBank : ScriptableObject
    {
        [SerializeField] private List<AudioData> _audioData;

        public AudioClip GetAudioClip(EAudioName audioName)
        {
            var clips = _audioData.Find(x => x.AudioName == audioName).AudioClips;
            if (clips.Count <= 1) return clips[0];

            var random = Random.Range(0, clips.Count);
            return clips[random];
        }
    }

    public enum EAudioName
    {
        Music,
        Die,
        Fly,
    }
}