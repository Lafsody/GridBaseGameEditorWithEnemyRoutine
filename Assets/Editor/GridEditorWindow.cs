using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

enum EditOption {Null, Add, Delete, Select}

public class GridEditorWindow : EditorWindow
{
	private static bool isEnabled;
	private static EditOption selected;

	Rect headerSection;
	Rect bodySection;
	Rect modeSection;
	Rect infoSection;

	GUISkin skin;

	public static void OpenWindow()
	{
		var window = GetWindow(typeof(GridEditorWindow)) as GridEditorWindow;
		window.minSize = new Vector2(400, 400);
		window.Show();
	}

	void OnEnable()
	{
		isEnabled = true;
		skin = Resources.Load<GUISkin>("GUIStyle/ObjectsEditorSkin");
		Editor.CreateInstance(typeof(SceneViewEventHandler));
	}

	void OnDestroy()
	{
		isEnabled = false;
	}

	void OnGUI()
	{
		DrawLayout();
		DrawHeader();
		DrawBody();
	}

	void DrawLayout()
	{
		headerSection.x = 0;
		headerSection.y = 0;
		headerSection.width = Screen.width;
		headerSection.height = 50;

		bodySection.x = 0;
		bodySection.y =  headerSection.height;
		bodySection.width = Screen.width;
		bodySection.height = Screen.height - headerSection.height;
	}

	void DrawHeader()
	{
		GUILayout.BeginArea(headerSection, skin.GetStyle("Header"));
		GUILayout.Label("Grid Editor", skin.GetStyle("Header"));
		GUILayout.EndArea();
	}

	void DrawBody()
	{
		GUILayout.BeginArea(bodySection, skin.GetStyle("Body"));
		GUILayout.EndArea();
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
