using Gameplay;
using System.Collections.Generic;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = nameof(ListOfAllPictures),
                    menuName = "Scriptables/" + nameof(ListOfAllPictures))]
    public class ListOfAllPictures : ScriptableObject
    {
        [SerializeField] private List<Picture> _pictures = new();

        public int picturesCount => _pictures.Count;

        public Picture GetPicture(int index)
        {
            if (index >= _pictures.Count)
            {
                index = 0;
            }

            if (index < 0)
            {
                index = 0;
            }

            return _pictures[index];
        }
    }
}