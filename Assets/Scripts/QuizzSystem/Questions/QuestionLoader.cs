using System.Collections.Generic;
using UnityEngine;
using Utility;

#if UNITY_EDITOR
using QuizzSystem.UI;
using UnityEditor;
#endif

namespace QuizzSystem.Questions
{
    public class QuestionLoader : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField] private Transform questionTransform;
        [SerializeField] private QuestionButton questionButton;
        
        [SerializeField] private string path;

        private readonly Dictionary<QuizzCategory, int> _count = new();
        
        [NaughtyAttributes.Button]
        private void SelectDirectoryAndLoadQuestions()
        {
            _count.Clear();
            path = EditorUtility.OpenFolderPanel("Load Questions", "", "");
            string relativePath = path.Substring(path.IndexOf("Assets/"));
            path = relativePath;
            if (string.IsNullOrEmpty(path))
                return;

            List<Question> questions = new();
            string[] guids = AssetDatabase.FindAssets("t:ScriptableObject", new[] {path});
            foreach (string guid in guids)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                Question question = AssetDatabase.LoadAssetAtPath<Question>(assetPath);
                questions.Add(question);

                _count.TryAdd(question.Category, 0);
                _count[question.Category]++;
            }
            
            questions.Shuffle();
            foreach (Question question in questions)
            {
                GameObject questionGameObject = (GameObject)PrefabUtility.InstantiatePrefab(questionButton.gameObject, questionTransform);
                bool found = questionGameObject.TryGetComponent(out QuestionButton qb);
                
                if (found == false)
                    continue;
                qb.AssignQuestion(question);
            }

            foreach (var c in _count)
            {
                Debug.Log($"Imported {c.Value} question on {c.Key}");
            }
        }
#endif
    }
}