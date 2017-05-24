using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MapEditorWindow : EditorWindow
{
	Texture2D headerTexture;
	Texture2D visibilityControlTexture;
	Texture2D objectEditorTexture;

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
	}

	void InitTexture()
	{
		headerTexture = Resources.Load<Texture2D>("bg/bg1");
		visibilityControlTexture = Resources.Load<Texture2D>("bg/bg2");
		objectEditorTexture = Resources.Load<Texture2D>("bg/bg3");
	}

	void OnGUI()
	{
		DrawLayout();
		DrawHeader();
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
		GUI.DrawTexture(visibilityControlSection, visibilityControlTexture);
		GUI.DrawTexture(objectEditorSection, objectEditorTexture);
	}

	void DrawHeader()
	{
		GUILayout.BeginArea(headerSection);
		GUILayout.Label("Map Editor");
		GUILayout.EndArea();
	}
}
