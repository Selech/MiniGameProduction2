//C# Example
using UnityEditor;
using UnityEngine;

using System.Linq;

public class AlignmentTool : EditorWindow
{
	static Vector3 alignmentVector;
	static Quaternion alignmentQuaternion;

	// Add menu item named "My Window" to the Window menu
	[MenuItem("Window/AlignmentTool")]
	public static void ShowWindow()
	{
		//Show existing window instance. If one doesn't exist, make one.
		EditorWindow.GetWindow(typeof(AlignmentTool));
	}

	// Add a new menu item with hotkey CTRL-SHIFT-A
	[MenuItem("Tools/Save Alignment #F1")]
	private static void SaveAlignment()
	{
		SaveStuff ();
	}

	// Add a new menu item with hotkey CTRL-
	[MenuItem("Tools/Item #F2")]
	private static void UseAlignment()
	{
		DoStuff ();
	}

	void OnGUI()
	{
		GUILayout.Label ("Alignment Settings", EditorStyles.boldLabel);
		EditorGUILayout.Vector3Field ("Position", alignmentVector);
		EditorGUILayout.Vector3Field ("Rotation", alignmentQuaternion.eulerAngles);

		if(GUILayout.Button("Save"))
			SaveStuff();
		if(GUILayout.Button("Apply"))
			DoStuff();
	}

	static void SaveStuff(){
		Transform endPoint = Selection.activeGameObject.GetComponentsInChildren<Transform> ().Where(t => t.gameObject.name == "EndPoint").FirstOrDefault();

		alignmentVector = endPoint.position;
		alignmentQuaternion = endPoint.rotation;
	}

	static void DoStuff(){
		Selection.activeGameObject.transform.position = alignmentVector;
		Selection.activeGameObject.transform.rotation = alignmentQuaternion;
	}
}