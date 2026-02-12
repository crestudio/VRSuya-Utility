using UnityEngine;
using UnityEditor;

/*
 * VRSuya Utility
 * Contact : vrsuya@gmail.com // Twitter : https://twitter.com/VRSuya
 */

namespace com.vrsuya.utility {

	public class ColorGeneratorEditor_NewColorDelta : EditorWindow {

		static SerializedObject SerializedColorGenerator;
		SerializedProperty SerializedTargetColorDelta;
		SerializedProperty SerializedShadeName_EN;
		SerializedProperty SerializedShadeName_KO;
		SerializedProperty SerializedShadeName_JA;
		SerializedProperty SerializedShadeTypeIndex;
		SerializedProperty SerializedReferenceColor;
		SerializedProperty SerializedColorDelta1;
		SerializedProperty SerializedColorDelta2;
		SerializedProperty SerializedColorDelta3;
		SerializedProperty SerializedRimLightDelta;
		SerializedProperty SerializedRimShadeDelta;

		static readonly Rect DefaultWindowRect = new Rect(0, 0, 420, 435);

		// ColorBox Rect 변수
		float BorderX = 30f;
		float BorderY = 30f;
		float ScreenWidth;
		float ButtonWidth = 150f;

		void OnEnable() {
			if (SerializedColorGenerator == null) {
				SerializedColorGenerator = new SerializedObject(ColorGenerator.Instance);
			}
			SerializedTargetColorDelta = SerializedColorGenerator.FindProperty("TargetColorDelta");
			SerializedShadeName_EN = SerializedTargetColorDelta.FindPropertyRelative("Name_EN");
			SerializedShadeName_KO = SerializedTargetColorDelta.FindPropertyRelative("Name_KO");
			SerializedShadeName_JA = SerializedTargetColorDelta.FindPropertyRelative("Name_JA");
			SerializedShadeTypeIndex = SerializedTargetColorDelta.FindPropertyRelative("ShadeTypeIndex");
			SerializedReferenceColor = SerializedTargetColorDelta.FindPropertyRelative("ReferenceColor");
			SerializedColorDelta1 = SerializedTargetColorDelta.FindPropertyRelative("ColorDelta1");
			SerializedColorDelta2 = SerializedTargetColorDelta.FindPropertyRelative("ColorDelta2");
			SerializedColorDelta3 = SerializedTargetColorDelta.FindPropertyRelative("ColorDelta3");
			SerializedRimLightDelta = SerializedTargetColorDelta.FindPropertyRelative("RimLightDelta");
			SerializedRimShadeDelta = SerializedTargetColorDelta.FindPropertyRelative("RimShadeDelta");
		}

		public static void CreateWindow() {
			ColorGeneratorEditor_NewColorDelta AppWindow = (ColorGeneratorEditor_NewColorDelta)GetWindowWithRect(typeof(ColorGeneratorEditor_NewColorDelta), DefaultWindowRect, true, "New ColorDelta");
			AppWindow.minSize = new Vector2(365, 435);
		}

		void OnGUI() {
			if (SerializedColorGenerator == null) {
				Close();
				return;
			}
			SerializedColorGenerator.Update();
			ScreenWidth = (position.size.x - BorderX * 2);
			GUILayout.Space(BorderY);
			GUILayout.BeginHorizontal();
			GUILayout.Space(BorderX);
			EditorGUILayout.PropertyField(SerializedShadeName_EN, new GUIContent("쉐이딩 영어 이름"), GUILayout.Width(ScreenWidth));
			GUILayout.Space(BorderX);
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal();
			GUILayout.Space(BorderX);
			EditorGUILayout.PropertyField(SerializedShadeName_KO, new GUIContent("쉐이딩 한국어 이름"), GUILayout.Width(ScreenWidth));
			GUILayout.Space(BorderX);
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal();
			GUILayout.Space(BorderX);
			EditorGUILayout.PropertyField(SerializedShadeName_JA, new GUIContent("쉐이딩 일본어 이름"), GUILayout.Width(ScreenWidth));
			GUILayout.Space(BorderX);
			GUILayout.EndHorizontal();
			EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);
			GUI.enabled = false;
			GUILayout.BeginHorizontal();
			GUILayout.Space(BorderX);
			EditorGUILayout.PropertyField(SerializedReferenceColor, new GUIContent("레퍼런스 컬러(HEX)"), GUILayout.Width(ScreenWidth));
			GUILayout.Space(BorderX);
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal();
			GUILayout.Space(BorderX);
			EditorGUILayout.PropertyField(SerializedColorDelta1, new GUIContent("그림자 1단 델타"), GUILayout.Width(ScreenWidth));
			GUILayout.Space(BorderX);
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal();
			GUILayout.Space(BorderX);
			EditorGUILayout.PropertyField(SerializedColorDelta2, new GUIContent("그림자 2단 델타"), GUILayout.Width(ScreenWidth));
			GUILayout.Space(BorderX);
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal();
			GUILayout.Space(BorderX);
			EditorGUILayout.PropertyField(SerializedColorDelta3, new GUIContent("그림자 3단 델타"), GUILayout.Width(ScreenWidth));
			GUILayout.Space(BorderX);
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal();
			GUILayout.Space(BorderX);
			EditorGUILayout.PropertyField(SerializedRimLightDelta, new GUIContent("림라이트 델타"), GUILayout.Width(ScreenWidth));
			GUILayout.Space(BorderX);
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal();
			GUILayout.Space(BorderX);
			EditorGUILayout.PropertyField(SerializedRimShadeDelta, new GUIContent("림쉐이드 델타"), GUILayout.Width(ScreenWidth));
			GUILayout.Space(BorderX);
			GUILayout.EndHorizontal();
			GUI.enabled = true;
			EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);
			EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
			EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);
			GUILayout.BeginHorizontal();
			GUILayout.Space(BorderX);
			GUILayout.FlexibleSpace();
			if (GUILayout.Button("저장", GUILayout.Width(ButtonWidth))) {
				ColorGenerator.Instance.SaveColorDelta();
				Close();
			}
			GUILayout.FlexibleSpace();
			GUILayout.Space(BorderX);
			GUILayout.EndHorizontal();
			SerializedColorGenerator.ApplyModifiedProperties();
		}
	}
}