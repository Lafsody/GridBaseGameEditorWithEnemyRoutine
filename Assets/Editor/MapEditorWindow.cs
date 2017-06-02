using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MapEditorWindow : EditorWindow
{
	Color headerSectionColor = new Color(40/255f, 40/255f, 100/255f, 1);

	GUISkin skin;

	Rect headerSection;
	Rect visibilityControlSection;
	Rect objectEditorSection;

	string ModeText;
	static readonly string defaultModeText = "Editor Mode";
	static readonly string prefixModeText = "Mode : ";
	static readonly string gridEditModeText = prefixModeText + "Grid Edit";
	static readonly string playerEditModeText = prefixModeText + "Player Edit";

	[MenuItem("GameEditor/MapEditor")]
	private static void Init()
	{
		var window = (MapEditorWindow)GetWindow(typeof(MapEditorWindow));
		window.minSize = new Vector2(300, 400);
		window.Show();
	}

	void OnEnable()
	{
		InitTexture();
		skin = Resources.Load<GUISkin>("GUIStyle/MapEditorSkin");
		SceneView.onSceneGUIDelegate += OnSceneCurrentModeText;
	}

	void OnDisable()
	{
		SceneView.onSceneGUIDelegate -= OnSceneCurrentModeText;
	}

	void InitTexture()
	{
		// headerTexture = new Texture2D(1, 1);
		// headerTexture.SetPixel(0, 0, headerSectionColor);
		// headerTexture.Apply();
	}

	void OnGUI()
	{
		DrawLayout();
		DrawHeader();
		DrawVisiblitySetting();
		DrawObjectEditor();
	}

	private void DrawLayout()
	{
		headerSection.x = 0;
		headerSection.y = 0;
		headerSection.width = Screen.width;
		headerSection.height = 50;

		visibilityControlSection.x = 0;
		visibilityControlSection.y = headerSection.height;
		visibilityControlSection.width = Screen.width / 2;
		visibilityControlSection.height = Screen.height - headerSection.height;
		
		objectEditorSection.x = visibilityControlSection.width;
		objectEditorSection.y = headerSection.height;
		objectEditorSection.width = Screen.width - visibilityControlSection.width;
		objectEditorSection.height = Screen.height - headerSection.height;
	}

	void DrawHeader()
	{
		GUILayout.BeginArea(headerSection, skin.GetStyle("Header1"));
		GUILayout.Label("Map Editor", skin.GetStyle("Header1"));
		GUILayout.EndArea();
	}

	void DrawVisiblitySetting()
	{
		GUILayout.BeginArea(visibilityControlSection, skin.GetStyle("VisibilitySetting"));

		GUILayout.EndArea();
	}

	void DrawObjectEditor()
	{
		GUILayout.BeginArea(objectEditorSection, skin.GetStyle("ObjectEditor"));

		if (GUILayout.Button("Grid Editor", skin.GetStyle("button")))
		{
			GridEditorWindow.OpenWindow();
		}

		GUILayout.EndArea();
	}

	void OnSceneCurrentModeText(SceneView sceneview)
	{
		Handles.BeginGUI();
		GUILayout.BeginArea( new Rect( 10, Screen.height - 90, Screen.width - 20, 50 ) );

		ModeText = defaultModeText;
		if (GridEditorWindow.isEnabled)
			ModeText = gridEditModeText;

		GUILayout.Label(ModeText, skin.GetStyle("OnScreenText"));

		GUILayout.EndArea();
		Handles.EndGUI();
	}

	public static GameObject GetContainer()
	{
		var container = GameObject.Find("Container");
		if (container == null)
		{
			var go = new GameObject();
			go.name = "Container";
			go.transform.position = Vector3.zero;
			container = go;

			Undo.RegisterCreatedObjectUndo(go, "Container");
		}
		return container;
	}
}
