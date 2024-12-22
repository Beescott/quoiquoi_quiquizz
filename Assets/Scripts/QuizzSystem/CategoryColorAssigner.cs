using System;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using ColorUtility = Utility.ColorUtility;

namespace QuizzSystem
{
    [AssetPathAttribute("Questions/Category Color Assigner")]
    public class CategoryColorAssigner : ScriptableSingleton<CategoryColorAssigner>
    {
        [SerializeField] private List<CategoryColor> categoryColors;

        public List<CategoryColor> CategoryColors => categoryColors;
        
        public bool TryGetColorFromCategory(QuizzCategory category, out Color color)
        {
            bool foundCategory = categoryColors.TryGet(c => c.category == category, out CategoryColor element);
            if (foundCategory == false)
            {
                color = default;
                return false;
            }

            color = element.color;
            return true;
        }
        
        private void OnValidate()
        {
            foreach (QuizzCategory category in Enum.GetValues(typeof(QuizzCategory)))
            {
                bool exists = categoryColors.TryGet(c => c.category == category, out CategoryColor element);
                if (exists)
                    continue;

                Color color = PlayerPrefs.HasKey(category.ToString())
                    ? ColorUtility.GetFromPlayerPrefs(category.ToString())
                    : UnityEngine.Random.ColorHSV();
                
                categoryColors.Add(new CategoryColor()
                {
                    category = category,
                    color = color
                });
            }
            
            foreach (CategoryColor categoryColor in categoryColors)
            {
                ColorUtility.SaveInPlayerPrefs(categoryColor.category.ToString(), categoryColor.color);
            }
        }
        
        [Serializable]
        public struct CategoryColor
        {
            public QuizzCategory category;
            public Color color;
        }
    }
}