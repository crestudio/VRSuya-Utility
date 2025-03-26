using UnityEngine;
using UnityEditor;

/*
 * VRSuya Utility
 * Contact : vrsuya@gmail.com // Twitter : https://twitter.com/VRSuya
 */

namespace com.vrsuya.utility {

	public class MaterialTemplateEditor : EditorWindow {

		MaterialTemplate MaterialTemplateInstance;
		SerializedObject SerializedMaterialTemplate;

		SerializedProperty SerializedTargetGameObject;
		SerializedProperty SerializedTargetMaterials;
		SerializedProperty SerializedTargetTexture2Ds;

		SerializedProperty SerializedUpdatelilToon;
		SerializedProperty SerializedUpdatepoiyomi;
		SerializedProperty SerializedUpdateUnityChanToonShader;

		SerializedProperty SerializedUpdatelilToonBasic;
		SerializedProperty SerializedUpdatelilToonLighting;
		SerializedProperty SerializedUpdatelilToonShadow;
		SerializedProperty SerializedUpdatelilToonReceiveShadow;
		SerializedProperty SerializedUpdatelilToonBackfaceMask;
		SerializedProperty SerializedUpdatelilToonBacklight;

		SerializedProperty SerializedUpdateUTSTextureShared;
		SerializedProperty SerializedUpdateUTSNormalMap;
		SerializedProperty SerializedUpdateUTSBasicShading;
		SerializedProperty SerializedUpdateUTSLightColor;
		SerializedProperty SerializedUpdateUTSEnvironmentalLightingPropertys;

		SerializedProperty SerializedUpdateRenderQueue;
		SerializedProperty SerializedUpdateGPUInstancing;
		SerializedProperty SerializedUpdateGlobalIllumination;

		SerializedProperty SerializedAnalyzeTextures;
		SerializedProperty SerializedUpdatesRGB;
		SerializedProperty SerializedUpdateNormal;
		SerializedProperty SerializedUpdateAlpha;
		SerializedProperty SerializedUpdateMaxTextureSize;
		SerializedProperty SerializedUpdateOverrideStandalone;

		SerializedProperty SerializedReturnString;

		public static bool FoldlilToon = false;
		public static bool Foldpoiyomi = false;
		public static bool FoldUnityChanToonShader = false;
		public static bool FoldTexture = false;

		void OnEnable() {
			MaterialTemplateInstance = CreateInstance<MaterialTemplate>();
			SerializedMaterialTemplate = new SerializedObject(MaterialTemplateInstance);
			SerializedTargetGameObject = SerializedMaterialTemplate.FindProperty("TargetGameObject");
			SerializedTargetMaterials = SerializedMaterialTemplate.FindProperty("TargetMaterials");
			SerializedTargetTexture2Ds = SerializedMaterialTemplate.FindProperty("TargetTexture2Ds");

			SerializedUpdatelilToon = SerializedMaterialTemplate.FindProperty("UpdatelilToon");
			SerializedUpdatepoiyomi = SerializedMaterialTemplate.FindProperty("Updatepoiyomi");
			SerializedUpdateUnityChanToonShader = SerializedMaterialTemplate.FindProperty("UpdateUnityChanToonShader");

			SerializedUpdatelilToonBasic = SerializedMaterialTemplate.FindProperty("UpdatelilToonBasic");
			SerializedUpdatelilToonLighting = SerializedMaterialTemplate.FindProperty("UpdatelilToonLighting");
			SerializedUpdatelilToonShadow = SerializedMaterialTemplate.FindProperty("UpdatelilToonShadow");
			SerializedUpdatelilToonReceiveShadow = SerializedMaterialTemplate.FindProperty("UpdatelilToonReceiveShadow");
			SerializedUpdatelilToonBackfaceMask = SerializedMaterialTemplate.FindProperty("UpdatelilToonBackfaceMask");
			SerializedUpdatelilToonBacklight = SerializedMaterialTemplate.FindProperty("UpdatelilToonBacklight");

			SerializedUpdateUTSTextureShared = SerializedMaterialTemplate.FindProperty("UpdateUTSTextureShared");
			SerializedUpdateUTSNormalMap = SerializedMaterialTemplate.FindProperty("UpdateUTSNormalMap");
			SerializedUpdateUTSBasicShading = SerializedMaterialTemplate.FindProperty("UpdateUTSBasicShading");
			SerializedUpdateUTSLightColor = SerializedMaterialTemplate.FindProperty("UpdateUTSLightColor");
			SerializedUpdateUTSEnvironmentalLightingPropertys = SerializedMaterialTemplate.FindProperty("UpdateUTSEnvironmentalLightingPropertys");

			SerializedUpdateRenderQueue = SerializedMaterialTemplate.FindProperty("UpdateRenderQueue");
			SerializedUpdateGPUInstancing = SerializedMaterialTemplate.FindProperty("UpdateGPUInstancing");
			SerializedUpdateGlobalIllumination = SerializedMaterialTemplate.FindProperty("UpdateGlobalIllumination");

			SerializedAnalyzeTextures = SerializedMaterialTemplate.FindProperty("AnalyzeTextures");
			SerializedUpdatesRGB = SerializedMaterialTemplate.FindProperty("UpdatesRGB");
			SerializedUpdateNormal = SerializedMaterialTemplate.FindProperty("UpdateNormal");
			SerializedUpdateAlpha = SerializedMaterialTemplate.FindProperty("UpdateAlpha");
			SerializedUpdateMaxTextureSize = SerializedMaterialTemplate.FindProperty("UpdateMaxTextureSize");
			SerializedUpdateOverrideStandalone = SerializedMaterialTemplate.FindProperty("UpdateOverrideStandalone");

			SerializedReturnString = SerializedMaterialTemplate.FindProperty("ReturnString");
		}

		[MenuItem("Tools/VRSuya/Utility/MaterialTemplate", priority = 1000)]
		static void CreateWindow() {
			MaterialTemplateEditor AppWindow = (MaterialTemplateEditor)GetWindow(typeof(MaterialTemplateEditor), true, "MaterialTemplate");
			AppWindow.minSize = new Vector2(550, 425);
			return;
		}

		void OnGUI() {
			if (MaterialTemplateInstance == null) {
				Close();
				return;
			}
			SerializedMaterialTemplate.Update();
			EditorGUILayout.PropertyField(SerializedTargetGameObject, new GUIContent("아바타"));
			EditorGUILayout.PropertyField(SerializedTargetMaterials, new GUIContent("머테리얼"));
			EditorGUILayout.PropertyField(SerializedTargetTexture2Ds, new GUIContent("텍스쳐"));
			if (GUILayout.Button("아바타 텍스쳐 추가")) {
				MaterialTemplateInstance.AddAvatarTextures();
			}
			if (GUILayout.Button("모든 텍스쳐 추가")) {
				MaterialTemplateInstance.AddTexture2Ds();
			}
			if (GUILayout.Button("아바타 DXT1 아닌 텍스쳐 추가")) {
				MaterialTemplateInstance.AddAvatarNotDXT1Textures();
			}
			if (GUILayout.Button("모든 DXT1 아닌 텍스쳐 추가")) {
				MaterialTemplateInstance.AddNotDXT1Textures();
			}
			EditorGUILayout.LabelField("변환을 적용할 쉐이더", EditorStyles.boldLabel);
			EditorGUI.indentLevel++;
			EditorGUILayout.PropertyField(SerializedUpdatelilToon, new GUIContent("lilToon"));
			if (GUILayout.Button("lilToon 머테리얼 추가")) {
				MaterialTemplateInstance.AddlilToonMaterials();
			}
			EditorGUILayout.PropertyField(SerializedUpdatepoiyomi, new GUIContent("poiyomi"));
			if (GUILayout.Button("poiyomi 머테리얼 추가")) {
				MaterialTemplateInstance.AddpoiyomiMaterials();
			}
			EditorGUILayout.PropertyField(SerializedUpdateUnityChanToonShader, new GUIContent("UnityChanToonShader"));
			if (GUILayout.Button("UTS 머테리얼 추가")) {
				MaterialTemplateInstance.AddUnityChanToonShaderMaterials();
			}
			EditorGUI.indentLevel--;
			EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);
			FoldlilToon = EditorGUILayout.Foldout(FoldlilToon, "lilToon 프로퍼티");
			if (FoldlilToon) {
				EditorGUI.indentLevel++;
				EditorGUILayout.PropertyField(SerializedUpdatelilToonBasic, new GUIContent("베이직 설정"));
				EditorGUILayout.PropertyField(SerializedUpdatelilToonLighting, new GUIContent("라이팅 설정"));
				EditorGUILayout.PropertyField(SerializedUpdatelilToonShadow, new GUIContent("그림자 설정"));
				EditorGUILayout.PropertyField(SerializedUpdatelilToonReceiveShadow, new GUIContent("그림자 영향 설정"));
				EditorGUILayout.PropertyField(SerializedUpdatelilToonBackfaceMask, new GUIContent("Back 페이스 마스킹"));
				EditorGUILayout.PropertyField(SerializedUpdatelilToonBacklight, new GUIContent("백라이트 설정"));
				EditorGUILayout.PropertyField(SerializedUpdateRenderQueue, new GUIContent("RenderQueue 설정"));
				EditorGUILayout.PropertyField(SerializedUpdateGPUInstancing, new GUIContent("GPU 인스턴싱 설정"));
				EditorGUILayout.PropertyField(SerializedUpdateGlobalIllumination, new GUIContent("Global Illumination 설정"));
				EditorGUI.indentLevel--;
			}
			Foldpoiyomi = EditorGUILayout.Foldout(Foldpoiyomi, "poiyomi 프로퍼티");
			if (Foldpoiyomi) {
				EditorGUI.indentLevel++;
				EditorGUILayout.PropertyField(SerializedUpdateRenderQueue, new GUIContent("RenderQueue 설정"));
				EditorGUILayout.PropertyField(SerializedUpdateGPUInstancing, new GUIContent("GPU 인스턴싱 설정"));
				EditorGUILayout.PropertyField(SerializedUpdateGlobalIllumination, new GUIContent("Global Illumination 설정"));
				EditorGUI.indentLevel--;
			}
			FoldUnityChanToonShader = EditorGUILayout.Foldout(FoldUnityChanToonShader, "UnityChanToonShader 프로퍼티");
			if (FoldUnityChanToonShader) {
				EditorGUI.indentLevel++;
				EditorGUILayout.PropertyField(SerializedUpdateUTSTextureShared, new GUIContent("텍스쳐 공유 설정"));
				EditorGUILayout.PropertyField(SerializedUpdateUTSNormalMap, new GUIContent("노멀맵 적용 설정"));
				EditorGUILayout.PropertyField(SerializedUpdateUTSBasicShading, new GUIContent("기본 쉐이딩 설정"));
				EditorGUILayout.PropertyField(SerializedUpdateUTSLightColor, new GUIContent("주광색 영향 설정"));
				EditorGUILayout.PropertyField(SerializedUpdateUTSEnvironmentalLightingPropertys, new GUIContent("환경광 설정"));
				EditorGUILayout.PropertyField(SerializedUpdateRenderQueue, new GUIContent("RenderQueue 설정"));
				EditorGUILayout.PropertyField(SerializedUpdateGPUInstancing, new GUIContent("GPU 인스턴싱 설정"));
				EditorGUILayout.PropertyField(SerializedUpdateGlobalIllumination, new GUIContent("Global Illumination 설정"));
				EditorGUI.indentLevel--;
			}
			FoldTexture = EditorGUILayout.Foldout(FoldTexture, "텍스쳐 프로퍼티");
			if (FoldTexture) {
				EditorGUI.indentLevel++;
				EditorGUILayout.PropertyField(SerializedAnalyzeTextures, new GUIContent("텍스쳐 분석"));
				GUI.enabled = SerializedAnalyzeTextures.boolValue;
				EditorGUILayout.PropertyField(SerializedUpdatesRGB, new GUIContent("sRGB 텍스쳐 분석"));
				EditorGUILayout.PropertyField(SerializedUpdateNormal, new GUIContent("노멀 텍스쳐 분석"));
				EditorGUILayout.PropertyField(SerializedUpdateAlpha, new GUIContent("알파 텍스쳐 분석"));
				GUI.enabled = true;
				EditorGUILayout.PropertyField(SerializedUpdateMaxTextureSize, new GUIContent("최대 해상도 설정"));
				EditorGUILayout.PropertyField(SerializedUpdateOverrideStandalone, new GUIContent("오버라이드 설정"));
				EditorGUI.indentLevel--;
			}
			if (!string.IsNullOrEmpty(SerializedReturnString.stringValue)) {
				EditorGUILayout.HelpBox(SerializedReturnString.stringValue, MessageType.Info);
			}
			SerializedMaterialTemplate.ApplyModifiedProperties();
			EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
			if (GUILayout.Button("업데이트")) {
				MaterialTemplateInstance.UpdateMaterialPropertys();
				Repaint();
			}
		}
	}
}
