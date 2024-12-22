using UnityEngine;

namespace Utility
{
    public static class ColorUtility
    {
        public static void SaveInPlayerPrefs(string key, Color color)
        {
            string rKey = $"{key}_r";
            PlayerPrefs.SetFloat(rKey, color.r);
            
            string gKey = $"{key}_g";
            PlayerPrefs.SetFloat(gKey, color.g);
            
            string bKey = $"{key}_b";
            PlayerPrefs.SetFloat(bKey, color.b);
            
            string aKey = $"{key}_a";
            PlayerPrefs.SetFloat(aKey, color.a);
        }

        public static Color GetFromPlayerPrefs(string key)
        {
            string rKey = $"{key}_r";
            float r = PlayerPrefs.GetFloat(rKey);
            
            string gKey = $"{key}_g";
            float g = PlayerPrefs.GetFloat(gKey);
            
            string bKey = $"{key}_b";
            float b = PlayerPrefs.GetFloat(bKey);
            
            string aKey = $"{key}_a";
            float a = PlayerPrefs.GetFloat(aKey);

            return new Color(r, g, b, a);
        }
    }
}