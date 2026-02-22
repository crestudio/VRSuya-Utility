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
		SerializedProperty SerializedReferenceMaterial;
		SerializedProperty SerializedTargetMaterials;
		SerializedProperty SerializedTargetTexture2Ds;

		SerializedProperty SerializedTargetShadow1Color;
		SerializedProperty SerializedTargetShadow2Color;
		SerializedProperty SerializedTargetShadow3Color;
		SerializedProperty SerializedTargetShadowBorderColor;
		SerializedProperty SerializedTargetRimShadeColor;
		SerializedProperty SerializedTargetBacklightColor;
		SerializedProperty SerializedTargetReflectionColor;
		SerializedProperty SerializedTargetRimLightColor;
		SerializedProperty SerializedTargetOutlineColor;
		SerializedProperty SerializedTargetOutlineHighlightColor;

		SerializedProperty SerializedUpdatelilToon;
		SerializedProperty SerializedUpdatepoiyomi;
		SerializedProperty SerializedUpdateUnityChanToonShader;

		SerializedProperty SerializedUpdatelilToonBasic;
		SerializedProperty SerializedUpdatelilToonLighting;
		SerializedProperty SerializedUpdatelilToonShadow;
		SerializedProperty SerializedUpdatelilToonReceiveShadow;
		SerializedProperty SerializedUpdatelilToonBackfaceMask;
		SerializedProperty SerializedUpdatelilToonBacklight;
		SerializedProperty SerializedUpdatelilToonShadowColor;
		SerializedProperty SerializedUpdatelilToonRimShadeColor;
		SerializedProperty SerializedUpdatelilToonBacklightColor;
		SerializedProperty SerializedUpdatelilToonReflectionColor;
		SerializedProperty SerializedUpdatelilToonRimLightColor;
		SerializedProperty SerializedUpdatelilToonOutlineColor;

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

		bool FoldMaterial = true;
		bool FoldMaterialProperty = false;
		bool FoldTexture = false;
		bool FoldTextureProperty = false;
		bool FoldlilToon = false;
		bool FoldlilToonColorPalette = false;
		bool FoldlilToonColor = false;
		bool Foldpoiyomi = false;
		bool FoldUnityChanToonShader = false;

		void OnEnable() {
			MaterialTemplateInstance = CreateInstance<MaterialTemplate>();
			SerializedMaterialTemplate = new SerializedObject(MaterialTemplateInstance);
			SerializedTargetGameObject = SerializedMaterialTemplate.FindProperty("TargetGameObject");
			SerializedReferenceMaterial = SerializedMaterialTemplate.FindProperty("ReferenceMaterial");
			SerializedTargetMaterials = SerializedMaterialTemplate.FindProperty("TargetMaterials");
			SerializedTargetTexture2Ds = SerializedMaterialTemplate.FindProperty("TargetTexture2Ds");

			SerializedTargetShadow1Color = SerializedMaterialTemplate.FindProperty("TargetShadow1Color");
			SerializedTargetShadow2Color = SerializedMaterialTemplate.FindProperty("TargetShadow2Color");
			SerializedTargetShadow3Color = SerializedMaterialTemplate.FindProperty("TargetShadow3Color"); ;
			SerializedTargetShadowBorderColor = SerializedMaterialTemplate.FindProperty("TargetShadowBorderColor");
			SerializedTargetRimShadeColor = SerializedMaterialTemplate.FindProperty("TargetRimShadeColor");
			SerializedTargetBacklightColor = SerializedMaterialTemplate.FindProperty("TargetBacklightColor");
			SerializedTargetReflectionColor = SerializedMaterialTemplate.FindProperty("TargetReflectionColor");
			SerializedTargetRimLightColor = SerializedMaterialTemplate.FindProperty("TargetRimLightColor");
			SerializedTargetOutlineColor = SerializedMaterialTemplate.FindProperty("TargetOutlineColor"); ;
			SerializedTargetOutlineHighlightColor = SerializedMaterialTemplate.FindProperty("TargetOutlineHighlightColor");

			SerializedUpdatelilToon = SerializedMaterialTemplate.FindProperty("UpdatelilToon");
			SerializedUpdatepoiyomi = SerializedMaterialTemplate.FindProperty("Updatepoiyomi");
			SerializedUpdateUnityChanToonShader = SerializedMaterialTemplate.FindProperty("UpdateUnityChanToonShader");

			SerializedUpdatelilToonBasic = SerializedMaterialTemplate.FindProperty("UpdatelilToonBasic");
			SerializedUpdatelilToonLighting = SerializedMaterialTemplate.FindProperty("UpdatelilToonLighting");
			SerializedUpdatelilToonShadow = SerializedMaterialTemplate.FindProperty("UpdatelilToonShadow");
			SerializedUpdatelilToonReceiveShadow = SerializedMaterialTemplate.FindProperty("UpdatelilToonReceiveShadow");
			SerializedUpdatelilToonBackfaceMask = SerializedMaterialTemplate.FindProperty("UpdatelilToonBackfaceMask");
			SerializedUpdatelilToonBacklight = SerializedMaterialTemplate.FindProperty("UpdatelilToonBacklight");
			SerializedUpdatelilToonShadowColor = SerializedMaterialTemplate.FindProperty("UpdatelilToonShadowColor");
			SerializedUpdatelilToonRimShadeColor = SerializedMaterialTemplate.FindProperty("UpdatelilToonRimShadeColor");
			SerializedUpdatelilToonBacklightColor = SerializedMaterialTemplate.FindProperty("UpdatelilToonBacklightColor"); ;
			SerializedUpdatelilToonReflectionColor = SerializedMaterialTemplate.FindProperty("UpdatelilToonReflectionColor"); ;
			SerializedUpdatelilToonRimLightColor = SerializedMaterialTemplate.FindProperty("UpdatelilToonRimLightColor");
			SerializedUpdatelilToonOutlineColor = SerializedMaterialTemplate.FindProperty("UpdatelilToonOutlineColor"); ;

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
			MaterialTemplateEditor AppWindow = GetWindow<MaterialTemplateEditor>(true, "MaterialTemplate", true);
			AppWindow.minSize = new Vector2(300, 550);
		}

		void OnGUI() {
			if (MaterialTemplateInstance == null) {
				Close();
				return;
			}
			SerializedMaterialTemplate.Update();
			FoldMaterial = EditorGUILayout.Foldout(FoldMaterial, "머테리얼");
			if (FoldMaterial) {
				EditorGUI.indentLevel++;
				EditorGUILayout.PropertyField(SerializedReferenceMaterial, new GUIContent("기준 머테리얼"));
				EditorGUILayout.LabelField(string.Empty, GUI.skin.horizontalSlider);
				EditorGUILayout.PropertyField(SerializedTargetMaterials, new GUIContent("머테리얼"));
				EditorGUILayout.BeginHorizontal();
				if (GUILayout.Button("lilToon")) {
					MaterialTemplateInstance.AddlilToonMaterials();
				}
				if (GUILayout.Button("poiyomi")) {
					MaterialTemplateInstance.AddpoiyomiMaterials();
				}
				if (GUILayout.Button("UTS")) {
					MaterialTemplateInstance.AddUnityChanToonShaderMaterials();
				}
				EditorGUILayout.EndHorizontal();
				EditorGUILayout.LabelField(string.Empty, GUI.skin.horizontalSlider);
				FoldMaterialProperty = EditorGUILayout.Foldout(FoldMaterialProperty, "머테리얼 설정");
				if (FoldMaterialProperty) {
					EditorGUI.indentLevel++;
					EditorGUILayout.PropertyField(SerializedUpdatelilToon, new GUIContent("lilToon"));
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
					FoldlilToonColor = EditorGUILayout.Foldout(FoldlilToonColor, "lilToon 컬러");
					if (FoldlilToonColor) {
						EditorGUI.indentLevel++;
						EditorGUILayout.PropertyField(SerializedUpdatelilToonShadowColor, new GUIContent("그림자 컬러 변경"));
						EditorGUILayout.PropertyField(SerializedUpdatelilToonRimShadeColor, new GUIContent("림 쉐이드 컬러 변경"));
						EditorGUILayout.PropertyField(SerializedUpdatelilToonBacklightColor, new GUIContent("백라이트 컬러 변경"));
						EditorGUILayout.PropertyField(SerializedUpdatelilToonReflectionColor, new GUIContent("반사 컬러 변경"));
						EditorGUILayout.PropertyField(SerializedUpdatelilToonRimLightColor, new GUIContent("림 라이트 컬러 변경"));
						EditorGUILayout.PropertyField(SerializedUpdatelilToonOutlineColor, new GUIContent("아웃라인 컬러 변경"));
						EditorGUI.indentLevel--;
					}
					FoldlilToonColorPalette = EditorGUILayout.Foldout(FoldlilToonColorPalette, "lilToon 컬러 팔렛트");
					if (FoldlilToonColorPalette) {
						EditorGUI.indentLevel++;
						EditorGUILayout.PropertyField(SerializedTargetShadow1Color, new GUIContent("그림자 1"));
						EditorGUILayout.PropertyField(SerializedTargetShadow2Color, new GUIContent("그림자 2"));
						EditorGUILayout.PropertyField(SerializedTargetShadow3Color, new GUIContent("그림자 3"));
						EditorGUILayout.PropertyField(SerializedTargetShadowBorderColor, new GUIContent("그림자 경계"));
						EditorGUILayout.PropertyField(SerializedTargetRimShadeColor, new GUIContent("림 쉐도우"));
						EditorGUILayout.PropertyField(SerializedTargetBacklightColor, new GUIContent("백라이트"));
						EditorGUILayout.PropertyField(SerializedTargetReflectionColor, new GUIContent("반사"));
						EditorGUILayout.PropertyField(SerializedTargetRimLightColor, new GUIContent("림 라이트"));
						EditorGUILayout.PropertyField(SerializedTargetOutlineColor, new GUIContent("아웃라인"));
						EditorGUILayout.PropertyField(SerializedTargetOutlineHighlightColor, new GUIContent("아웃라인 하이라이트"));
						EditorGUI.indentLevel--;
					}
					EditorGUILayout.LabelField(string.Empty, GUI.skin.horizontalSlider);
					EditorGUILayout.PropertyField(SerializedUpdatepoiyomi, new GUIContent("poiyomi"));
					Foldpoiyomi = EditorGUILayout.Foldout(Foldpoiyomi, "poiyomi 프로퍼티");
					if (Foldpoiyomi) {
						EditorGUI.indentLevel++;
						EditorGUILayout.PropertyField(SerializedUpdateRenderQueue, new GUIContent("RenderQueue 설정"));
						EditorGUILayout.PropertyField(SerializedUpdateGPUInstancing, new GUIContent("GPU 인스턴싱 설정"));
						EditorGUILayout.PropertyField(SerializedUpdateGlobalIllumination, new GUIContent("Global Illumination 설정"));
						EditorGUI.indentLevel--;
					}
					EditorGUILayout.LabelField(string.Empty, GUI.skin.horizontalSlider);
					EditorGUILayout.PropertyField(SerializedUpdateUnityChanToonShader, new GUIContent("UnityChanToonShader"));
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
				}
				EditorGUI.indentLevel--;
				EditorGUI.indentLevel--;
			}
			
			EditorGUILayout.LabelField(string.Empty, GUI.skin.horizontalSlider);
			FoldTexture = EditorGUILayout.Foldout(FoldTexture, "텍스쳐");
			if (FoldTexture) {
				EditorGUI.indentLevel++;
				EditorGUILayout.PropertyField(SerializedTargetGameObject, new GUIContent("아바타"));
				EditorGUILayout.PropertyField(SerializedTargetTexture2Ds, new GUIContent("텍스쳐"));
				EditorGUILayout.BeginHorizontal();
				if (GUILayout.Button("아바타")) {
					MaterialTemplateInstance.AddAvatarTextures();
				}
				if (GUILayout.Button("모두")) {
					MaterialTemplateInstance.AddTexture2Ds();
				}
				EditorGUILayout.EndHorizontal();
				EditorGUILayout.BeginHorizontal();
				if (GUILayout.Button("아바타 (DXT1 제외)")) {
					MaterialTemplateInstance.AddAvatarNotDXT1Textures();
				}
				if (GUILayout.Button("모두 (DXT1 제외)")) {
					MaterialTemplateInstance.AddNotDXT1Textures();
				}
				EditorGUILayout.EndHorizontal();
				FoldTextureProperty = EditorGUILayout.Foldout(FoldTextureProperty, "텍스쳐 프로퍼티");
				if (FoldTextureProperty) {
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
				EditorGUI.indentLevel--;
			}
			if (!string.IsNullOrEmpty(SerializedReturnString.stringValue)) {
				EditorGUILayout.HelpBox(SerializedReturnString.stringValue, MessageType.Info);
			}
			SerializedMaterialTemplate.ApplyModifiedProperties();
			EditorGUILayout.LabelField(string.Empty, GUI.skin.horizontalSlider);
			if (GUILayout.Button("업데이트")) {
				MaterialTemplateInstance.UpdateMaterialPropertys();
				Repaint();
			}
		}
	}
}
