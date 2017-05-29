using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
using UnityEditor.AnimatedValues;

public class EnemyDesignerWindow : EditorWindow
{
	Texture2D headerSectionTexture;
	Texture2D sectionOneTexture;
	Texture2D sectionTwoTexture;
	Texture2D sectionThreeTexture;

 	Color headerSectionColor = new Color(13f/255f, 32f/255f, 144f/255f, 1);

	Rect headerSection;
	Rect firstSection;
	Rect secondSection;
	Rect thirdSection;

	[MenuItem("Window/Enemy Designer")]
	static void OpenWindow()
	{
		EnemyDesignerWindow window = (EnemyDesignerWindow)GetWindow(typeof(EnemyDesignerWindow));
		window.minSize = new Vector2(600, 300);
		window.Show();
	}

	void OnEnable()
	{
		InitTextures();
	}

	void InitTextures()
	{
		headerSectionTexture = new Texture2D(1, 1);
		headerSectionTexture.SetPixel(0, 0, headerSectionColor);
		headerSectionTexture.Apply();

		sectionOneTexture = Resources.Load<Texture2D>("bg/bg1");
		sectionTwoTexture = Resources.Load<Texture2D>("bg/bg2");
		sectionThreeTexture = Resources.Load<Texture2D>("bg/bg3");
	}

	void OnGUI()
	{
		DrawLayouts();
		DrawHeader();
	}

	void DrawLayouts()
	{
		headerSection.x = 0;
		headerSection.y = 0;
		headerSection.width = Screen.width;
		headerSection.height = 50;

		firstSection.x = 0;
		firstSection.y = 50;
		firstSection.width = Screen.width / 3;
		firstSection.height = Screen.height - 50;

		secondSection.x = Screen.width / 3;
		secondSection.y = 50;
		secondSection.width = Screen.width / 3;
		secondSection.height = Screen.height - 50;

		thirdSection.x = Screen.width * 2 / 3;
		thirdSection.y = 50;
		thirdSection.width = Screen.width / 3;
		thirdSection.height = Screen.height - 50;

		GUI.DrawTexture(headerSection, headerSectionTexture);
		GUI.DrawTexture(firstSection, sectionOneTexture);
		GUI.DrawTexture(secondSection, sectionTwoTexture);
		GUI.DrawTexture(thirdSection, sectionThreeTexture);
	}

	void DrawHeader()
	{
		GUILayout.BeginArea(headerSection);
		
		GUILayout.Label("Header Label");

		GUILayout.EndArea();
	}
}

public class MyWindow : EditorWindow
{
    AnimBool m_ShowExtraFields;
    string m_String;
    Color m_Color = Color.white;
    int m_Number = 0;

    [MenuItem("Window/My Window")]
    static void Init()
    {
        MyWindow window = (MyWindow)EditorWindow.GetWindow(typeof(MyWindow));
    }

    void OnEnable()
    {
        m_ShowExtraFields = new AnimBool(true);
        m_ShowExtraFields.valueChanged.AddListener(Repaint);
    }

    void OnGUI()
    {
        m_ShowExtraFields.target = EditorGUILayout.ToggleLeft("Show extra fields", m_ShowExtraFields.target);

        //Extra block that can be toggled on and off.
        if (EditorGUILayout.BeginFadeGroup(m_ShowExtraFields.faded))
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PrefixLabel("Color");
            m_Color = EditorGUILayout.ColorField(m_Color);
            EditorGUILayout.PrefixLabel("Text");
            m_String = EditorGUILayout.TextField(m_String);
            EditorGUILayout.PrefixLabel("Number");
            m_Number = EditorGUILayout.IntSlider(m_Number, 0, 10);
            EditorGUI.indentLevel--;
        }

        EditorGUILayout.EndFadeGroup();
    }
}
