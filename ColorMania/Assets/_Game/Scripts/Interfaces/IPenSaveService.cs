using DataClasses;

namespace Interfaces
{
    public interface IPenSaveService
    {
        public PenDTO GetSavedPen(string ID);
        public void SavePen(PenDTO pen);
    }
}