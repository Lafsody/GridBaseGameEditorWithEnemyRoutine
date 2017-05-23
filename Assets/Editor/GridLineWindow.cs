using UnityEditor;
using UnityEngine;
using System.Collections;
using System;

public class GridLineWindow : EditorWindow
{
	static readonly String enableFlagString = "isGridLineEnable";

	static Vector2 gridSize = new Vector2(1, 1);
	static Vector2 maxGrid = new Vector2(100, 100);
	static bool shiftToMiddle = true;

	[MenuItem("Tools/GridLine/ToggleGridline")]
	private static void Init()
	{
		ToggleGridLine();
	}

	private static void ToggleGridLine()
	{
		if (!EditorPrefs.HasKey(enableFlagString))
			EditorPrefs.SetBool(enableFlagString, false);
		var isEnable = EditorPrefs.GetBool(enableFlagString);
		EditorPrefs.SetBool(enableFlagString, !isEnable);
	}

	[CustomEditor(typeof(Transform))]
	public class SceneGUITest : Editor
	{
		[DrawGizmo(GizmoType.NotInSelectionHierarchy)]
		static void RenderCustomGizmo(Transform objectTransform, GizmoType gizmoType)
		{
			if (!EditorPrefs.GetBool(enableFlagString))
				return;
			
			var xOffset = 0f;
			var yOffset = 0f;
			if (shiftToMiddle)
			{
				xOffset = gridSize.x / 2;
				yOffset = gridSize.y / 2;
			}

			Gizmos.color = Color.white;
			for (float i = 0; i < maxGrid.x; i+=gridSize.x)
				Gizmos.DrawLine(new Vector3(i - xOffset, 0.0f, 0 - xOffset), new Vector3(i - xOffset, 0.0f, maxGrid.x - xOffset));
			for (float j = 0; j < maxGrid.y; j+=gridSize.y)
				Gizmos.DrawLine(new Vector3(0 - yOffset, 0.0f, j - yOffset), new Vector3(maxGrid.y - yOffset, 0.0f, j - yOffset));
			SceneView.RepaintAll();
		}

	}
}
