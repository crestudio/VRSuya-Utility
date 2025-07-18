#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

using VRC.SDK3.Dynamics.PhysBone.Components;

using static VRSuya.Core.Unity;

/*
 * VRSuya Utility
 * Contact : vrsuya@gmail.com // Twitter : https://twitter.com/VRSuya
 */

namespace com.vrsuya.utility {

	public class SkirtColliderGenerator : MonoBehaviour {

		public struct SkirtColliderParameters {
			public Vector3 TopCirclePoint1;
			public Vector3 TopCirclePoint2;
			public Vector3 TopCirclePoint3;
			public Vector3 TopCirclePoint4;
			public Vector3 BottomCirclePoint1;
			public Vector3 BottomCirclePoint2;
			public Vector3 BottomCirclePoint3;
			public Vector3 BottomCirclePoint4;
			public GameObject TargetGameObject;
			public int TargetColliderCount;
			public float TargetColliderRadius;
			public float TargetColliderHeight;
		}
		private static string ColliderNamePrefix = "SkirtCollider";

		private static string UndoGroupName = "VRSuya SkirtColliderGenerator";
		private static int UndoGroupIndex;

		/// <summary>치마용 PhysBone 콜라이더들을 지정한 GameObject 하위에 생성합니다</summary>
		/// <param name="TargetSkirtColliderParameters">치마 콜라이더 생성용 파라메터</param>
		private static void GenerateColliders(SkirtColliderParameters TargetSkirtColliderParameters) {
			Vector3 CenterPoint = GetCenterPoint(TargetSkirtColliderParameters);
			for (int Index = 0; Index < TargetSkirtColliderParameters.TargetColliderCount; Index++) {
				Vector3 TopPoint = Vector3.zero;
				Vector3 BottomPoint = Vector3.zero;
				Vector3 Direction = BottomPoint - TopPoint;
				Vector3 MiddlePoint = (TopPoint + BottomPoint) / 2f;

				Quaternion NewRotation = Quaternion.FromToRotation(Vector3.up, Direction.normalized);
				Vector3 Outward = Vector3.Cross(Direction.normalized, Vector3.up).normalized;
				Vector3 Offset = Outward * TargetSkirtColliderParameters.TargetColliderRadius;
				Vector3 NewPosition = MiddlePoint + Offset;

				GameObject NewGameObject = new GameObject($"{ColliderNamePrefix}_{Index:D2}");
				NewGameObject.transform.SetParent(TargetSkirtColliderParameters.TargetGameObject.transform);
				NewGameObject.transform.position = NewPosition;
				NewGameObject.transform.rotation = NewRotation;

				VRCPhysBoneCollider NewCollider = NewGameObject.AddComponent<VRCPhysBoneCollider>();
				NewCollider.shapeType = VRCPhysBoneCollider.ShapeType.Capsule;
				NewCollider.radius = TargetSkirtColliderParameters.TargetColliderRadius;
				NewCollider.height = TargetSkirtColliderParameters.TargetColliderHeight;
			}
			return;
		}

		/// <summary>상단 원과 하단 원의 중심점을 계산해서 반환합니다</summary>
		/// <param name="TargetSkirtColliderParameters">치마 콜라이더 생성용 파라메터</param>
		/// <returns>Vector3 타입의 중심점</returns>
		private static Vector3 GetCenterPoint(SkirtColliderParameters TargetSkirtColliderParameters) {
			Vector3[] Points = new Vector3[] { 
				TargetSkirtColliderParameters.TopCirclePoint1,
				TargetSkirtColliderParameters.TopCirclePoint2,
				TargetSkirtColliderParameters.TopCirclePoint3,
				TargetSkirtColliderParameters.TopCirclePoint4,
				TargetSkirtColliderParameters.BottomCirclePoint1,
				TargetSkirtColliderParameters.BottomCirclePoint2,
				TargetSkirtColliderParameters.BottomCirclePoint3,
				TargetSkirtColliderParameters.BottomCirclePoint4
			};
			Vector3 NewCenterPoint = Vector3.zero;
			foreach (Vector3 TargetPoint in Points) { NewCenterPoint += TargetPoint; }
			NewCenterPoint /= Points.Length;
			return NewCenterPoint;
		}

		[ExecuteInEditMode]
		public class SkirtColliderGeneratorEditor : EditorWindow {

			public static Vector3 NewTopCirclePoint1 = Vector3.zero;
			public static Vector3 NewTopCirclePoint2 = Vector3.zero;
			public static Vector3 NewTopCirclePoint3 = Vector3.zero;
			public static Vector3 NewTopCirclePoint4 = Vector3.zero;
			public static Vector3 NewBottomCirclePoint1 = Vector3.zero;
			public static Vector3 NewBottomCirclePoint2 = Vector3.zero;
			public static Vector3 NewBottomCirclePoint3 = Vector3.zero;
			public static Vector3 NewBottomCirclePoint4 = Vector3.zero;
			public static Object NewGameObject = null;
			public static int NewColliderCount = 8;
			public static float NewColliderRadius = 0.1f;
			public static float NewColliderHeight = 0.5f;

			[MenuItem("Tools/VRSuya/Utility/SkirtColliderGenerator", priority = 1000)]
			public static void CreateWindow() {
				SkirtColliderGeneratorEditor AppWindow = (SkirtColliderGeneratorEditor)GetWindowWithRect(typeof(SkirtColliderGeneratorEditor), new Rect(0, 0, 230, 600), true, "SkirtColliderGenerator");
				return;
			}

			private void OnGUI() {
				EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);

				GUILayout.BeginHorizontal();
				GUILayout.FlexibleSpace();
				GUILayout.Label("Top Circle Points", EditorStyles.boldLabel);
				GUILayout.FlexibleSpace();
				GUILayout.EndHorizontal();

				GUILayout.BeginHorizontal();
				GUILayout.FlexibleSpace();
				NewTopCirclePoint1 = EditorGUILayout.Vector3Field(GUIContent.none, NewTopCirclePoint1, GUILayout.Width(200));
				GUILayout.FlexibleSpace();
				GUILayout.EndHorizontal();

				GUILayout.BeginHorizontal();
				GUILayout.FlexibleSpace();
				NewTopCirclePoint2 = EditorGUILayout.Vector3Field(GUIContent.none, NewTopCirclePoint2, GUILayout.Width(200));
				GUILayout.FlexibleSpace();
				GUILayout.EndHorizontal();

				GUILayout.BeginHorizontal();
				GUILayout.FlexibleSpace();
				NewTopCirclePoint3 = EditorGUILayout.Vector3Field(GUIContent.none, NewTopCirclePoint3, GUILayout.Width(200));
				GUILayout.FlexibleSpace();
				GUILayout.EndHorizontal();

				GUILayout.BeginHorizontal();
				GUILayout.FlexibleSpace();
				NewTopCirclePoint4 = EditorGUILayout.Vector3Field(GUIContent.none, NewTopCirclePoint4, GUILayout.Width(200));
				GUILayout.FlexibleSpace();
				GUILayout.EndHorizontal();

				GUILayout.BeginHorizontal();
				GUILayout.FlexibleSpace();
				GUILayout.Label("Bottom Circle Points", EditorStyles.boldLabel);
				GUILayout.FlexibleSpace();
				GUILayout.EndHorizontal();

				GUILayout.BeginHorizontal();
				GUILayout.FlexibleSpace();
				NewBottomCirclePoint1 = EditorGUILayout.Vector3Field(GUIContent.none, NewBottomCirclePoint1, GUILayout.Width(200));
				GUILayout.FlexibleSpace();
				GUILayout.EndHorizontal();

				GUILayout.BeginHorizontal();
				GUILayout.FlexibleSpace();
				NewBottomCirclePoint2 = EditorGUILayout.Vector3Field(GUIContent.none, NewBottomCirclePoint2, GUILayout.Width(200));
				GUILayout.FlexibleSpace();
				GUILayout.EndHorizontal();

				GUILayout.BeginHorizontal();
				GUILayout.FlexibleSpace();
				NewBottomCirclePoint3 = EditorGUILayout.Vector3Field(GUIContent.none, NewBottomCirclePoint3, GUILayout.Width(200));
				GUILayout.FlexibleSpace();
				GUILayout.EndHorizontal();

				GUILayout.BeginHorizontal();
				GUILayout.FlexibleSpace();
				NewBottomCirclePoint4 = EditorGUILayout.Vector3Field(GUIContent.none, NewBottomCirclePoint4, GUILayout.Width(200));
				GUILayout.FlexibleSpace();
				GUILayout.EndHorizontal();

				EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);

				GUILayout.BeginHorizontal();
				GUILayout.FlexibleSpace();
				NewGameObject = EditorGUILayout.ObjectField("Parent", NewGameObject, typeof(GameObject), true, GUILayout.Width(200));
				GUILayout.FlexibleSpace();
				GUILayout.EndHorizontal();

				GUILayout.BeginHorizontal();
				GUILayout.FlexibleSpace();
				NewColliderCount = EditorGUILayout.IntField("Count", NewColliderCount, GUILayout.Width(200));
				GUILayout.FlexibleSpace();
				GUILayout.EndHorizontal();

				GUILayout.BeginHorizontal();
				GUILayout.FlexibleSpace();
				NewColliderRadius = EditorGUILayout.FloatField("Radius", NewColliderRadius, GUILayout.Width(200));
				GUILayout.FlexibleSpace();
				GUILayout.EndHorizontal();

				GUILayout.BeginHorizontal();
				GUILayout.FlexibleSpace();
				NewColliderHeight = EditorGUILayout.FloatField("Height", NewColliderHeight, GUILayout.Width(200));
				GUILayout.FlexibleSpace();
				GUILayout.EndHorizontal();

				EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);

				GUILayout.BeginHorizontal();
				GUILayout.FlexibleSpace();
				EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);
				if (GUILayout.Button("Generate", GUILayout.Width(100))) {
					SkirtColliderParameters NewSkirtColliderParameters = new SkirtColliderParameters {
						TopCirclePoint1 = NewTopCirclePoint1,
						TopCirclePoint2 = NewTopCirclePoint2,
						TopCirclePoint3 = NewTopCirclePoint3,
						TopCirclePoint4 = NewTopCirclePoint4,
						BottomCirclePoint1 = NewBottomCirclePoint1,
						BottomCirclePoint2 = NewBottomCirclePoint2,
						BottomCirclePoint3 = NewBottomCirclePoint3,
						BottomCirclePoint4 = NewBottomCirclePoint4,
						TargetGameObject = (GameObject)NewGameObject,
						TargetColliderCount = NewColliderCount,
						TargetColliderRadius = NewColliderRadius,
						TargetColliderHeight = NewColliderHeight
					};
					GenerateColliders(NewSkirtColliderParameters);
				}
				GUILayout.FlexibleSpace();
				GUILayout.EndHorizontal();

				EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);
				return;
			}
		}
	}
}
#endif