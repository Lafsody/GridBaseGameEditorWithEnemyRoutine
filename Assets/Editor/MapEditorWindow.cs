using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MapEditorWindow : EditorWindow
{
	Color headerSectionColor = new Color(40/255f, 40/255f, 100/255f, 1);

	Texture2D headerTexture;
	Texture2D visibilityControlTexture;
	Texture2D objectEditorTexture;
	
	GUISkin skin;

	Rect headerSection;
	Rect visibilityControlSection;
	Rect objectEditorSection;

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
	}

	void InitTexture()
	{
		headerTexture = new Texture2D(1, 1);
		headerTexture.SetPixel(0, 0, headerSectionColor);
		headerTexture.Apply();

		visibilityControlTexture = Resources.Load<Texture2D>("bg/bg2");
		objectEditorTexture = Resources.Load<Texture2D>("bg/bg3");	
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

		GUI.DrawTexture(headerSection, headerTexture);
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

		GUILayout.EndArea();
	}
}
