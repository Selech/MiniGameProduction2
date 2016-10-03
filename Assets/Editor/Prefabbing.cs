//C# Example
using UnityEditor;
using UnityEngine;

using System.Linq;

public class Prefabbing : EditorWindow
{
    static Vector3 alignmentVector;
    static Quaternion alignmentQuaternion;

    // Add a new menu item with hotkey CTRL-SHIFT-A
    [MenuItem("Tools/Prefab stuff %#T")]
    private static void PrefabObjects()
    {
        PrefabStuff();
    }

    static void PrefabStuff()
    {
        var objects = Selection.gameObjects;

        foreach(var obj in objects)
        {
            var finalName = obj.name.Remove(0, 10);

            var displayers = obj.GetComponentsInChildren<Transform>().Where(t => t.gameObject.name == "DISPLAYER").ToList();
            foreach (var l in displayers)
            {
                l.gameObject.SetActive(false);
            }

            PrefabUtility.CreatePrefab("Assets/Resources/GeneratedChunks/" + finalName +".prefab", obj);

            foreach (var l in displayers)
            {
                l.gameObject.SetActive(true);
            }
        }

    }

}