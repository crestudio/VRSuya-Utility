#if UNITY_EDITOR
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;

using Object = UnityEngine.Object;

/*
 * VRSuya Utility
 * Contact : vrsuya@gmail.com // Twitter : https://twitter.com/VRSuya
 */

namespace com.vrsuya.utility {

	[InitializeOnLoad]
	public class AnimatorView : MonoBehaviour {

		private static Dictionary<SceneView, string> SceneViewModes = new Dictionary<SceneView, string>();

		/// <summary>Unity Editor가 매 프레임마다 Scene을 업데이트 하도록 합니다.</summary>
		static AnimatorView() {
			EditorApplication.update += OnEditorUpdate;
		}

		/// <summary>해당 오브젝트를 기준으로 Scene 뷰를 정렬합니다.</summary>
		private static void OnEditorUpdate() {
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

		/// <summary>Scene이 어느 방향을 향하고 있는지 검사합니다.</summary>
		private static void CheckSceneViewModes() {
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

		/// <summary>Vector 방향을 반환합니다.</summary>
		private static string GetViewName(Vector3 Direction) {
			if (Direction == Vector3.down) return "Top";
			if (Direction == Vector3.up) return "Bottom";
			if (Direction == Vector3.left) return "Right";
			if (Direction == Vector3.right) return "Left";
			if (Direction == Vector3.forward) return "Back";
			if (Direction == Vector3.back) return "Front";
			return "Custom";
		}

		[ExecuteInEditMode]
		public class AnimatorViewEditor : EditorWindow {

			public static bool IsSceneViewLocked = false;
			public static bool IsRotationLocked = false;
			public static Object TargetGameObject = null;
			public static Vector3 TargetOffset = Vector3.zero;
			public static float TargetSceneZoom = 0.2f;

			[MenuItem("Tools/VRSuya/Utility/AnimatorView", priority = 1000)]
			static void CreateWindow() {
				AnimatorViewEditor AppWindow = (AnimatorViewEditor)GetWindowWithRect(typeof(AnimatorViewEditor), new Rect(0, 0, 230, 180), true, "AnimatorView");
			}

			void OnGUI() {
				EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);
				GUILayout.BeginHorizontal();
				GUILayout.FlexibleSpace();
				GUILayout.Label("Track the GameObject", EditorStyles.boldLabel);
				GUILayout.FlexibleSpace();
				GUILayout.EndHorizontal();
				GUILayout.BeginHorizontal();
				GUILayout.FlexibleSpace();
				IsSceneViewLocked = EditorGUILayout.Toggle("Active", IsSceneViewLocked, GUILayout.Width(200));
				GUILayout.FlexibleSpace();
				GUILayout.EndHorizontal();
				GUILayout.BeginHorizontal();
				GUILayout.FlexibleSpace();
				IsRotationLocked = EditorGUILayout.Toggle("Rotation Lock", IsRotationLocked, GUILayout.Width(200));
				GUILayout.FlexibleSpace();
				GUILayout.EndHorizontal();
				GUILayout.BeginHorizontal();
				GUILayout.FlexibleSpace();
				TargetGameObject = EditorGUILayout.ObjectField(GUIContent.none, TargetGameObject, typeof(GameObject), true, GUILayout.Width(200));
				GUILayout.FlexibleSpace();
				GUILayout.EndHorizontal();
				GUILayout.BeginHorizontal();
				GUILayout.FlexibleSpace();
				TargetOffset = EditorGUILayout.Vector3Field(GUIContent.none, TargetOffset, GUILayout.Width(200));
				GUILayout.FlexibleSpace();
				GUILayout.EndHorizontal();
				GUILayout.BeginHorizontal();
				GUILayout.FlexibleSpace();
				TargetSceneZoom = EditorGUILayout.Slider(GUIContent.none, TargetSceneZoom, 0.0f, 1.5f, GUILayout.Width(200));
				GUILayout.FlexibleSpace();
				GUILayout.EndHorizontal();
				GUILayout.BeginHorizontal();
				GUILayout.FlexibleSpace();
				EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);
				if (GUILayout.Button("Close", GUILayout.Width(100))) {
					Close();
				}
				GUILayout.FlexibleSpace();
				GUILayout.EndHorizontal();
				EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);
			}
		}
	}
}
#endif