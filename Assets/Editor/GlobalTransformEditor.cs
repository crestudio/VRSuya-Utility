using UnityEngine;
using UnityEditor;

/*
 * VRSuya Utility
 * Contact : vrsuya@gmail.com // Twitter : https://twitter.com/VRSuya
 */

namespace VRSuya.Utility {

	[CustomEditor(typeof(GlobalTransform))]
	[CanEditMultipleObjects]
	public class GlobalTransformEditor : Editor {

		const string UndoGroupName = "VRSuya Global Transform";

		public override void OnInspectorGUI() {
			GlobalTransform TargetGlobalTransform = (GlobalTransform)target;
			Transform TargetTransformComponent = TargetGlobalTransform.transform;
			EditorGUILayout.Space(2f);
			EditorGUI.BeginChangeCheck();
			Vector3 CurrentGlobalPosition = TargetTransformComponent.position;
			Vector3 NewGlobalPosition = EditorGUILayout.Vector3Field("Position", CurrentGlobalPosition);
			Vector3 CurrentGlobalRotation = TargetTransformComponent.eulerAngles;
			Vector3 NewGlobalRotation = EditorGUILayout.Vector3Field("Rotation", CurrentGlobalRotation);
			if (EditorGUI.EndChangeCheck()) {
				Undo.RecordObject(TargetTransformComponent, UndoGroupName);
				TargetTransformComponent.position = NewGlobalPosition;
				TargetTransformComponent.eulerAngles = NewGlobalRotation;
				EditorUtility.SetDirty(TargetTransformComponent);
			}
			GUI.enabled = false;
			Vector3 CurrentGlobalScale = TargetTransformComponent.lossyScale;
			EditorGUILayout.Vector3Field("Scale", CurrentGlobalScale);
			GUI.enabled = true;
		}
	}
}