using System.Collections.Generic;

using UnityEngine;
using UnityEditor;

using static VRSuya.Core.Translator;

/*
 * VRSuya Utility
 * Contact : vrsuya@gmail.com // Twitter : https://twitter.com/VRSuya
 */

namespace VRSuya.Utility {

	[InitializeOnLoad]
	public class AnimatorView : Editor {

		static Dictionary<SceneView, string> SceneViewModes = new Dictionary<SceneView, string>();

		static AnimatorView() {
			EditorApplication.update += OnEditorUpdate;
		}

		static void OnEditorUpdate() {
			if (AnimatorViewEditor.IsSceneViewLocked) {
				if ((GameObject)AnimatorViewEditor.TargetGameObject) {
					CheckSceneViewModes();
					foreach (SceneView TargetSceneView in SceneView.sceneViews) {
						Vector3 TargetInterestingPoint = ((GameObject)AnimatorViewEditor.TargetGameObject).transform.localPosition + AnimatorViewEditor.TargetOffset;
						Vector3 TargetWorldPosition = ((GameObject)AnimatorViewEditor.TargetGameObject).transform.parent.TransformPoint(TargetInterestingPoint);
						if (!AnimatorViewEditor.IsRotationLocked) {
							TargetSceneView.pivot = TargetWorldPosition;
							TargetSceneView.size = AnimatorViewEditor.TargetSceneZoom;
						} else {
							Quaternion ReferenceRotation = ((GameObject)AnimatorViewEditor.TargetGameObject).transform.rotation;
							Quaternion TopView = ReferenceRotation * Quaternion.LookRotation(Vector3.up);
							Quaternion BottomView = ReferenceRotation * Quaternion.LookRotation(Vector3.down);
							Quaternion LeftView = ReferenceRotation * Quaternion.LookRotation(Vector3.left);
							Quaternion RightView = ReferenceRotation * Quaternion.LookRotation(Vector3.right);
							Quaternion FrontView = ReferenceRotation * Quaternion.LookRotation(Vector3.forward);
							Quaternion BackView = ReferenceRotation * Quaternion.LookRotation(Vector3.back);
							switch (SceneViewModes[TargetSceneView]) {
								case "Top":
									TargetSceneView.LookAt(TargetWorldPosition, BottomView, AnimatorViewEditor.TargetSceneZoom, true, true);
									break;
								case "Bottom":
									TargetSceneView.LookAt(TargetWorldPosition, TopView, AnimatorViewEditor.TargetSceneZoom, true, true);
									break;
								case "Left":
									TargetSceneView.LookAt(TargetWorldPosition, RightView, AnimatorViewEditor.TargetSceneZoom, true, true);
									break;
								case "Right":
									TargetSceneView.LookAt(TargetWorldPosition, LeftView, AnimatorViewEditor.TargetSceneZoom, true, true);
									break;
								case "Front":
									TargetSceneView.LookAt(TargetWorldPosition, BackView, AnimatorViewEditor.TargetSceneZoom, true, true);
									break;
								case "Back":
									TargetSceneView.LookAt(TargetWorldPosition, FrontView, AnimatorViewEditor.TargetSceneZoom, true, true);
									break;
								default:
									TargetSceneView.LookAt(TargetWorldPosition, BackView, AnimatorViewEditor.TargetSceneZoom, true, true);
									break;
							}
						}
						TargetSceneView.Repaint();
					}
				}
			}
		}

		static void CheckSceneViewModes() {
			if (AnimatorViewEditor.IsRotationLocked) {
				if (SceneViewModes.Count == 0) {
					foreach (SceneView TargetSceneView in SceneView.sceneViews) {
						SceneViewModes.Add(TargetSceneView, GetViewName(TargetSceneView.camera.transform.forward));
					}
				}
			} else {
				if (SceneViewModes.Count != 0) {
					SceneViewModes = new Dictionary<SceneView, string>();
				}
			}
		}

		static string GetViewName(Vector3 Direction) {
			if (Direction == Vector3.down) return "Top";
			if (Direction == Vector3.up) return "Bottom";
			if (Direction == Vector3.left) return "Right";
			if (Direction == Vector3.right) return "Left";
			if (Direction == Vector3.forward) return "Back";
			if (Direction == Vector3.back) return "Front";
			return "Custom";
		}

		public class AnimatorViewEditor : EditorWindow {

			public static bool IsSceneViewLocked = false;
			public static bool IsRotationLocked = false;
			public static Object TargetGameObject = null;
			public static Vector3 TargetOffset = Vector3.zero;
			public static float TargetSceneZoom = 0.2f;

			const float BorderX = 15f;

			[MenuItem("Tools/VRSuya/Utility/AnimatorView", priority = 1000)]
			static void CreateWindow() {
				AnimatorViewEditor AppWindow = GetWindowWithRect<AnimatorViewEditor>(new Rect(0, 0, 240, 180), true, "VRSuya AnimatorView");
			}

			void OnGUI() {
				EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);
				GUILayout.BeginHorizontal();
				GUILayout.Space(BorderX);
				GUILayout.FlexibleSpace();
				GUILayout.Label(GetTranslatedString("String_FollowGameObject"), EditorStyles.boldLabel);
				GUILayout.FlexibleSpace();
				GUILayout.Space(BorderX);
				GUILayout.EndHorizontal();
				GUILayout.BeginHorizontal();
				GUILayout.Space(BorderX);
				IsSceneViewLocked = EditorGUILayout.ToggleLeft(GetTranslatedString("String_Active"), IsSceneViewLocked);
				GUILayout.Space(BorderX);
				GUILayout.EndHorizontal();
				GUILayout.BeginHorizontal();
				GUILayout.Space(BorderX);
				IsRotationLocked = EditorGUILayout.ToggleLeft(GetTranslatedString("String_LockRotation"), IsRotationLocked);
				GUILayout.Space(BorderX);
				GUILayout.EndHorizontal();
				GUILayout.BeginHorizontal();
				GUILayout.Space(BorderX);
				TargetGameObject = EditorGUILayout.ObjectField(GUIContent.none, TargetGameObject, typeof(GameObject), true);
				GUILayout.Space(BorderX);
				GUILayout.EndHorizontal();
				GUILayout.BeginHorizontal();
				GUILayout.Space(BorderX);
				TargetOffset = EditorGUILayout.Vector3Field(GUIContent.none, TargetOffset);
				GUILayout.Space(BorderX);
				GUILayout.EndHorizontal();
				GUILayout.BeginHorizontal();
				GUILayout.Space(BorderX);
				TargetSceneZoom = EditorGUILayout.Slider(GUIContent.none, TargetSceneZoom, 0.0f, 1.5f);
				GUILayout.Space(BorderX);
				GUILayout.EndHorizontal();
				GUILayout.BeginHorizontal();
				GUILayout.Space(BorderX);
				GUILayout.FlexibleSpace();
				if (GUILayout.Button(GetTranslatedString("String_Close"), GUILayout.Width(100))) {
					Close();
				}
				GUILayout.FlexibleSpace();
				GUILayout.Space(BorderX);
				GUILayout.EndHorizontal();
				EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);
			}
		}
	}
}