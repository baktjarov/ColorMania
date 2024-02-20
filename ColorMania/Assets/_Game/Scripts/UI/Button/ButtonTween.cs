using DG.Tweening;
using Services;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class ButtonTween : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Button _button;

        [Header("Settings")]
        [SerializeField] private float _vectorX;
        [SerializeField] private float _vectorY;
        [SerializeField] private float _animationTime;
        private Vector3 _originalButtonPosition;

        private void Awake()
        {
            _originalButtonPosition = _button.transform.position;
        }

        private void OnEnable()
        {
            _button.transform.position = _originalButtonPosition;
            TweenService.TweenPositionBack(_button.transform, new Vector3(_vectorX, _vectorY, 0), _animationTime);
        }

        private void OnDisable()
        {
            _button.DOKill();
        }
    }
}