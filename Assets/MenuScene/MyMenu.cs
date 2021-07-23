using UnityEngine;
using UnityEditor;

public class MyMenu : MonoBehaviour
{
    static string packageFile = "ManyGames.unitypackage";

    [MenuItem("Assets/Export Backup", false, 0)]
    static void action01()
    {
        string[] exportpaths = new string[]
        {
            "Assets/AngryBird",
            "Assets/FlappyBird",
            "Assets/Jumper",
            "Assets/MenuScene",
            "Assets/NCMB",
            "Assets/PublicResources",
            "Assets/workflow",
            "ProjectSettings/TagManager.asset",
            "ProjectSettings/EditorBuildSettings.asset"
        };

        AssetDatabase.ExportPackage(exportpaths, packageFile,
            ExportPackageOptions.Interactive |
            ExportPackageOptions.Recurse |
            ExportPackageOptions.IncludeDependencies);

        print("Backup Export Complete!");
    }

    [MenuItem("Assets/Import Backup", false, 1)]
    static void action02()
    {
        AssetDatabase.ImportPackage(packageFile, true);
    }

}