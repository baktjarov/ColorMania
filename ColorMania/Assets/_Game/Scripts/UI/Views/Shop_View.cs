using UnityEngine;
using UnityEngine.UI;
using UIElements;
using Services;
using SO;
using Zenject;

namespace UI.Views
{
    public class Shop_View : ViewBase
    {
        [Header("UI")]
        [SerializeField] private Transform _contentHolder;

        [Header("Buttons")]
        [SerializeField] private Button _backButton;

        [Inject] private ListOfAllPens _listOffAllPens;
        [Inject] private PenShopUnit _penShopUnityPrefab;

        protected override void Awake()
        {
            base.Awake();

            InjectService.Inject(this);
        }

        public override void Open()
        {
            base.Open();

            _backButton.onClick.AddListener(Close);
            
            SpawnPenShopUnits();
        }

        public override void Close()
        {
            base.Close();

            _backButton.onClick.RemoveListener(Close);
        }

        private void SpawnPenShopUnits()
        {
            foreach (var pen in _contentHolder.GetComponentsInChildren<PenShopUnit>(true))
            {
                Destroy(pen.gameObject);
            }

            foreach (var penDTO in _listOffAllPens.pens)
            {
                var penShopUnit = Instantiate(_penShopUnityPrefab, parent: _contentHolder);
                penShopUnit.SetPenDTO(penDTO);
            }
        }
    }
}