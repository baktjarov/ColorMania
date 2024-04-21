using Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class ColorPickService : MonoBehaviour, IColorPicker
    {
        public Action onInitlialized { get; set; }
        public Action<Color> onColorPicked { get; set; }

        public Color currentColor { get; private set; }

        public IEnumerable<Color> colors { get; private set; }

        public void PickColor(Color color)
        {
            currentColor = color;

            onColorPicked?.Invoke(currentColor);
        }

        public void Initialize(IEnumerable<Color> newColors)
        {
            colors = newColors;
            onInitlialized?.Invoke();
        }
    }
}