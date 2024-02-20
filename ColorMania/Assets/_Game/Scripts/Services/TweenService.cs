using DG.Tweening;
using UnityEngine;

namespace Services
{
    public class TweenService
    {
        public static void TweenPositionBack(Transform obj, Vector3 targetPositiion, float duartion)
        {
            Vector3 originalPosition = obj.transform.position;
            obj.transform.position = originalPosition + targetPositiion;
            obj.transform.DOMove(originalPosition, duartion);
        }
    }
}