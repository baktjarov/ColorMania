using System;
using DataClasses;
using SO;

namespace Interfaces
{
    public interface IPenUnlocker
    {
        public void Unlock(PenDTO pen, Action onUnlocked);
    }
}