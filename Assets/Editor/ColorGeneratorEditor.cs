using UnityEngine;
using UnityEditor;

/*
 * VRSuya Utility
 * Contact : vrsuya@gmail.com // Twitter : https://twitter.com/VRSuya
 */

namespace com.vrsuya.utility {

	public class ColorGeneratorEditor : EditorWindow {

		private static SerializedObject SerializedColorGenerator;
		SerializedProperty SerializedShadeColor1;
		SerializedProperty SerializedShadeColor2;
		SerializedProperty SerializedShadeColor3;
		SerializedProperty SerializedShadeColor4;
		SerializedProperty SerializedRimLightColor;
		SerializedProperty SerializedRimShadowColor;
		SerializedProperty SerializedTargetMaterial;
		SerializedProperty SerializedTargetMaterials;

		private static readonly Rect DefaultWindowRect = new Rect(100, 100, 730, 425);

		// ColorBox Rect 변수
		private float BorderX = 30f;
		private float BorderY = EditorGUIUtility.singleLineHeight * 3;
		private float ShadowRectWidth = float.NaN;
		private float ShadeRectWidth = float.NaN;
		private float RectHeight = 100f;
		private float ColorFieldOffset = 2f;
		private float ButtonWidth = float.NaN;
		private float SpaceWidth = 10f;
		private Rect ShadeBoxPosition1 = new Rect();
		private Rect ShadeBoxPosition2 = new Rect();
		private Rect ShadeBoxPosition3 = new Rect();
		private Rect ShadeBoxPosition4 = new Rect();
		private Rect RimLightBoxPosition1 = new Rect();
		private Rect RimLightBoxPosition2 = new Rect();
		private Rect RimShadeBoxPosition1 = new Rect();
		private Rect RimShadeBoxPosition2 = new Rect();

		void OnEnable() {
			if (SerializedColorGenerator == null) {
				SerializedColorGenerator = new SerializedObject(ColorGenerator.Instance);
			}
			SerializedShadeColor1 = SerializedColorGenerator.FindProperty("ShadeColor1");
			SerializedShadeColor2 = SerializedColorGenerator.FindProperty("ShadeColor2");
			SerializedShadeColor3 = SerializedColorGenerator.FindProperty("ShadeColor3");
			SerializedShadeColor4 = SerializedColorGenerator.FindProperty("ShadeColor4");
			SerializedRimLightColor = SerializedColorGenerator.FindProperty("RimLightColor");
			SerializedRimShadowColor = SerializedColorGenerator.FindProperty("RimShadeColor");
			SerializedTargetMaterial = SerializedColorGenerator.FindProperty("TargetMaterial");
			SerializedTargetMaterials = SerializedColorGenerator.FindProperty("TargetMaterials");
			return;
		}

		[MenuItem("Tools/VRSuya/Utility/ColorGenerator", priority = 1000)]
		static void CreateWindow() {
			ColorGeneratorEditor AppWindow = (ColorGeneratorEditor)GetWindowWithRect(typeof(ColorGeneratorEditor), DefaultWindowRect, true, "ColorGenerator");
			AppWindow.minSize = new Vector2(550, 425);
			return;
		}

		void OnGUI() {
			if (SerializedColorGenerator == null) {
				Close();
				return;
			}
			SerializedColorGenerator.Update();
			Vector2 WindowSize = position.size;
			GUIStyle CenteredStyle = new GUIStyle(EditorStyles.label) { alignment = TextAnchor.MiddleCenter };
			UpdateRect(WindowSize);
			(ShadeBoxPosition1, ShadeBoxPosition2, ShadeBoxPosition3, ShadeBoxPosition4) = GetShadeBoxPosition();
			(RimLightBoxPosition1, RimLightBoxPosition2, RimShadeBoxPosition1, RimShadeBoxPosition2) = GetRimShadeBoxPosition();
			Rect WhiteColorBox = new Rect(BorderX, BorderY, (WindowSize.x - BorderX * 2), RectHeight + (RectHeight / 4));
			Rect ShadeColorBox1 = ShadeBoxPosition1;
			Rect ShadeColorBox2 = ShadeBoxPosition2;
			Rect ShadeColorBox3 = ShadeBoxPosition3;
			Rect ShadeColorBox4 = ShadeBoxPosition4;
			Rect RimLightColorBox1 = RimLightBoxPosition1;
			Rect RimLightColorBox2 = RimLightBoxPosition2;
			Rect RimShadeColorBox1 = RimShadeBoxPosition1;
			Rect RimShadeColorBox2 = RimShadeBoxPosition2;
			GUILayout.Space(EditorGUIUtility.singleLineHeight);
			GUILayout.BeginHorizontal();
			GUILayout.Space(BorderX);
			EditorGUILayout.PropertyField(SerializedTargetMaterial, new GUIContent("색상 추출할 머테리얼"), GUILayout.Width(ShadeRectWidth * 1.5f));
			GUILayout.Space(SpaceWidth);
			if (GUILayout.Button("추출", GUILayout.Width(ButtonWidth + ColorFieldOffset))) {
				ColorGenerator.Instance.RequestGetMaterialShadeColor();
			}
			GUILayout.Space(BorderX);
			GUILayout.EndHorizontal();
			GUILayout.Space(RectHeight + (RectHeight / 4) + EditorGUIUtility.singleLineHeight);
			EditorGUI.DrawRect(WhiteColorBox, SerializedShadeColor1.colorValue);
			EditorGUI.DrawRect(ShadeColorBox1, SerializedShadeColor1.colorValue);
			EditorGUI.DrawRect(ShadeColorBox2, SerializedShadeColor2.colorValue);
			EditorGUI.DrawRect(ShadeColorBox3, SerializedShadeColor3.colorValue);
			EditorGUI.DrawRect(ShadeColorBox4, SerializedShadeColor4.colorValue);
			EditorGUI.DrawRect(RimLightColorBox1, MultiplyColor(SerializedShadeColor1.colorValue, SerializedRimLightColor.colorValue));
			EditorGUI.DrawRect(RimLightColorBox2, MultiplyColor(SerializedShadeColor2.colorValue, SerializedRimLightColor.colorValue));
			EditorGUI.DrawRect(RimShadeColorBox1, MultiplyColor(SerializedShadeColor3.colorValue, SerializedRimShadowColor.colorValue));
			EditorGUI.DrawRect(RimShadeColorBox2, MultiplyColor(SerializedShadeColor4.colorValue, SerializedRimShadowColor.colorValue));
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
				ColorGenerator.Instance.ModifiedColor1();
			}
			if (GUILayout.Button("재계산", GUILayout.Width(ShadowRectWidth - ColorFieldOffset))) {
				ColorGenerator.Instance.ModifiedColor2();
			}
			if (GUILayout.Button("재계산", GUILayout.Width(ShadowRectWidth - ColorFieldOffset))) {
				ColorGenerator.Instance.ModifiedColor3();
			}
			if (GUILayout.Button("재계산", GUILayout.Width(ShadowRectWidth - ColorFieldOffset))) {
				ColorGenerator.Instance.ModifiedColor4();
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
			if (GUILayout.Button("생성", GUILayout.Width(ButtonWidth * 0.75f))) {
				ColorGenerator.Instance.RequestCreateColorDelta();
				ColorGeneratorEditor_NewColorDelta.CreateWindow();
			}
			if (GUILayout.Button("편집", GUILayout.Width(ButtonWidth * 0.75f))) {
				ColorGeneratorEditor_NewColorDelta.CreateWindow();
			}
			if (GUILayout.Button("가져오기", GUILayout.Width(ButtonWidth * 0.75f))) {
				Close();
			}
			if (GUILayout.Button("내보내기", GUILayout.Width(ButtonWidth * 0.75f))) {
				Close();
			}
			GUILayout.Space(BorderX);
			GUILayout.EndHorizontal();
			EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);
			EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
			EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);
			GUILayout.BeginHorizontal();
			GUILayout.Space(BorderX);
			EditorGUILayout.PropertyField(SerializedTargetMaterials, new GUIContent("적용 대상 머테리얼"), GUILayout.Width(ShadeRectWidth * 1.5f));
			GUILayout.Space(SpaceWidth);
			if (GUILayout.Button("적용", GUILayout.Width(ButtonWidth / 2))) {
				ColorGenerator.Instance.RequestSetMaterialShadeColor();
				Repaint();
			}
			if (GUILayout.Button("실행 취소", GUILayout.Width(ButtonWidth / 2))) {
				Undo.PerformUndo();
				Repaint();
			}
			GUILayout.Space(BorderX);
			GUILayout.EndHorizontal();
			EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);
			SerializedColorGenerator.ApplyModifiedProperties();
			return;
		}

		private void UpdateRect(Vector2 CurrentWindowSize) {
			ShadowRectWidth = (CurrentWindowSize.x - BorderX * 2) / 4;
			ShadeRectWidth = (CurrentWindowSize.x - BorderX * 2) / 2;
			ButtonWidth = (CurrentWindowSize.x - BorderX * 2) - (ShadeRectWidth * 1.5f) - SpaceWidth - ColorFieldOffset;
			return;
		}

		private (Rect, Rect, Rect, Rect) GetShadeBoxPosition() {
			Rect NewShadeBoxPosition1 = new Rect(BorderX + ShadowRectWidth * 0, BorderY, ShadowRectWidth, RectHeight + (RectHeight / 4));
			Rect NewShadeBoxPosition2 = new Rect(BorderX + ShadowRectWidth * 1, BorderY, ShadowRectWidth, RectHeight + (RectHeight / 4));
			Rect NewShadeBoxPosition3 = new Rect(BorderX + ShadowRectWidth * 2, BorderY, ShadowRectWidth, RectHeight + (RectHeight / 4));
			Rect NewShadeBoxPosition4 = new Rect(BorderX + ShadowRectWidth * 3, BorderY, ShadowRectWidth, RectHeight + (RectHeight / 4));
			return (NewShadeBoxPosition1, NewShadeBoxPosition2, NewShadeBoxPosition3, NewShadeBoxPosition4);
		}

		private (Rect, Rect, Rect, Rect) GetRimShadeBoxPosition() {
			Rect NewRimLightBoxPosition1 = new Rect(BorderX + ShadowRectWidth * 0, BorderY + RectHeight, ShadowRectWidth, RectHeight / 4);
			Rect NewRimLightBoxPosition2 = new Rect(BorderX + ShadowRectWidth * 1, BorderY + RectHeight, ShadowRectWidth, RectHeight / 4);
			Rect NewRimShadeBoxPosition1 = new Rect(BorderX + ShadowRectWidth * 2, BorderY + RectHeight, ShadowRectWidth, RectHeight / 4);
			Rect NewRimShadeBoxPosition2 = new Rect(BorderX + ShadowRectWidth * 3, BorderY + RectHeight, ShadowRectWidth, RectHeight / 4);
			return (NewRimLightBoxPosition1, NewRimLightBoxPosition2, NewRimShadeBoxPosition1, NewRimShadeBoxPosition2);
		}

		private Color MultiplyColor(Color OriginalColor, Color TargetColor) {
			float NewR = OriginalColor.r * TargetColor.r;
			float NewG = OriginalColor.g * TargetColor.g;
			float NewB = OriginalColor.b * TargetColor.b;
			return new Color(NewR, NewG, NewB, TargetColor.a);
		}
	}
}