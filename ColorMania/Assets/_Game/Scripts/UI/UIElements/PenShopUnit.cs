using DataClasses;
using Interfaces;
using Services;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UIElements
{
    public class PenShopUnit : MonoBehaviour
    {
        private static Action<PenDTO> _onPenSelected;

        [Header("States")]
        [SerializeField] private GameObject _unlockState;
        [SerializeField] private GameObject _lockState;
        [SerializeField] private GameObject _selectedState;

        [Header("UI")]
        [SerializeField] private Image _icon;
        [SerializeField] private Button _button;

        [Header("Debug")]
        [SerializeField] private Pen_Data _penData;

        [Inject] private IPenUnlocker _penUnlocker;
        [Inject] private IPenSelecter _penSelecter;

        private void Awake()
        {
            InjectService.Inject(this);
        }

        public void SetPenDTO(Pen_Data penDTO)
        {
            _penData = penDTO;
            Reinitialize();
        }

        private void Reinitialize()
        {
            UnsubscribeFromEvents();
            SubscribeToEvents();
            UpdateUI();
        }

        private void SubscribeToEvents()
        {
            if (_penData == null) { return; }

            _button.onClick.AddListener(_penData.IsAvaiable() ? ChoosePen : UnlockPen);
            _onPenSelected += OnPenSelected;
        }

        private void UnsubscribeFromEvents()
        {
            if (_penData == null) { return; }

            _button.onClick.RemoveListener(ChoosePen);
            _button.onClick.RemoveListener(UnlockPen);
            _onPenSelected -= OnPenSelected;
        }

        private void UpdateUI()
        {
            if (_penData == null) { return; }

            _icon.sprite = _penData.penIcon;

            _unlockState.SetActive(false);
            _lockState.SetActive(false);
            _selectedState.SetActive(false);

            if (_penData.IsAvaiable())
            {
                _unlockState.SetActive(true);
            }
            else
            {
                _lockState.SetActive(true);
            }

            if (_penSelecter.IsSelected(_penData))
            {
                _selectedState.SetActive(true);
            }
            else
            {
                _selectedState.SetActive(false);
            }
        }

        private void ChoosePen()
        {
            if (_penData == null) { return; }

            _penSelecter.Select(_penData.penDTO, () =>
            {
                _onPenSelected?.Invoke(_penData.penDTO);
            });
        }

        private void UnlockPen()
        {
            if (_penData == null) { return; }

            _penUnlocker.Unlock(_penData.penDTO, Reinitialize);
        }

        private void OnPenSelected(PenDTO penDTO)
        {
            Reinitialize();
        }
    }
}