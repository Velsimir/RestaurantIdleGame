using UnityEditor;
using UnityEngine;

namespace MainGame.Scripts.Editor
{
    public class Tools
    {
        [MenuItem("Tools/Clear prefs")]
        public static void ClearPrefs()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}