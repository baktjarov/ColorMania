using DataClasses;
using Interfaces;
using SO;
using System;

namespace Services
{
    public class PenUnlocker_Ad : IPenUnlocker
    {
        private IAdsShower _adsShower;
        private IPenSaveService _penSaveService;

        public PenUnlocker_Ad(IAdsShower adsShower, IPenSaveService penSaveService)
        {
            _adsShower = adsShower;
            _penSaveService = penSaveService;
        }

        public void Unlock(PenDTO pen, Action onUnlocked)
        {
            _adsShower.ShowRewarded((rewarded) =>
            {
                OnRewarded(pen, onUnlocked);
            });
        }

        private void OnRewarded(PenDTO pen, Action onUnlocked)
        {
            pen.isUnlocked = true;
            _penSaveService.SavePen(pen);

            onUnlocked?.Invoke();
        }
    }
}