using System;

using UnityEngine;
using UnityEditor;

using static VRSuya.Core.Translator;

/*
 * VRSuya Utility
 * Contact : vrsuya@gmail.com // Twitter : https://twitter.com/VRSuya
 */

namespace VRSuya.Utility {

    [CustomEditor(typeof(TextureReplacer))]
    public class TextureReplacerEditor : Editor {

		SerializedProperty SerializedAvatarTextures;

		SerializedProperty SerializedAvatarGameObject;
		SerializedProperty SerializedAvatarMaterials;

		void OnEnable() {
			SerializedAvatarTextures = serializedObject.FindProperty("AvatarTextures");

			SerializedAvatarGameObject = serializedObject.FindProperty("AvatarGameObject");
			SerializedAvatarMaterials = serializedObject.FindProperty("AvatarMaterials");
		}

        public override void OnInspectorGUI() {
			GUIStyle CenteredStyle = new GUIStyle(EditorStyles.label) { alignment = TextAnchor.MiddleCenter };
			GUILayoutOption PropertyWidth = GUILayout.Width((EditorGUIUtility.currentViewWidth - 15f - 50f - 30f) / 2f);
			GUILayoutOption ButtonWidth = GUILayout.Width(50);
			GUILayoutOption ArrowWidth = GUILayout.Width(15);
			TextureReplacer newTextureReplacer = (TextureReplacer)target;
			serializedObject.Update();
			LanguageIndex = EditorGUILayout.Popup(GetTranslatedString("String_Language"), LanguageIndex, LanguageOption);
			EditorGUILayout.PropertyField(SerializedAvatarGameObject, new GUIContent(GetTranslatedString("String_Avatar")));
			GUI.enabled = false;
			EditorGUILayout.PropertyField(SerializedAvatarMaterials, new GUIContent(GetTranslatedString("String_Material")));
			GUI.enabled = true;
			EditorGUILayout.LabelField(GetTranslatedString("String_Texture"), EditorStyles.boldLabel);
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(GetTranslatedString("String_Before"), CenteredStyle, PropertyWidth);
			EditorGUILayout.LabelField("▶", CenteredStyle, ArrowWidth);
			EditorGUILayout.LabelField(GetTranslatedString("String_After"), CenteredStyle, PropertyWidth);
			EditorGUILayout.LabelField(string.Empty, ButtonWidth);
			EditorGUILayout.EndHorizontal();
			if (SerializedAvatarTextures.arraySize > 0) {
				for (int Index = 0; Index < SerializedAvatarTextures.arraySize; Index++) {
					SerializedProperty TextureProperty = SerializedAvatarTextures.GetArrayElementAtIndex(Index);
					SerializedProperty ShowProperty = TextureProperty.FindPropertyRelative("ShowDetails");
					SerializedProperty BeforeProperty = TextureProperty.FindPropertyRelative("BeforeTexture");
					SerializedProperty AfterProperty = TextureProperty.FindPropertyRelative("AfterTexture");
					SerializedProperty MaterialProperty = TextureProperty.FindPropertyRelative("OriginMaterial");
					bool ShowDetailValue = ShowProperty.boolValue;
					EditorGUILayout.BeginHorizontal();
					GUI.enabled = false;
					EditorGUILayout.PropertyField(BeforeProperty, GUIContent.none, PropertyWidth);
					GUI.enabled = true;
					EditorGUILayout.LabelField("▶", CenteredStyle, ArrowWidth);
					EditorGUILayout.PropertyField(AfterProperty, GUIContent.none, PropertyWidth);
					if (GUILayout.Button(ShowDetailValue ? GetTranslatedString("String_Hide") : GetTranslatedString("String_Show"), ButtonWidth)) {
						ShowProperty.boolValue = !ShowDetailValue;
					}
					EditorGUILayout.EndHorizontal();
					if (ShowProperty.boolValue) {
						for (int MaterialIndex = 0; MaterialIndex < MaterialProperty.arraySize; MaterialIndex++) {
							EditorGUI.indentLevel++;
							EditorGUILayout.BeginHorizontal();
							SerializedProperty OriginMaterialProperty = MaterialProperty.GetArrayElementAtIndex(MaterialIndex);
							SerializedProperty OriginMaterial = OriginMaterialProperty.FindPropertyRelative("OriginMaterial");
							SerializedProperty OriginProperty = OriginMaterialProperty.FindPropertyRelative("PropertyName");
							string[] StringPropertys = new string[OriginProperty.arraySize];
							for (int PropertyIndex = 0; PropertyIndex < OriginProperty.arraySize; PropertyIndex++) {
								SerializedProperty StringProperty = OriginProperty.GetArrayElementAtIndex(PropertyIndex);
								StringPropertys[PropertyIndex] = StringProperty.stringValue;
							}
							GUI.enabled = false;
							EditorGUILayout.PropertyField(OriginMaterial, new GUIContent(string.Empty));
							GUI.enabled = true;
							EditorGUILayout.LabelField(String.Join(Environment.NewLine, StringPropertys));
							EditorGUILayout.EndHorizontal();
							EditorGUI.indentLevel--;
						}
					}
				}
			} else {
				EditorGUILayout.HelpBox(GetTranslatedString("NO_DATA"), MessageType.Info);
			}
			EditorGUILayout.HelpBox(GetTranslatedString("String_Null"), MessageType.Info);
			if (GUILayout.Button(GetTranslatedString("String_Refresh"))) {
				(target as TextureReplacer).RefreshAvatarProprety();
				Repaint();
			}
			EditorGUILayout.LabelField(string.Empty, GUI.skin.horizontalSlider);
			serializedObject.ApplyModifiedProperties();
			if (GUILayout.Button(GetTranslatedString("String_Replace"))) {
				(target as TextureReplacer).RequestUpdateAvatarTextures();
				(target as TextureReplacer).RefreshAvatarProprety();
				Repaint();
			}
			EditorGUILayout.BeginHorizontal();
			if (GUILayout.Button(GetTranslatedString("String_Undo"))) {
				Undo.PerformUndo();
				(target as TextureReplacer).RefreshAvatarProprety();
				Repaint();
			}
			if (GUILayout.Button(GetTranslatedString("String_Save"))) {
				AssetDatabase.SaveAssets();
			}
			EditorGUILayout.EndHorizontal();
		}
    }
}

