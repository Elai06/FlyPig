using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Infrastructure.Audio
{
    [Serializable]
    public class AudioData
    {
        public EAudioName AudioName;
        public List<AudioClip> AudioClips;
    }
}