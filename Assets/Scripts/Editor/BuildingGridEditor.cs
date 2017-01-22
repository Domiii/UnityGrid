using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(BuildingGrid))]
public class BuildingGridEditor : Editor {


	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();

		var grid = (BuildingGrid)target;
		grid.Awake ();

		if (GUILayout.Button ("Snap all buildings")) {
			var buildings = GameObject.FindObjectsOfType<GridSnappedSprite> ();

			foreach (var building in buildings) {
				grid.SnapToGrid (building.GetComponent<SpriteRenderer> ());
			}
		}
	}
}
