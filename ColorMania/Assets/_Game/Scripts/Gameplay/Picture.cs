using UnityEngine;

namespace Gameplay
{
    public class Picture : MonoBehaviour
    {
        [field: SerializeField] public SpriteRenderer pictureSpriteRenderer { get; private set; }
        [SerializeField] private SpriteRenderer _borderSpriteRenderer;
        [field: SerializeField] public Color[] colors { get; private set; }
    }
}