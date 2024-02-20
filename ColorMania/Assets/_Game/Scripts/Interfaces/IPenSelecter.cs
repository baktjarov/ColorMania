using DataClasses;
using System;

namespace Interfaces
{
    public interface IPenSelecter
    {
        public bool IsSelected(Pen_Data penDTO);
        public PenDTO GetSelectedPen();
        public void Select(PenDTO pen, Action onSelected);
    }
}