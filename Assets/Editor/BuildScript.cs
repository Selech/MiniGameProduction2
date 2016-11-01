using System;
using System.IO;
using UnityEditor;
using UnityEngine;

class BuildScript
{
    static void PerformBuild()
    {
        string[] scenes = { "Assets/_Project/_Scenes/IntroCutscene.unity", "Assets/_Project/_Scenes/Gameplay.unity", "Assets/_Project/_Scenes/WinCutscene.unity" };

        string buildPath = "../Build/Android/";

        // Create build folder if not yet exists
        Directory.CreateDirectory(buildPath);

        string fileName = DateTime.Now.ToString("yyyy-MM-ddTHH:mm") + "_build.apk";

        BuildPipeline.BuildPlayer(scenes, buildPath+fileName, BuildTarget.Android, BuildOptions.Development);
    }
}