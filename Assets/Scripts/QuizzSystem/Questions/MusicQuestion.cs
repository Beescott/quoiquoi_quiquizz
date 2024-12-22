using UnityEngine;

namespace QuizzSystem.Questions
{
    [CreateAssetMenu(menuName = "Questions/Music")]
    public class MusicQuestion : Question
    {
        [SerializeField] private AudioClip audioClip;
        
        public AudioClip AudioClip => audioClip;
    }
}