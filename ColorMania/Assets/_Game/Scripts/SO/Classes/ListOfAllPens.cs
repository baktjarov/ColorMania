using DataClasses;
using Interfaces;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = nameof(ListOfAllPens),
                    menuName = "Scriptables/" + nameof(ListOfAllPens))]
    public class ListOfAllPens : ScriptableObject
    {
        public List<Pen_Data> pens = new();

        public void Initialize(IPenSaveService penSaveService)
        {
            foreach (var pen in pens)
            {
                PenDTO savedPen = penSaveService.GetSavedPen(pen.penID);
                pen.SetDTO(savedPen);
            }
        }

        public Pen_Data GetPen(string id)
        {
            Pen_Data selectedPen = pens.FirstOrDefault((pen) => pen.penID == id);
            return selectedPen;
        }
    }
}