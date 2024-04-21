using GoogleMobileAds.Api;
using Interfaces;
using System;
using UnityEngine;

namespace Services
{
    public class AdsShower_AdMob : IAdsShower
    {
        private string _bannerID = "ca-app-pub-3940256099942544/6300978111";
        private string _interstitialID = "ca-app-pub-3940256099942544/1033173712";
        private string _rewardedID = "ca-app-pub-3940256099942544/5224354917";

        private BannerView _bannerView;
        private InterstitialAd _interstitialAd;
        private RewardedAd _rewardedAd;

        public void Initialize()
        {
            LoadBanner();
            LoadInterstitial();
            LoadRewarded();
        }

        private void LoadBanner()
        {
            _bannerView = new BannerView(_bannerID, AdSize.Banner, AdPosition.Bottom);
            _bannerView.LoadAd(new AdRequest());
        }

        private void LoadInterstitial()
        {
            InterstitialAd.Load(_interstitialID, new AdRequest(), OnLoaded);

            void OnLoaded(InterstitialAd interstitialAd, LoadAdError loadAdError)
            {
                if (loadAdError != null) { Debug.LogError(loadAdError.GetMessage()); }
                _interstitialAd = interstitialAd;
            }
        }

        private void LoadRewarded()
        {
            RewardedAd.Load(_rewardedID, new AdRequest(), OnLoaded);

            void OnLoaded(RewardedAd rewardedAd, LoadAdError loadAdError)
            {
                if (loadAdError != null) { Debug.LogError(loadAdError.GetMessage()); }
                _rewardedAd = rewardedAd;
            }
        }

        public void ShowBanner()
        {
            _bannerView?.Show();
        }

        public void HideBanner()
        {
            _bannerView?.Hide();
        }

        public void ShowInterstitial()
        {
            if (_interstitialAd != null && _interstitialAd.CanShowAd() == true)
            {
                _interstitialAd.Show();
            }
        }

        public void ShowRewarded(Action onRewarded)
        {
            if (_rewardedAd != null && _rewardedAd.CanShowAd() == true)
            {
                _rewardedAd.Show((rewarded) =>
                {
                    onRewarded?.Invoke();
                    LoadRewarded();
                });
            }
        }
    }
}