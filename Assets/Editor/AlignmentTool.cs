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
	[MenuItem("Tools/Save Chunk Alignment %#X")]
	private static void SaveAlignment()
	{
		SaveStuff ();
	}

    // Add a new menu item with hotkey CTRL-SHIFT-A
    [MenuItem("Tools/Align entire road %#X")]
    private static void AlignRoad()
    {
        AlignFullRoad();
    }

    // Add a new menu item with hotkey CTRL-
    [MenuItem("Tools/Use Chunk Alignment %#C")]
	private static void UseAlignment()
	{
        DoStuff();
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
		Transform startPoint = Selection.activeGameObject.GetComponentsInChildren<Transform> ().Where(t => t.gameObject.name == "StartPoint").FirstOrDefault();

		Selection.activeGameObject.transform.position = alignmentVector-startPoint.localPosition;
		Selection.activeGameObject.transform.rotation = alignmentQuaternion;
	}

    static void AlignFullRoad()
    {
        var selection = Selection.activeGameObject;
        var pieces = selection.GetComponentsInChildren<Transform>().Where(t => t.gameObject.tag == "Pieces").ToList();

        var overallPos = new Vector3();

        pieces[0].transform.position = selection.transform.position;
        pieces[0].transform.rotation = selection.transform.rotation;

        for (int i = 0; i < pieces.Count-1; i++)
        {
            var end = pieces[i].GetComponentsInChildren<Transform>().Where(t => t.gameObject.name == "EndPoint").FirstOrDefault();
            pieces[i + 1].transform.position = end.transform.position;
            pieces[i + 1].transform.rotation = end.transform.rotation;
        }

        // Final alignment
        GameObject startPoint = (GameObject) Instantiate(pieces.FirstOrDefault().GetComponentsInChildren<Transform>().Where(t => t.gameObject.name == "StartPoint").FirstOrDefault().gameObject);
        GameObject globalEnd = pieces.LastOrDefault().GetComponentsInChildren<Transform>().Where(t => t.gameObject.name == "EndPoint").LastOrDefault().gameObject;
        GameObject endPoint = (GameObject)Instantiate(globalEnd);

        overallPos = selection.transform.TransformPoint(globalEnd.transform.position);

        startPoint.transform.SetParent(selection.transform);
        endPoint.transform.SetParent(selection.transform);
        endPoint.transform.position = overallPos;
        endPoint.transform.rotation = globalEnd.transform.rotation;

        var script = selection.GetComponent<ChunkScript>();

        if(script == null)
            script = selection.AddComponent<ChunkScript>();

        script.StartPoint = startPoint;
        script.EndPoint = endPoint;

        var starts = selection.GetComponentsInChildren<Transform>().Where(t => t.gameObject.name == "StartPoint");
        foreach (var s in starts) {
            s.gameObject.SetActive(false);
        }

        var ends = selection.GetComponentsInChildren<Transform>().Where(t => t.gameObject.name == "EndPoint");
        foreach (var e in ends)
        {
            e.gameObject.SetActive(false);
        }
    }
}