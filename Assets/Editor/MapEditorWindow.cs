using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MapEditorWindow : EditorWindow
{
	[MenuItem("GameEditor/MapEditor")]
	private static void Init()
	{
		var window = (MapEditorWindow)GetWindow(typeof(MapEditorWindow));
		window.minSize = new Vector2(300, 400);
		window.Show();
	}

	void OnEnable()
	{
		
	}
}
