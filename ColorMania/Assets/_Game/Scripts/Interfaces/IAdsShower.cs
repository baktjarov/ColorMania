using System;
using GoogleMobileAds.Api;

namespace Interfaces
{
    public interface IAdsShower
    {
        public void ShowBanner();
        public void HideBanner();
        public void ShowInterstitial();
        public void ShowRewarded(Action<Reward> onRewarded);
    }
}