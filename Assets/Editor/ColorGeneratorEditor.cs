using UnityEngine;
using UnityEditor;

/*
 * VRSuya Utility
 * Contact : vrsuya@gmail.com // Twitter : https://twitter.com/VRSuya
 */

namespace com.vrsuya.utility {

	public class ColorGeneratorEditor : EditorWindow {

		ColorGenerator ColorGeneratorInstance;
		SerializedObject SerializedColorGenerator;
		SerializedProperty SerializedShadeColor1;
		SerializedProperty SerializedShadeColor2;
		SerializedProperty SerializedShadeColor3;
		SerializedProperty SerializedShadeColor4;
		SerializedProperty SerializedRimLightColor;
		SerializedProperty SerializedRimShadowColor;
		SerializedProperty SerializedTargetMaterial;

		// ColorBox Rect 변수
		private float BorderX = 30f;
		private float BorderY = 30f;
		private float ShadowRectWidth = float.NaN;
		private float ShadeRectWidth = float.NaN;
		private float RectHeight = 100f;
		private float ColorFieldOffset = 2f;
		private Rect ShadeBoxPosition1 = new Rect();
		private Rect ShadeBoxPosition2 = new Rect();
		private Rect ShadeBoxPosition3 = new Rect();
		private Rect ShadeBoxPosition4 = new Rect();
		private Rect RimLightBoxPosition = new Rect();
		private Rect RimShadeBoxPosition = new Rect();

		void OnEnable() {
			ColorGeneratorInstance = CreateInstance<ColorGenerator>();
			SerializedColorGenerator = new SerializedObject(ColorGeneratorInstance);
			SerializedShadeColor1 = SerializedColorGenerator.FindProperty("ShadeColor1");
			SerializedShadeColor2 = SerializedColorGenerator.FindProperty("ShadeColor2");
			SerializedShadeColor3 = SerializedColorGenerator.FindProperty("ShadeColor3");
			SerializedShadeColor4 = SerializedColorGenerator.FindProperty("ShadeColor4");
			SerializedRimLightColor = SerializedColorGenerator.FindProperty("RimLightColor");
			SerializedRimShadowColor = SerializedColorGenerator.FindProperty("RimShadeColor");
			SerializedTargetMaterial = SerializedColorGenerator.FindProperty("TargetMaterial");
			return;
		}

		[MenuItem("Tools/VRSuya/ColorGenerator", priority = 1000)]
		static void CreateWindow() {
			ColorGeneratorEditor AppWindow = (ColorGeneratorEditor)GetWindow(typeof(ColorGeneratorEditor), true, "ColorGenerator", true);
			return;
		}

		void OnGUI() {
			if (ColorGeneratorInstance == null) {
				Close();
				return;
			}
			SerializedColorGenerator.Update();
			Vector2 WindowSize = position.size;
			GUIStyle CenteredStyle = new GUIStyle(EditorStyles.label) { alignment = TextAnchor.MiddleCenter };
			(ShadeBoxPosition1, ShadeBoxPosition2, ShadeBoxPosition3, ShadeBoxPosition4) = GetShadeBoxPosition(WindowSize);
			(RimLightBoxPosition, RimShadeBoxPosition) = GetRimShadeBoxPosition(WindowSize);
			GUILayout.Space(BorderY + RectHeight + (RectHeight / 4) + EditorGUIUtility.singleLineHeight);
			Rect ShadeColorBox1 = ShadeBoxPosition1;
			Rect ShadeColorBox2 = ShadeBoxPosition2;
			Rect ShadeColorBox3 = ShadeBoxPosition3;
			Rect ShadeColorBox4 = ShadeBoxPosition4;
			Rect RimLightColorBox = RimLightBoxPosition;
			Rect RimShadeColorBox = RimShadeBoxPosition;
			EditorGUI.DrawRect(ShadeColorBox1, SerializedShadeColor1.colorValue);
			EditorGUI.DrawRect(ShadeColorBox2, SerializedShadeColor2.colorValue);
			EditorGUI.DrawRect(ShadeColorBox3, SerializedShadeColor3.colorValue);
			EditorGUI.DrawRect(ShadeColorBox4, SerializedShadeColor4.colorValue);
			EditorGUI.DrawRect(RimLightColorBox, SerializedRimLightColor.colorValue);
			EditorGUI.DrawRect(RimShadeColorBox, SerializedRimShadowColor.colorValue);
			GUILayout.BeginHorizontal();
			GUILayout.Space(BorderX);
			EditorGUILayout.PropertyField(SerializedShadeColor1, new GUIContent(string.Empty), GUILayout.Width(ShadowRectWidth - ColorFieldOffset));
			EditorGUILayout.PropertyField(SerializedShadeColor2, new GUIContent(string.Empty), GUILayout.Width(ShadowRectWidth - ColorFieldOffset));
			EditorGUILayout.PropertyField(SerializedShadeColor3, new GUIContent(string.Empty), GUILayout.Width(ShadowRectWidth - ColorFieldOffset));
			EditorGUILayout.PropertyField(SerializedShadeColor4, new GUIContent(string.Empty), GUILayout.Width(ShadowRectWidth - ColorFieldOffset));
			GUILayout.Space(BorderX);
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal();
			GUILayout.Space(BorderX);
			EditorGUILayout.PropertyField(SerializedRimLightColor, new GUIContent(string.Empty), GUILayout.Width(ShadeRectWidth - (ColorFieldOffset / 2)));
			EditorGUILayout.PropertyField(SerializedRimShadowColor, new GUIContent(string.Empty), GUILayout.Width(ShadeRectWidth - (ColorFieldOffset / 2)));
			GUILayout.Space(BorderX);
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal();
			GUILayout.Space(BorderX);
			if (GUILayout.Button("재계산", GUILayout.Width(ShadowRectWidth - ColorFieldOffset))) {
				ColorGeneratorInstance.ModifiedColor1();
			}
			if (GUILayout.Button("재계산", GUILayout.Width(ShadowRectWidth - ColorFieldOffset))) {
				ColorGeneratorInstance.ModifiedColor2();
			}
			if (GUILayout.Button("재계산", GUILayout.Width(ShadowRectWidth - ColorFieldOffset))) {
				ColorGeneratorInstance.ModifiedColor3();
			}
			if (GUILayout.Button("재계산", GUILayout.Width(ShadowRectWidth - ColorFieldOffset))) {
				ColorGeneratorInstance.ModifiedColor4();
			}
			GUILayout.Space(BorderX);
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal();
			GUILayout.Space(BorderX);
			GUILayout.Label("베이스 컬러", CenteredStyle, GUILayout.Width(ShadowRectWidth));
			GUILayout.Label("1단 그림자", CenteredStyle, GUILayout.Width(ShadowRectWidth));
			GUILayout.Label("2단 그림자", CenteredStyle, GUILayout.Width(ShadowRectWidth));
			GUILayout.Label("3단 그림자", CenteredStyle, GUILayout.Width(ShadowRectWidth));
			GUILayout.Space(BorderX);
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal();
			GUILayout.Space(BorderX);
			GUILayout.Label("림라이트 컬러", CenteredStyle, GUILayout.Width(ShadeRectWidth));
			GUILayout.Label("림쉐도우 컬러", CenteredStyle, GUILayout.Width(ShadeRectWidth));
			GUILayout.Space(BorderX);
			GUILayout.EndHorizontal();
			EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);
			GUILayout.BeginHorizontal();
			GUILayout.Space(BorderX);
			GUILayout.FlexibleSpace();
			EditorGUILayout.PropertyField(SerializedTargetMaterial, new GUIContent(string.Empty));
			GUILayout.Space(10);
			if (GUILayout.Button("추출", GUILayout.Width(100))) {
				ColorGeneratorInstance.RequestGetMaterialShadeColor();
			}
			GUILayout.Space(10);
			if (GUILayout.Button("적용", GUILayout.Width(100))) {
				ColorGeneratorInstance.RequestSetMaterialShadeColor();
				Repaint();
			}
			GUILayout.Space(10);
			if (GUILayout.Button("실행 취소", GUILayout.Width(100))) {
				ColorGeneratorInstance.DebugColorDelta();
				// Undo.PerformUndo();
				// Repaint();
			}
			GUILayout.FlexibleSpace();
			GUILayout.Space(BorderX);
			GUILayout.EndHorizontal();
			EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);
			UpdateModifiedColor();
			SerializedColorGenerator.ApplyModifiedProperties();
			return;
		}

		private (Rect, Rect, Rect, Rect) GetShadeBoxPosition(Vector2 CurrentWindowSize) {
			ShadowRectWidth = (CurrentWindowSize.x - BorderX * 2) / 4;
			Rect NewShadeBoxPosition1 = new Rect(BorderX + ShadowRectWidth * 0, BorderY, ShadowRectWidth, RectHeight);
			Rect NewShadeBoxPosition2 = new Rect(BorderX + ShadowRectWidth * 1, BorderY, ShadowRectWidth, RectHeight);
			Rect NewShadeBoxPosition3 = new Rect(BorderX + ShadowRectWidth * 2, BorderY, ShadowRectWidth, RectHeight);
			Rect NewShadeBoxPosition4 = new Rect(BorderX + ShadowRectWidth * 3, BorderY, ShadowRectWidth, RectHeight);
			return (NewShadeBoxPosition1, NewShadeBoxPosition2, NewShadeBoxPosition3, NewShadeBoxPosition4);
		}

		private (Rect, Rect) GetRimShadeBoxPosition(Vector2 CurrentWindowSize) {
			ShadeRectWidth = (CurrentWindowSize.x - BorderX * 2) / 2;
			Rect NewRimLightBoxPosition = new Rect(BorderX + ShadeRectWidth * 0, BorderY + RectHeight, ShadeRectWidth, RectHeight / 4);
			Rect NewRimShadeBoxPosition = new Rect(BorderX + ShadeRectWidth * 1, BorderY + RectHeight, ShadeRectWidth, RectHeight / 4);
			return (NewRimLightBoxPosition, NewRimShadeBoxPosition);
		}

		private void UpdateModifiedColor() {
			if (SerializedColorGenerator.hasModifiedProperties) {
				if (SerializedShadeColor1.prefabOverride) {
					ColorGeneratorInstance.ModifiedColor1();
					Debug.Log("ShadeColor1이 변경되었습니다.");
				}
				if (SerializedShadeColor2.prefabOverride) {
					ColorGeneratorInstance.ModifiedColor2();
					Debug.Log("ShadeColor2이 변경되었습니다.");
				}
				if (SerializedShadeColor3.prefabOverride) {
					ColorGeneratorInstance.ModifiedColor3();
					Debug.Log("ShadeColor3이 변경되었습니다.");
				}
				if (SerializedShadeColor4.prefabOverride) {
					ColorGeneratorInstance.ModifiedColor4();
					Debug.Log("ShadeColor4이 변경되었습니다.");
				}
			}
			return;
		}
	}
}