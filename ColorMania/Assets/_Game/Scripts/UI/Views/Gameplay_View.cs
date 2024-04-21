using Gameplay;
using Interfaces;
using UI.UIElements;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class Gameplay_View : ViewBase
    {
        [Header("Buttons")]
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Slider _progressSlider;

        [Header("Color")]
        [SerializeField] private Transform _colorPickersParent;
        [SerializeField] private ColorPickerUnit _colorPickersUnitPrefab;

        private Drawable _drawable;
        private Pause_View _pauseView;
        private Win_View _winView;
        private IColorPicker _colorPicker;

        private float _percentage;

        public void Construct(Pause_View pauseView, Win_View winView, Drawable drawable, IColorPicker colorPicker)
        {
            _pauseView = pauseView;
            _winView = winView;
            _drawable = drawable;
            _colorPicker = colorPicker;
        }

        private void OnEnable()
        {
            if (_progressSlider == null)
            {
                _progressSlider = GetComponentInChildren<Slider>(true);
            }

            _progressSlider.maxValue = 100;

            _colorPicker.onInitlialized += InitliazeColorPickerUnits;
        }

        private void OnDisable()
        {
            if (_colorPicker != null) { _colorPicker.onInitlialized -= InitliazeColorPickerUnits; }
        }

        private void Update()
        {
            _percentage = _drawable.percentageOfColoring.value;
            _progressSlider.value = _percentage;

            if (_percentage >= 90)
            {
                _winView?.Open();
            }
        }

        public override void Open()
        {
            base.Open();

            _pauseButton.onClick.AddListener(OpenPause);
        }

        public override void Close()
        {
            base.Close();

            _pauseButton.onClick.RemoveListener(OpenPause);
        }

        private void OpenPause()
        {
            _pauseView?.Open();
        }

        public void SetProgress(float progress)
        {
            _progressSlider.value = progress;
        }

        private void InitliazeColorPickerUnits()
        {
            ColorPickerUnit colorPickerUnit = null;

            foreach (var existingColorPickerUnit in _colorPickersParent.GetComponentsInChildren<ColorPickerUnit>(true))
            {
                Destroy(existingColorPickerUnit.gameObject);
            }

            colorPickerUnit = Instantiate(_colorPickersUnitPrefab, _colorPickersParent);
            colorPickerUnit.Initlialize(IColorPicker.noColor, true);

            foreach (var color in _colorPicker.colors)
            {
                colorPickerUnit = Instantiate(_colorPickersUnitPrefab, _colorPickersParent);
                colorPickerUnit.Initlialize(color);
            }
        }
    }
}