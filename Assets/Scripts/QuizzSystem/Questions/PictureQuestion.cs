using System.Collections.Generic;
using UnityEngine;

namespace QuizzSystem.Questions
{
    [CreateAssetMenu(menuName = "Questions/Picture")]
    public class PictureQuestion : Question
    {
        [SerializeField] private Sprite picture;

        public Sprite Picture => picture;
    }
}