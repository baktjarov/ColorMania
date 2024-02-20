using UnityEngine;

namespace Gameplay
{
    public class PenSkin : MonoBehaviour
    {
        [SerializeField] private Color _penColor = Color.black;

        public Color penColor
        {
            get { return _penColor; }
            set { _penColor = value; }
        }
    }
}
