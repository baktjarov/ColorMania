namespace Interfaces
{
    public interface ILevelSaveService
    {
        public int GetCurrentLevel();
        public void SetLevel(int level);
    }
}