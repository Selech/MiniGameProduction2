﻿using System;
using System.IO;
using UnityEditor;
using UnityEngine;

class BuildScript
{
    [MenuItem("Build/Android")]
    static void PerformBuild()
    {
        string[] scenes = { "Assets/_Project/_Scenes/IntroCutscene.unity", "Assets/_Project/_Scenes/Gameplay.unity", "Assets/_Project/_Scenes/WinCutscene.unity" };

        string buildPath = "../Build/Android/";
        string fileName = DateTime.Now.ToString("yyyy-MM-dd_HH:mm") + "_build.apk";
        // Create build folder if not yet exists
        var directory = Directory.CreateDirectory(buildPath);

        string path = directory.FullName + fileName;

        BuildPipeline.BuildPlayer(scenes, buildPath+fileName, BuildTarget.Android, BuildOptions.Development);
    }
}