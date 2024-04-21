using Interfaces;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Services
{
    public class BannerShower : MonoBehaviour
    {
        [Inject] private IAdsShower _adsShower;

        private void OnEnable()
        {
            StartCoroutine(TryShowBanner());
        }

        private IEnumerator TryShowBanner()
        {
            yield return new WaitForSecondsRealtime(2);
            _adsShower.ShowBanner();
        }
    }
}