using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEditor;

/*
 * VRSuya Utility
 * Contact : vrsuya@gmail.com // Twitter : https://twitter.com/VRSuya
 */

namespace com.vrsuya.utility {

    [CustomEditor(typeof(SkirtColliderGenerator))]
    public class SkirtColliderGeneratorEditor : Editor {

		SerializedProperty SerializedTopCirclePoint_12;
		SerializedProperty SerializedTopCirclePoint_3;
		SerializedProperty SerializedTopCirclePoint_6;
		SerializedProperty SerializedTopCirclePoint_9;
		SerializedProperty SerializedBottomCirclePoint_12;
		SerializedProperty SerializedBottomCirclePoint_3;
		SerializedProperty SerializedBottomCirclePoint_6;
		SerializedProperty SerializedBottomCirclePoint_9;
		SerializedProperty SerializedFrontCurvature;
		SerializedProperty SerializedTopFrontCurvature;
		SerializedProperty SerializedBottomFrontCurvature;
		SerializedProperty SerializedBackCurvature;
		SerializedProperty SerializedTopBackCurvature;
		SerializedProperty SerializedBottomBackCurvature;
		SerializedProperty SerializedSampleCount;
		SerializedProperty SerializedTargetOffset;
		SerializedProperty SerializedTargetShiftOffset;
		SerializedProperty SerializedTargetRadius;
		SerializedProperty SerializedTargetHeight;
		SerializedProperty SerializedGizmoSize;
		SerializedProperty SerializedHipsTransform;
		SerializedProperty SerializedLeftLegTransform;
		SerializedProperty SerializedRightLegTransform;
		SerializedProperty SerializedColliderNamePrefix;

		void OnEnable() {
			SerializedTopCirclePoint_12 = serializedObject.FindProperty("TopCirclePoint_12");
			SerializedTopCirclePoint_3 = serializedObject.FindProperty("TopCirclePoint_3");
			SerializedTopCirclePoint_6 = serializedObject.FindProperty("TopCirclePoint_6");
			SerializedTopCirclePoint_9 = serializedObject.FindProperty("TopCirclePoint_9");
			SerializedBottomCirclePoint_12 = serializedObject.FindProperty("BottomCirclePoint_12");
			SerializedBottomCirclePoint_3 = serializedObject.FindProperty("BottomCirclePoint_3");
			SerializedBottomCirclePoint_6 = serializedObject.FindProperty("BottomCirclePoint_6");
			SerializedBottomCirclePoint_9 = serializedObject.FindProperty("BottomCirclePoint_9");
			SerializedFrontCurvature = serializedObject.FindProperty("FrontCurvature");
			SerializedTopFrontCurvature = serializedObject.FindProperty("TopFrontCurvature");
			SerializedBottomFrontCurvature = serializedObject.FindProperty("BottomFrontCurvature");
			SerializedBackCurvature = serializedObject.FindProperty("BackCurvature");
			SerializedTopBackCurvature = serializedObject.FindProperty("TopBackCurvature");
			SerializedBottomBackCurvature = serializedObject.FindProperty("BottomBackCurvature");
			SerializedSampleCount = serializedObject.FindProperty("SampleCount");
			SerializedTargetOffset = serializedObject.FindProperty("TargetOffset");
			SerializedTargetShiftOffset = serializedObject.FindProperty("TargetShiftOffset");
			SerializedTargetRadius = serializedObject.FindProperty("TargetRadius");
			SerializedTargetHeight = serializedObject.FindProperty("TargetHeight");
			SerializedGizmoSize = serializedObject.FindProperty("GizmoSize");
			SerializedHipsTransform = serializedObject.FindProperty("HipsTransform");
			SerializedLeftLegTransform = serializedObject.FindProperty("LeftLegTransform");
			SerializedRightLegTransform = serializedObject.FindProperty("RightLegTransform");
			SerializedColliderNamePrefix = serializedObject.FindProperty("ColliderNamePrefix");
		}

        public override void OnInspectorGUI() {
			serializedObject.Update();
			SkirtColliderGenerator Instance = (SkirtColliderGenerator)target;
			EditorGUILayout.LabelField("Bone Transform");
			EditorGUI.indentLevel++;
			EditorGUILayout.PropertyField(SerializedHipsTransform, new GUIContent("Hips"));
			EditorGUILayout.PropertyField(SerializedLeftLegTransform, new GUIContent("Left Leg"));
			EditorGUILayout.PropertyField(SerializedRightLegTransform, new GUIContent("Right Leg"));
			EditorGUI.indentLevel--;
			EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
			EditorGUILayout.LabelField("Top Circle Points");
			EditorGUI.indentLevel++;
			EditorGUILayout.PropertyField(SerializedTopCirclePoint_12, new GUIContent("Front"));
			EditorGUILayout.PropertyField(SerializedTopCirclePoint_3, new GUIContent("Right"));
			EditorGUILayout.PropertyField(SerializedTopCirclePoint_6, new GUIContent("Back"));
			EditorGUILayout.PropertyField(SerializedTopCirclePoint_9, new GUIContent("Left"));
			EditorGUI.indentLevel--;
			EditorGUILayout.LabelField("Bottom Circle Points");
			EditorGUI.indentLevel++;
			EditorGUILayout.PropertyField(SerializedBottomCirclePoint_12, new GUIContent("Front"));
			EditorGUILayout.PropertyField(SerializedBottomCirclePoint_3, new GUIContent("Right"));
			EditorGUILayout.PropertyField(SerializedBottomCirclePoint_6, new GUIContent("Back"));
			EditorGUILayout.PropertyField(SerializedBottomCirclePoint_9, new GUIContent("Left"));
			EditorGUI.indentLevel--;
			EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
			EditorGUILayout.LabelField("Front Curvature");
			EditorGUI.indentLevel++;
			EditorGUILayout.PropertyField(SerializedFrontCurvature, new GUIContent("Full"));
			EditorGUILayout.PropertyField(SerializedTopFrontCurvature, new GUIContent("Top"));
			EditorGUILayout.PropertyField(SerializedBottomFrontCurvature, new GUIContent("Bottom"));
			EditorGUI.indentLevel--;
			EditorGUILayout.LabelField("Back Curvature");
			EditorGUI.indentLevel++;
			EditorGUILayout.PropertyField(SerializedBackCurvature, new GUIContent("Full"));
			EditorGUILayout.PropertyField(SerializedTopBackCurvature, new GUIContent("Top"));
			EditorGUILayout.PropertyField(SerializedBottomBackCurvature, new GUIContent("Bottom"));
			EditorGUI.indentLevel--;
			EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
			EditorGUILayout.LabelField("Option");
			EditorGUI.indentLevel++;
			EditorGUILayout.PropertyField(SerializedSampleCount, new GUIContent("Sample Count"));
			EditorGUILayout.PropertyField(SerializedTargetOffset, new GUIContent("Offset"));
			EditorGUILayout.PropertyField(SerializedTargetShiftOffset, new GUIContent("Shift Offset"));
			EditorGUILayout.PropertyField(SerializedTargetRadius, new GUIContent("Radius"));
			EditorGUILayout.PropertyField(SerializedTargetHeight, new GUIContent("Height"));
			EditorGUILayout.PropertyField(SerializedGizmoSize, new GUIContent("Gizmo Size"));
			EditorGUI.indentLevel--;
			EditorGUILayout.LabelField(string.Empty);
			serializedObject.ApplyModifiedProperties();
			if (GUILayout.Button("Generate")) {
				(target as SkirtColliderGenerator).GeneratePhysBoneColliders();
			}
		}
    }
}