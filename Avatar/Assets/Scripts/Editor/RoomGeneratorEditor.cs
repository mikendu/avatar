using System;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RoomGenerator))]
public class RoomGeneratorEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		
		RoomGenerator myScript = (RoomGenerator)target;
		if(GUILayout.Button("Generate Room"))
		{
			myScript.GenerateRoom();
		}
	}
}