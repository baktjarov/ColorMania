using DataClasses;
using Gameplay;
using System;
using UnityEngine;

namespace DataClasses
{
    [Serializable]
    public class Pen_Data
    {
        public string penID;
        public PenSkin targetPen;
        public Sprite penIcon;
        public bool isDefault;

        public PenDTO penDTO;

        public void SetDTO(PenDTO dto)
        {
            penDTO = dto;
        }

        public bool IsAvaiable()
        {
            bool isAvaiable = isDefault == true || penDTO.isUnlocked == true;
            return isAvaiable;
        }
    }
}