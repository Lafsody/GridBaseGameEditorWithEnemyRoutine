using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GridEditorWindow : EditorWindow
{
	public static void OpenWindow()
	{
		var window = GetWindow(typeof(GridEditorWindow)) as GridEditorWindow;
		window.minSize = new Vector2(300, 400);
		window.Show();
	}

	void OnEnable()
	{
		
	}
}
