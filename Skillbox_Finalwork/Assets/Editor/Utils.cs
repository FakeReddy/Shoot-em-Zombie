using UnityEngine;
using UnityEditor;
public class Utils
{
    [MenuItem("Utils/Clear prefs")]
    public static void ClearPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.LogWarning("PlayerPrefs cleared!");
        Debug.LogError("PlayerPrefs cleared!");
    }
}
