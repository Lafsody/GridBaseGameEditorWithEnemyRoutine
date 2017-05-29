using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

enum EditOption {Null, Add, Delete, Select}

public class GridEditorWindow : EditorWindow
{
	private static bool isEnabled;
	private static EditOption selected;

	public static void OpenWindow()
	{
		var window = GetWindow(typeof(GridEditorWindow)) as GridEditorWindow;
		window.minSize = new Vector2(300, 400);
		window.Show();
	}

	void OnEnable()
	{
		isEnabled = true;
		Editor.CreateInstance(typeof(SceneViewEventHandler));
	}

	void OnDestroy()
	{
		isEnabled = false;
	}

	void OnGUI()
	{
		
	}

	public class SceneViewEventHandler : Editor
	{
		static SceneViewEventHandler()
		{
			SceneView.onSceneGUIDelegate += OnSceneGUI;
		}

		static void OnSceneGUI(SceneView aView)
		{
			if (!isEnabled)
				return;

			Event hotkey_e = Event.current;
			switch(hotkey_e.type)
			{
				case EventType.KeyDown:
					if (hotkey_e.shift)
					{
						switch(hotkey_e.keyCode)
						{
							case KeyCode.A:
								CreateGrid();
								break;
						}
					}
					break;
			}
		}
	}

	static void CreateGrid()
	{
		Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/SceneObjects/Floor.prefab", typeof(GameObject));
		GameObject clone = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
		
	}
}
