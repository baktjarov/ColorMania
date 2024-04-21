using System;
using System.Collections.Generic;
using UnityEngine;

namespace Interfaces
{
    public interface IColorPicker
    {
        public static readonly Color noColor = Color.white;

        public Action onInitlialized { get; set; }
        public Action<Color> onColorPicked { get; set; }

        public Color currentColor { get; }

        public IEnumerable<Color> colors { get; }

        public void PickColor(Color color);
        public void Initialize(IEnumerable<Color> newColors);
    }
}
