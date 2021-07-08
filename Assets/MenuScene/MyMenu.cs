using UnityEditor;
using UnityEngine;

public class MyMenu : MonoBehaviour
{
    [MenuItem("My Menu/Reset Ranking")]
    static void ResetRanking()
    {
        PlayerPrefs.DeleteAll();
    }
}
