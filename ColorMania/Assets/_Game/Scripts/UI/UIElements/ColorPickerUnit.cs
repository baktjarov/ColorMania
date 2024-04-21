using Interfaces;
using Services;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace UI.UIElements
{
    public class ColorPickerUnit : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image _colorImage;
        [SerializeField] private Outline _selectStateGameobject;
        [SerializeField] private GameObject _noColorState;

        [Inject] private IColorPicker _colorPicker;

        private void Awake()
        {
            InjectService.Inject(this);
        }

        private void OnEnable()
        {
            _colorPicker.onColorPicked += UpdateSelectState;
        }

        private void OnDisable()
        {
            _colorPicker.onColorPicked -= UpdateSelectState;
        }

        public void Initlialize(Color color, bool initializePicked = false)
        {
            _colorImage.color = color;

            if (initializePicked == true)
            {
                Pick();
            }

            UpdateSelectState(color);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Pick();
        }

        private void Pick()
        {
            _colorPicker.PickColor(_colorImage.color);
        }

        private void UpdateSelectState(Color color)
        {
            _selectStateGameobject.enabled = _colorImage.color == _colorPicker.currentColor;
            _noColorState?.SetActive(_colorImage.color == IColorPicker.noColor);
        }
    }
}
