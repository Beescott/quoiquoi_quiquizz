using UnityEngine;
using UnityEngine.Video;

namespace QuizzSystem.Questions
{
    [CreateAssetMenu(menuName = "Questions/Video")]
    public class VideoQuestion : Question
    {
        [SerializeField] private string videoClipName;

        public string VideoClip => videoClipName;
    }
}