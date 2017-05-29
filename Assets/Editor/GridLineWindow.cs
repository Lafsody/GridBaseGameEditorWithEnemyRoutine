using UnityEditor;
using UnityEngine;
using System.Collections;
using System;

public class GridLineWindow : EditorWindow
{
	static readonly String enableFlagStringPrefs = "isGridLineEnable";
	static readonly String gridSizePrefs = "gridSize";
	static readonly String shiftToMiddlePrefs = "shiftToMiddle";

	static Vector2 gridSize = new Vector2(1, 1);
	static Vector2 maxGrid = new Vector2(100, 100);
	static bool shiftToMiddle = true;

	[MenuItem("GameEditor/GridLine/ToggleGridline %t")]
	private static void Init()
	{
		ToggleGridLine();
	}

	private static void ToggleGridLine()
	{
		if (!EditorPrefs.HasKey(enableFlagStringPrefs))
			EditorPrefs.SetBool(enableFlagStringPrefs, false);
		var isEnable = EditorPrefs.GetBool(enableFlagStringPrefs);
		EditorPrefs.SetBool(enableFlagStringPrefs, !isEnable);
	}

	[CustomEditor(typeof(Transform))]
	public class SceneGUITest : Editor
	{
		[DrawGizmo(GizmoType.NotInSelectionHierarchy)]
		static void RenderCustomGizmo(Transform objectTransform, GizmoType gizmoType)
		{
			if (!EditorPrefs.GetBool(enableFlagStringPrefs))
				return;
			
			var xOffset = 0f;
			var yOffset = 0f;
			if (shiftToMiddle)
			{
				xOffset = gridSize.x / 2;
				yOffset = gridSize.y / 2;
			}

			Gizmos.color = Color.white;
			for (float i = 0; i <= maxGrid.x; i+=gridSize.x)
				Gizmos.DrawLine(new Vector3(i - xOffset, 0.0f, 0 - yOffset), new Vector3(i - xOffset, 0.0f, maxGrid.x - yOffset));
			for (float j = 0; j <= maxGrid.y; j+=gridSize.y)
				Gizmos.DrawLine(new Vector3(0 - xOffset, 0.0f, j - yOffset), new Vector3(maxGrid.y - xOffset, 0.0f, j - yOffset));
			SceneView.RepaintAll();
		}

	}

	public class SettingWindow : EditorWindow
	{
		Rect SettingSection;

		[Range(0, 100)]
		static Vector2 tempGridSize;
		[Range(0, 100)]
		static Vector2 tempMaxGrid;
		static bool tempShiftToMiddle;

		[MenuItem("GameEditor/GridLine/Setting")]
		static void Init()
		{
			var window = (SettingWindow)GetWindow(typeof(SettingWindow));
			window.minSize = new Vector2(400, 300);
			window.Show();
		}

		void OnEnable()
		{
			InitData();
		}

		void InitData()
		{
			tempGridSize = gridSize;
			tempMaxGrid = maxGrid;
			tempShiftToMiddle = shiftToMiddle;
		}

		void OnGUI()
		{
			DrawLayouts();
			DrawSetting();
		}

		void DrawLayouts()
		{
			SettingSection.width = Screen.width;
			SettingSection.height = Screen.height;
		}

		void DrawSetting()
		{
			GUILayout.BeginArea(SettingSection);
			
			tempGridSize.x = Mathf.Clamp(tempGridSize.x, 0.5f, 100);
			tempGridSize.y = Mathf.Clamp(tempGridSize.y, 0.5f, 100);

			tempMaxGrid.x = Mathf.Clamp(tempMaxGrid.x, 2, 100);
			tempMaxGrid.y = Mathf.Clamp(tempMaxGrid.y, 2, 100);

			tempGridSize = EditorGUILayout.Vector2Field("Grid Size", tempGridSize);
			tempMaxGrid = EditorGUILayout.Vector2Field("Max Grid", tempMaxGrid);
			
			GUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Shift to Middle ");
			tempShiftToMiddle = EditorGUILayout.Toggle(tempShiftToMiddle);
			GUILayout.EndHorizontal();

			if (GUILayout.Button("Update"))
			{
				UpdateValue();
			}

			GUILayout.EndArea();
		}

		void UpdateValue()
		{
			gridSize = tempGridSize;
			maxGrid = tempMaxGrid;
			shiftToMiddle = tempShiftToMiddle;

			EditorPrefs.SetFloat(gridSizePrefs + "x", gridSize.x);
			EditorPrefs.SetFloat(gridSizePrefs + "y", gridSize.y);
			EditorPrefs.SetBool(shiftToMiddlePrefs, shiftToMiddle);
		}
	}
}
