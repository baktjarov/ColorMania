using Gameplay;
using System.Collections.Generic;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = nameof(ListOfAllPictures),
                    menuName = "Scriptables/" + nameof(ListOfAllPictures))]
    public class ListOfAllPictures : ScriptableObject
    {
        [SerializeField] private List<Picture> pictures = new();

        public Picture GetPicture(int index)
        {
            if (index >= pictures.Count)
            {
                index = 0;
            }

            if (index < 0)
            {
                index = 0;
            }

            return pictures[index];
        }
    }
}