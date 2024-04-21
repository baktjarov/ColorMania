using Interfaces;
using Services;
using SO;
using Zenject;

namespace GameStates
{
    public class Gameplay_GameState_Model
    {
        [Inject] public IColorPicker colorPicker { get; private set; }
        [Inject] public ListOfAllPens listOfAllPens { get; private set; }
        [Inject] public ListOfAllPictures listOfAllPictures { get; private set; }
        [Inject] public ListOfAllViews listOfAllViews { get; private set; }

        public void Initialize()
        {
            InjectService.Inject(this);
        }
    }
}
