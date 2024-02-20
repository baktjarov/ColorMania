using DataClasses;
using Helpers;
using Interfaces;
using System;
using UnityEngine;

namespace Services
{
    public class PenSaveService : IPenSaveService, IPenSelecter
    {
        [SerializeField] private PenSaveDTO _savedPens;

        private void ValidatePen(string ID)
        {
            PenDTO pen = _savedPens.savedPens.Find((x) => x.penID == ID);

            if (pen == null)
            {
                pen = new PenDTO(ID);
                _savedPens.savedPens.Add(pen);
            }
        }

        private void Validate()
        {
            if (_savedPens == null)
            {
                _savedPens = SaveHelper.GetStoredDataClass<PenSaveDTO>
                            (PenSaveDTO.folderPath, PenSaveDTO.filePath);
            }

            if (_savedPens == null)
            {
                _savedPens = new PenSaveDTO();
            }
        }

        public PenDTO GetSavedPen(string ID)
        {
            Validate();
            ValidatePen(ID);

            PenDTO savedPen = _savedPens.savedPens.Find((x) => x.penID == ID);

            return savedPen;
        }

        public void SavePen(PenDTO pen)
        {
            PenDTO savedPen = GetSavedPen(pen.penID);
            savedPen.isUnlocked = pen.isUnlocked;

            SaveHelper.SaveToJson<PenSaveDTO>(_savedPens, PenSaveDTO.folderPath, PenSaveDTO.filePath);
        }

        public PenDTO GetSelectedPen()
        {
            return GetSavedPen(_savedPens.currentSelectedPen);
        }

        public void Select(PenDTO pen, Action onSelected)
        {
            _savedPens.currentSelectedPen = pen.penID;
            SaveHelper.SaveToJson<PenSaveDTO>(_savedPens, PenSaveDTO.folderPath, PenSaveDTO.filePath);

            onSelected?.Invoke();
        }

        public bool IsSelected(Pen_Data penDTO)
        {
            if (string.IsNullOrEmpty(_savedPens.currentSelectedPen))
            {
                if (penDTO.isDefault == true)
                {
                    _savedPens.currentSelectedPen = penDTO.penID;
                }
            }

            return _savedPens.currentSelectedPen == penDTO.penID;
        }
    }
}