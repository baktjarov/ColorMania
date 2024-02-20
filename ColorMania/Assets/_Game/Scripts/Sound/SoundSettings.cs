using System;
using UnityEngine;

namespace Sound
{
    [Serializable]
    public class SoundSettings
    {
        [field: SerializeField] public bool isLoop { get; private set; }
        [field: SerializeField, Range(0, 1)] public float volume { get; private set; } = 1;
    }
}