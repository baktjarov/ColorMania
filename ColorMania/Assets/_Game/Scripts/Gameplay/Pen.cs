using DataClasses;
using Interfaces;
using Services;
using SO;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    [RequireComponent(typeof(Rigidbody))]
    public class Pen : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Camera _camera_;
        [SerializeField] private Drawable _drawable;


        [Header("Debug")]
        [SerializeField] private Vector3 _onStartMoveOffset;
        [SerializeField] private Pen_Data _penDTO;
        [SerializeField] private PenSkin _penSkin;

        [Inject] private ListOfAllPens _listOfAllPens;
        [Inject] private IPenSelecter _penSelecter;

        public Camera _camera
        {
            get
            {
                if (_camera_ == null) { _camera_ = FindObjectOfType<Camera>(true); }
                return _camera_;
            }
        }

        private void Awake()
        {
            InjectService.Inject(this);

            if (_rigidbody == null) { _rigidbody = GetComponent<Rigidbody>(); }
            if (_drawable == null) { _drawable = GetComponent<Drawable>(); }

            SpawnSkin();
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _onStartMoveOffset = transform.position - _camera.ScreenToWorldPoint(Input.mousePosition);
            }

            if (Input.GetMouseButton(0) == true)
            {
                Vector3 newPosition = _camera.ScreenToWorldPoint(Input.mousePosition) + _onStartMoveOffset;
                newPosition.z = 0;

                _rigidbody.MovePosition(newPosition);
            }
        }

        private void SpawnSkin()
        {
            PenSkin penSkinsPrefab = _listOfAllPens.GetPen(_penSelecter.GetSelectedPen().penID).targetPen;
            _penSkin = UnityEngine.Object.Instantiate(penSkinsPrefab);
            _penSkin.transform.SetParent(transform);
        }
    }
}