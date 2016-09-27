//C# Example
using UnityEditor;
using UnityEngine;

public class AlignmentTool : EditorWindow
{
	string myString = "Hello World";
	bool groupEnabled;
	bool canAlign = false;
	bool canCopy = false;
	float myFloat = 1.23f;

	Vector3 alignmentVector;
	Quaternion alignmentQuaternion;

	GameObject selection;

	// Add menu item named "My Window" to the Window menu
	[MenuItem("Window/AlignmentTool")]
	public static void ShowWindow()
	{
		//Show existing window instance. If one doesn't exist, make one.
		EditorWindow.GetWindow(typeof(AlignmentTool));
	}

	void OnSelectionChange() {
		selection = Selection.activeGameObject;
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

	void SaveStuff(){
		alignmentVector = Selection.activeGameObject.transform.position;
		alignmentQuaternion = Selection.activeGameObject.transform.rotation;
	}

	void DoStuff(){
		Selection.activeGameObject.transform.position = alignmentVector;
		Selection.activeGameObject.transform.rotation = alignmentQuaternion;
	}
}