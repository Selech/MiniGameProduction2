using System.IO;
using UnityEditor;
class BuildScript
{
    static void PerformBuild()
    {
        string[] scenes = { "Assets/_Project/_Scenes/IntroCutscene.unity", "Assets/_Project/_Scenes/Gameplay.unity", "Assets/_Project/_Scenes/WinCutscene.unity" };

        string buildPath = "../../../Build/Android";

        // Create build folder if not yet exists
        Directory.CreateDirectory(buildPath);

        BuildPipeline.BuildPlayer(scenes, buildPath, BuildTarget.Android, BuildOptions.Development);
    }
}